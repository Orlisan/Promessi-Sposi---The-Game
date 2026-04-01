using UnityEngine;

public class MainSlotChecker : MonoBehaviour
{
    public GameObject[] modelli;
    public GameObject[] correlatiItemNellInventario;
    public GameObject MainSlot;
    public GameObject impugnatura;

    private GameObject vecchioObject;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(MainSlot.transform.childCount > 0)
        {
            foreach(GameObject item in correlatiItemNellInventario)
            {
                if(item == MainSlot.transform.GetChild(0).gameObject && item != vecchioObject)
                {
                    int indice = System.Array.IndexOf(correlatiItemNellInventario, item);
                    if(impugnatura.transform.childCount > 0)
                    {
                        Destroy(impugnatura.transform.GetChild(0).gameObject);
                    }
                    
                    Instantiate(modelli[indice], impugnatura.transform);
                    vecchioObject = item;
                    break;

                }
            }
        }
    }
}
