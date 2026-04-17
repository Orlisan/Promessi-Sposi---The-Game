using UnityEngine;
using UnityEditor;
using UnityEditor.Rendering.Universal;
using System.IO;

public class ConvertMaterials : EditorWindow
{
    [MenuItem("Tools/Converti Materiali in URP")]
    public static void ConvertAll()
    {
        string[] guids = AssetDatabase.FindAssets("t:Material", new[] { "Assets" });
        int convertiti = 0;
        int saltati = 0;

        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            Material mat = AssetDatabase.LoadAssetAtPath<Material>(path);

            if (mat == null) continue;

            // Salta materiali già in URP
            if (mat.shader.name.StartsWith("Universal Render Pipeline"))
            {
                saltati++;
                continue;
            }

            // Salta shader custom (non Standard)
            if (mat.shader.name != "Standard" && 
                mat.shader.name != "Standard (Specular setup)" &&
                !mat.shader.name.StartsWith("Legacy"))
            {
                Debug.LogWarning($"SALTATO (shader custom): {path} - Shader: {mat.shader.name}");
                saltati++;
                continue;
            }

            // Salva texture prima della conversione
            Texture albedo = mat.GetTexture("_MainTex");
            Texture normal = mat.GetTexture("_BumpMap");
            Texture metallic = mat.GetTexture("_MetallicGlossMap");
            Texture occlusion = mat.GetTexture("_OcclusionMap");
            Color color = mat.GetColor("_Color");
            float metallicVal = mat.GetFloat("_Metallic");
            float smoothness = mat.GetFloat("_Glossiness");

            // Converti a URP/Lit
            mat.shader = Shader.Find("Universal Render Pipeline/Lit");

            // Riassegna texture
            if (albedo) mat.SetTexture("_BaseMap", albedo);
            if (normal) mat.SetTexture("_BumpMap", normal);
            if (metallic) mat.SetTexture("_MetallicGlossMap", metallic);
            if (occlusion) mat.SetTexture("_OcclusionMap", occlusion);
            mat.SetColor("_BaseColor", color);
            mat.SetFloat("_Metallic", metallicVal);
            mat.SetFloat("_Smoothness", smoothness);

            EditorUtility.SetDirty(mat);
            convertiti++;
            Debug.Log($"Convertito: {path}");
        }

        AssetDatabase.SaveAssets();
        Debug.Log($"Fatto! Convertiti: {convertiti}, Saltati: {saltati}");
    }
}