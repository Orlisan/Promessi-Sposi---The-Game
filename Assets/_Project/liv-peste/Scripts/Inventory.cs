using UnityEngine;
using UnityEngine.InputSystem;
public class Inventory : MonoBehaviour
{//ALPHAAAA--------------ALPHAAAAA
    public GameObject prefab;
    public GameObject dove;
    public bool trovato = false;
    public GameObject istanza;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Keyboard.current.iKey.wasPressedThisFrame) {
            if(!trovato) {
                istanza = Instantiate(prefab, dove.transform);
                istanza.transform.SetAsLastSibling();
            }else{
                Destroy(istanza);
            }
            trovato = !trovato;
        }
    }
}
