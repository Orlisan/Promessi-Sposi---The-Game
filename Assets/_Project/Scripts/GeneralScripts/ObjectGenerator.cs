using UnityEngine;

public class ObjectGenerator : MonoBehaviour
{
    public GameObject[] prefabErba;
public int[] quantita;
public float areaX = 50f;
public float areaZ = 50f;

void Start()
{
    for (int i = 0; i < prefabErba.Length; i++)
    {
        for (int j = 0; j < quantita[i]; j++)
        {
            float x = Random.Range(-areaX, areaX);
            float z = Random.Range(-areaZ, areaZ);
            Instantiate(prefabErba[i], new Vector3(x, 0, z), prefabErba[i].transform.rotation);
        }
    }
}
}