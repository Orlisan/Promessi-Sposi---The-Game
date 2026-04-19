// ============================================================
//  PolytopeMaterialFixer.cs  v2.0
//  Metti questo file in una cartella "Editor" nel tuo progetto Unity.
//  Menu: Tools → Polytope Material Fixer
// ============================================================

#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;

public class PolytopeMaterialFixer : EditorWindow
{
    private Vector2 _scroll;
    private string  _log      = "";
    private bool    _analyzed = false;

    private List<MaterialInfo> _broken   = new();
    private Texture2D          _foundTex = null;

    private bool   _backupMats    = true;
    private bool   _fixVegetation = true;
    private bool   _fixWater      = false;
    private bool   _fixBark       = true;
    private string _texOverride   = "";

    private const string SHADER_UNLIT      = "Universal Render Pipeline/Unlit";
    private const string SHADER_SIMPLE_LIT = "Universal Render Pipeline/Simple Lit";
    private const string SHADER_LIT        = "Universal Render Pipeline/Lit";

    enum MatType { Opaque, Foliage, Flowers, NPC, Rock, Water, Bark, Unknown }

    [MenuItem("Tools/Polytope Material Fixer")]
    public static void ShowWindow()
    {
        var w = GetWindow<PolytopeMaterialFixer>("Polytope Fixer v2");
        w.minSize = new Vector2(500, 640);
    }

    void OnGUI()
    {
        GUILayout.Label("Polytope Studio - Material Fixer v2.0 (URP)", EditorStyles.boldLabel);
        EditorGUILayout.HelpBox(
            "Converte i materiali Polytope Studio in URP, gestendo correttamente " +
            "ogni tipo: NPC, vegetazione con trasparenza, rocce, acqua, corteccia.",
            MessageType.Info);

        GUILayout.Space(8);
        GUILayout.Label("Opzioni", EditorStyles.boldLabel);
        _backupMats    = EditorGUILayout.Toggle("Crea backup materiali prima del fix", _backupMats);
        _fixVegetation = EditorGUILayout.Toggle("Converti vegetazione (foglie/fiori) con trasparenza", _fixVegetation);
        _fixWater      = EditorGUILayout.Toggle("Converti acqua (risultato approssimativo)", _fixWater);
        _fixBark       = EditorGUILayout.Toggle("Converti corteccia alberi Unity (Bark)", _fixBark);

        GUILayout.Space(6);
        GUILayout.Label("Texture palette (lascia vuoto = ricerca automatica)", EditorStyles.miniBoldLabel);
        using (new EditorGUILayout.HorizontalScope())
        {
            _texOverride = EditorGUILayout.TextField(_texOverride);
            if (GUILayout.Button("Sfoglia", GUILayout.Width(60)))
            {
                string path = EditorUtility.OpenFilePanel("Seleziona texture palette", "Assets", "png,jpg,tga,psd");
                if (!string.IsNullOrEmpty(path))
                    _texOverride = "Assets" + path.Substring(Application.dataPath.Length);
            }
        }

        GUILayout.Space(10);
        using (new EditorGUILayout.HorizontalScope())
        {
            if (GUILayout.Button("1. Analizza", GUILayout.Height(34)))
                Analyze();

            GUI.enabled = _analyzed;
            if (GUILayout.Button("2. Applica Fix", GUILayout.Height(34)))
                ApplyFix();
            GUI.enabled = true;
        }

        GUILayout.Space(6);

        if (_analyzed)
        {
            int opaques  = _broken.FindAll(m => m.type == MatType.Opaque || m.type == MatType.Rock || m.type == MatType.NPC).Count;
            int foliages = _broken.FindAll(m => m.type == MatType.Foliage || m.type == MatType.Flowers).Count;
            int waters   = _broken.FindAll(m => m.type == MatType.Water).Count;
            int barks    = _broken.FindAll(m => m.type == MatType.Bark).Count;

            EditorGUILayout.HelpBox(
                $"Da fixare:  {opaques} opachi/NPC/rocce  |  {foliages} vegetazione  |  {waters} acqua  |  {barks} corteccia\n" +
                (_foundTex != null
                    ? $"Texture palette: {_foundTex.name}"
                    : "ATTENZIONE: Texture palette NON trovata - usa 'Sfoglia' per selezionarla"),
                _foundTex != null ? MessageType.Info : MessageType.Warning);
        }

        GUILayout.Space(4);
        GUILayout.Label("Log", EditorStyles.boldLabel);
        _scroll = EditorGUILayout.BeginScrollView(_scroll, GUILayout.ExpandHeight(true));
        EditorGUILayout.TextArea(_log, GUILayout.ExpandHeight(true));
        EditorGUILayout.EndScrollView();

        if (GUILayout.Button("Pulisci log"))
        {
            _log = ""; _analyzed = false; _broken.Clear(); _foundTex = null;
        }
    }

