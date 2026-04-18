using UnityEngine;
using UnityEngine.UI;
using System;
public class MainSlotChecker : MonoBehaviour
{
    public GameObject[] modelli;
    public GameObject[] correlatiItemNellInventario;
    public GameObject MainSlot;
    public GameObject firstSlot;
    public GameObject impugnatura;
    public GameObject cintura;

    private GameObject vecchioObject;
    private GameObject vecchioObject2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        vecchioObject = new GameObject();
        vecchioObject.name = "Ciao";
        vecchioObject2= new GameObject();
        vecchioObject2.name = "Ciao";
    }

    // Update is called once per frame
    void Update()
    {
        if(MainSlot.transform.childCount > 0)
        {
    GameObject childInSlot = MainSlot.transform.GetChild(0).gameObject;
    bool itemTrovato = false;
    
        foreach(GameObject item in correlatiItemNellInventario)
            {
            if(childInSlot.name.StartsWith(item.name) && childInSlot.name != vecchioObject.name)
                {
                    int indice = System.Array.IndexOf(correlatiItemNellInventario, item);
                        if(impugnatura.transform.childCount > 0)
                        {
                            Destroy(impugnatura.transform.GetChild(0).gameObject);
                        }
                    if(modelli[indice] != null) {
                        GameObject obj = Instantiate(modelli[indice], impugnatura.transform);
                        obj.transform.localPosition = Vector3.zero;
                    }else {
                        GameObject obj = new GameObject("SpriteMondiale");
                        obj.transform.SetParent(impugnatura.transform);
                        SpriteRenderer sr = obj.AddComponent<SpriteRenderer>();
                        sr.sprite = childInSlot.GetComponent<Image>().sprite;
                        obj.transform.localPosition = new Vector3(0.1f, 0.05f, 0.05f);
                        obj.transform.localRotation = Quaternion.Euler(0f, 0f, -135f);
                        obj.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);}
                    vecchioObject.name = childInSlot.name;
                    itemTrovato = true;
                    break;

                }if(childInSlot.name.StartsWith(item.name) && childInSlot.name == vecchioObject.name) {
                    itemTrovato = true;
                }
                }
            
            if(itemTrovato == false) {
                throw new Exception("L'item attualmente selezionato non è presente nell'elenco degli item del livello corrente");
            }
        
        
        }else
        {
            if(impugnatura.transform.childCount > 0)
                        {
                            Destroy(impugnatura.transform.GetChild(0).gameObject);
                        }
            vecchioObject.name = "Ciao";
        }

        if(firstSlot.transform.childCount > 0)
        {
    GameObject childInSlot = firstSlot.transform.GetChild(0).gameObject;
    bool itemTrovato = false;
    
        foreach(GameObject item in correlatiItemNellInventario)
            {
            if(childInSlot.name.StartsWith(item.name) && childInSlot.name != vecchioObject2.name)
                {
                    int indice = System.Array.IndexOf(correlatiItemNellInventario, item);
                        if(cintura.transform.childCount > 0)
                        {
                            Destroy(cintura.transform.GetChild(0).gameObject);
                        }
                    if(modelli[indice] != null) {
                        GameObject obj = Instantiate(modelli[indice], cintura.transform);
                        obj.transform.localPosition = Vector3.zero;
                    }else {
                        GameObject obj = new GameObject("SpriteMondiale");
                        obj.transform.SetParent(cintura.transform);
                        SpriteRenderer sr = obj.AddComponent<SpriteRenderer>();
                        sr.sprite = childInSlot.GetComponent<Image>().sprite;
                        obj.transform.localPosition = new Vector3(0.01f, 0.05f, 0.01f);
                        obj.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                        obj.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);}
                    vecchioObject2.name = childInSlot.name;
                    itemTrovato = true;
                    break;

                }if(childInSlot.name.StartsWith(item.name) && childInSlot.name == vecchioObject2.name) {
                    itemTrovato = true;
                }
                }
            
            if(itemTrovato == false) {
                throw new Exception("L'item attualmente selezionato non è presente nell'elenco degli item del livello corrente");
            }
        
        
        }else
        {
            if(cintura.transform.childCount > 0)
                        {
                            Destroy(cintura.transform.GetChild(0).gameObject);
                        }
            vecchioObject2.name = "Ciao";
        }
    }
}