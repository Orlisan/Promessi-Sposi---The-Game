using UnityEngine;
using UnityEditor;

public class TilaOggetto : EditorWindow
{
    public GameObject oggetto;
    public int quantita = 5;
    public float distanza = 3f;
    public Vector3 asse = Vector3.right;

    [MenuItem("Tools/Tila Oggetto")]
    public static void ShowWindow()
    {
        GetWindow<TilaOggetto>("Tila Oggetto");
    }

    void OnGUI()
    {
        oggetto = (GameObject)EditorGUILayout.ObjectField("Oggetto", oggetto, typeof(GameObject), true);
        quantita = EditorGUILayout.IntField("Quantità", quantita);
        distanza = EditorGUILayout.FloatField("Distanza", distanza);
        asse = EditorGUILayout.Vector3Field("Asse", asse);

        if(GUILayout.Button("Tila"))
        {
            for(int i = 1; i < quantita; i++)
            {
                GameObject copia = Instantiate(oggetto);
                copia.transform.position = oggetto.transform.position + asse * distanza * i;
            }
        }
    }
}