    void Analyze()
    {
        _broken.Clear();
        _log      = "";
        _analyzed = false;
        Log("=== ANALISI AVVIATA ===\n");

        var guids = AssetDatabase.FindAssets("t:Material", new[] { "Assets" });
        Log($"Materiali totali nel progetto: {guids.Length}");

        foreach (var guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            var mat = AssetDatabase.LoadAssetAtPath<Material>(path);
            if (mat == null || mat.shader == null) continue;

            var type = ClassifyShader(mat.shader.name);
            if (type == MatType.Unknown) continue;

            _broken.Add(new MaterialInfo { path = path, mat = mat, type = type });
            Log($"  ROTTO [{type,-10}]  {mat.name}  ->  {mat.shader.name}");
        }

        _foundTex = FindPaletteTexture();
        Log($"\nTexture trovata: {(_foundTex != null ? AssetDatabase.GetAssetPath(_foundTex) : "NESSUNA")}");
        Log($"\n=== COMPLETATO: {_broken.Count} materiali da convertire ===");
        _analyzed = true;
        Repaint();
    }

    void ApplyFix()
    {
        Log("\n=== APPLICAZIONE FIX ===");
        int ok = 0, skip = 0, fail = 0;

        foreach (var info in _broken)
        {
            if (info.type == MatType.Water && !_fixWater)
                { Log($"  SKIP acqua: {info.mat.name}"); skip++; continue; }
            if (info.type == MatType.Bark && !_fixBark)
                { Log($"  SKIP bark: {info.mat.name}"); skip++; continue; }
            if ((info.type == MatType.Foliage || info.type == MatType.Flowers) && !_fixVegetation)
                { Log($"  SKIP vegetazione: {info.mat.name}"); skip++; continue; }

            try
            {
                if (_backupMats) BackupMaterial(info.path);

                var mat = info.mat;
                Texture existingTex = TryGetExistingTexture(mat);

                switch (info.type)
                {
                    case MatType.NPC:
                    case MatType.Rock:
                    case MatType.Opaque:
                        ApplyUnlit(mat, existingTex);
                        break;
                    case MatType.Foliage:
                    case MatType.Flowers:
                        ApplyFoliage(mat, existingTex);
                        break;
                    case MatType.Water:
                        ApplyWater(mat);
                        break;
                    case MatType.Bark:
                        ApplyBark(mat, existingTex);
                        break;
                }

                EditorUtility.SetDirty(mat);
                Log($"  OK [{info.type,-10}]  {info.mat.name}");
                ok++;
            }
            catch (System.Exception e)
            {
                Log($"  ERRORE su {info.mat?.name}: {e.Message}");
                fail++;
            }
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Log($"\n=== FIX COMPLETATO ===");
        Log($"Convertiti: {ok}  |  Saltati: {skip}  |  Errori: {fail}");
        if (fail == 0) Log("Tutto fatto senza errori!");
        Repaint();
    }

    void ApplyUnlit(Material mat, Texture existingTex)
    {
        mat.shader = Shader.Find(SHADER_UNLIT);
        AssignTexture(mat, existingTex);
        mat.SetFloat("_Surface", 0f);
        mat.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Geometry;
        mat.SetOverrideTag("RenderType", "Opaque");
        mat.DisableKeyword("_SURFACE_TYPE_TRANSPARENT");
    }

    void ApplyFoliage(Material mat, Texture existingTex)
    {
        // Simple Lit con alpha cutout: evita i quadrati pieni sulle foglie
        mat.shader = Shader.Find(SHADER_SIMPLE_LIT);
        AssignTexture(mat, existingTex);
        mat.SetFloat("_Surface",    1f);
        mat.SetFloat("_AlphaClip",  1f);
        mat.SetFloat("_Cutoff",     0.5f); // abbassa se le foglie spariscono
        mat.SetFloat("_Smoothness", 0f);
        mat.SetFloat("_Metallic",   0f);
        mat.renderQueue = (int)UnityEngine.Rendering.RenderQueue.AlphaTest;
        mat.SetOverrideTag("RenderType", "TransparentCutout");
        mat.EnableKeyword("_ALPHATEST_ON");
    }

    void ApplyWater(Material mat)
    {
        mat.shader = Shader.Find(SHADER_LIT);
        mat.SetColor("_BaseColor",  new Color(0.1f, 0.4f, 0.7f, 0.6f));
        mat.SetFloat("_Surface",    1f);
        mat.SetFloat("_Smoothness", 0.9f);
        mat.SetFloat("_Metallic",   0f);
        mat.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
        mat.SetOverrideTag("RenderType", "Transparent");
        mat.EnableKeyword("_SURFACE_TYPE_TRANSPARENT");
        Log("     NOTA: acqua approssimativa - considera uno shader URP dedicato.");
    }

    void ApplyBark(Material mat, Texture existingTex)
    {
        mat.shader = Shader.Find(SHADER_SIMPLE_LIT);
        AssignTexture(mat, existingTex);
        mat.SetFloat("_Smoothness", 0f);
        mat.SetFloat("_Metallic",   0f);
        mat.SetFloat("_Surface",    0f);
        mat.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Geometry;
        mat.SetOverrideTag("RenderType", "Opaque");
    }

    MatType ClassifyShader(string name)
    {
        if (!name.Contains("Polytope") && !name.Contains("PT_") && !name.StartsWith("Hidden/"))
            return MatType.Unknown;

        string low = name.ToLower();
        if (low.Contains("bark"))      return MatType.Bark;
        if (low.Contains("foliage"))   return MatType.Foliage;
        if (low.Contains("flower"))    return MatType.Flowers;
        if (low.Contains("water"))     return MatType.Water;
        if (low.Contains("rock"))      return MatType.Rock;
        if (low.Contains("npc") || low.Contains("character") || low.Contains("modular")) return MatType.NPC;
        if (low.Contains("opaque"))    return MatType.Opaque;
        if (low.StartsWith("hidden/")) return MatType.Bark;

        return MatType.Opaque;
    }

    void AssignTexture(Material mat, Texture existingTex)
    {
        Texture2D tex = _foundTex ?? existingTex as Texture2D;
        if (tex == null) return;

        mat.SetTexture("_BaseMap", tex);
        mat.SetTexture("_MainTex", tex);

        string path = AssetDatabase.GetAssetPath(tex);
        var imp = AssetImporter.GetAtPath(path) as TextureImporter;
        if (imp != null && imp.filterMode != FilterMode.Point)
        {
            imp.filterMode  = FilterMode.Point;
            imp.sRGBTexture = true;
            imp.SaveAndReimport();
        }
    }

    Texture TryGetExistingTexture(Material mat)
    {
        foreach (var slot in new[] { "_MainTex", "_BaseMap", "_BaseColorMap", "_AlbedoMap", "_DiffuseMap", "_BaseAlbedoMap" })
            if (mat.HasProperty(slot)) { var t = mat.GetTexture(slot); if (t != null) return t; }
        return null;
    }

    Texture2D FindPaletteTexture()
    {
        if (!string.IsNullOrEmpty(_texOverride))
        {
            var t = AssetDatabase.LoadAssetAtPath<Texture2D>(_texOverride);
            if (t != null) return t;
            Log($"ATTENZIONE: Texture manuale non trovata al percorso: {_texOverride}");
        }

        var guids = AssetDatabase.FindAssets("t:Texture2D", new[] { "Assets" });

        // Prima cerca per nome tipico Polytope
        string[] polyNames = { "PT_Texture", "PT_Palette", "PT_Atlas", "PolytopePalette", "PT_Color" };
        foreach (var guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            string file = Path.GetFileNameWithoutExtension(path);
            foreach (var kw in polyNames)
                if (file.IndexOf(kw, System.StringComparison.OrdinalIgnoreCase) >= 0)
                    return AssetDatabase.LoadAssetAtPath<Texture2D>(path);
        }

        // Fallback: qualsiasi texture nella cartella Polytope
        foreach (var guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            if ((path.Contains("Polytope") || path.Contains("PT_")) && path.ToLower().Contains("texture"))
                return AssetDatabase.LoadAssetAtPath<Texture2D>(path);
        }

        return null;
    }

    void BackupMaterial(string path)
    {
        try { if (!File.Exists(path + ".bak")) File.Copy(path, path + ".bak"); }
        catch { }
    }

    void Log(string msg) => _log += msg + "\n";

    class MaterialInfo
    {
        public string   path;
        public Material mat;
        public MatType  type;
    }
}
#endif
