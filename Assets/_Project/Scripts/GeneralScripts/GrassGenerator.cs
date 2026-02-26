using UnityEngine;

public class GrassGenerator : MonoBehaviour
{
    public GameObject prefabErba;
public int quantita = 100;
public float areaX = 50f;
public float areaZ = 50f;

void Start()
{
    for (int i = 0; i < quantita; i++)
    {
        float x = Random.Range(-areaX, areaX);
        float z = Random.Range(-areaZ, areaZ);
        
        Instantiate(prefabErba, new Vector3(x, 0, z), Quaternion.identity);
    }
}
}
