using UnityEngine;
using UnityEngine.InputSystem;
public class Inventory : MonoBehaviour
{
    public GameObject dove;
    public GameObject istanza;

    public GameObject slot1;
    private GameObject figlio1;
    public GameObject slot2;
    private GameObject figlio2;
    public GameObject slot3;
    private GameObject figlio3;
    public GameObject slot4;
    private GameObject figlio4;
    public GameObject slot5;
    private GameObject figlio5;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()

    {
        
        if(Keyboard.current.digit1Key.wasPressedThisFrame || Keyboard.current.numpad1Key.wasPressedThisFrame) {
            if(slot1.transform.childCount > 0)
            {
                figlio1 = slot1.transform.GetChild(0).gameObject;
            }
            
            if(slot1.transform.childCount == 0 && dove.transform.childCount == 1)
            {
                dove.transform.GetChild(0).transform.SetParent(slot1.transform);
                slot1.transform.GetChild(0).localPosition = Vector3.zero;
            }else if(slot1.transform.childCount == 1 && dove.transform.childCount == 1)
            {
                dove.transform.GetChild(0).transform.SetParent(slot1.transform);
                figlio1.transform.SetParent(dove.transform);
                slot1.transform.GetChild(0).localPosition = Vector3.zero;
                figlio1.transform.localPosition = Vector3.zero;
            }else if(slot1.transform.childCount == 1 && dove.transform.childCount == 0)
            {
                figlio1.transform.SetParent(dove.transform);
                figlio1.transform.localPosition = Vector3.zero;
            }
        }
        if(Keyboard.current.digit2Key.wasPressedThisFrame || Keyboard.current.numpad2Key.wasPressedThisFrame) {
            if(slot2.transform.childCount > 0)
            {
                figlio2 = slot2.transform.GetChild(0).gameObject;
            }
            
            if(slot2.transform.childCount == 0 && dove.transform.childCount == 1)
            {
                dove.transform.GetChild(0).transform.SetParent(slot2.transform);
                slot2.transform.GetChild(0).localPosition = Vector3.zero;
            }else if(slot2.transform.childCount == 1 && dove.transform.childCount == 1)
            {
                dove.transform.GetChild(0).transform.SetParent(slot2.transform);
                figlio2.transform.SetParent(dove.transform);
                slot2.transform.GetChild(0).localPosition = Vector3.zero;
                figlio2.transform.localPosition = Vector3.zero;
            }else if(slot2.transform.childCount == 1 && dove.transform.childCount == 0)
            {
                figlio2.transform.SetParent(dove.transform);
                figlio2.transform.localPosition = Vector3.zero;
            }
        }
        if(Keyboard.current.digit3Key.wasPressedThisFrame || Keyboard.current.numpad3Key.wasPressedThisFrame) {
            if(slot3.transform.childCount > 0)
            {
                figlio3 = slot3.transform.GetChild(0).gameObject;
            }
            
            if(slot3.transform.childCount == 0 && dove.transform.childCount == 1)
            {
                dove.transform.GetChild(0).transform.SetParent(slot3.transform);
                slot3.transform.GetChild(0).localPosition = Vector3.zero;
            }else if(slot3.transform.childCount == 1 && dove.transform.childCount == 1)
            {
                dove.transform.GetChild(0).transform.SetParent(slot3.transform);
                figlio3.transform.SetParent(dove.transform);
                slot3.transform.GetChild(0).localPosition = Vector3.zero;
                figlio3.transform.localPosition = Vector3.zero;
            }else if(slot3.transform.childCount == 1 && dove.transform.childCount == 0)
            {
                figlio3.transform.SetParent(dove.transform);
                figlio3.transform.localPosition = Vector3.zero;
            }
        }
        if(Keyboard.current.digit4Key.wasPressedThisFrame || Keyboard.current.numpad4Key.wasPressedThisFrame) {
            if(slot4.transform.childCount > 0)
            {
                figlio4 = slot4.transform.GetChild(0).gameObject;
            }
            
            if(slot4.transform.childCount == 0 && dove.transform.childCount == 1)
            {
                dove.transform.GetChild(0).transform.SetParent(slot4.transform);
                slot4.transform.GetChild(0).localPosition = Vector3.zero;
            }else if(slot4.transform.childCount == 1 && dove.transform.childCount == 1)
            {
                dove.transform.GetChild(0).transform.SetParent(slot4.transform);
                figlio4.transform.SetParent(dove.transform);
                slot4.transform.GetChild(0).localPosition = Vector3.zero;
                figlio4.transform.localPosition = Vector3.zero;
            }else if(slot4.transform.childCount == 1 && dove.transform.childCount == 0)
            {
                figlio4.transform.SetParent(dove.transform);
                figlio4.transform.localPosition = Vector3.zero;
            }
        }
        if(Keyboard.current.digit5Key.wasPressedThisFrame || Keyboard.current.numpad5Key.wasPressedThisFrame) {
            if(slot5.transform.childCount > 0)
            {
                figlio5 = slot5.transform.GetChild(0).gameObject;
            }
            
            if(slot5.transform.childCount == 0 && dove.transform.childCount == 1)
            {
                dove.transform.GetChild(0).transform.SetParent(slot5.transform);
                slot5.transform.GetChild(0).localPosition = Vector3.zero;
            }else if(slot5.transform.childCount == 1 && dove.transform.childCount == 1)
            {
                dove.transform.GetChild(0).transform.SetParent(slot5.transform);
                figlio5.transform.SetParent(dove.transform);
                slot5.transform.GetChild(0).localPosition = Vector3.zero;
                figlio5.transform.localPosition = Vector3.zero;
            }else if(slot5.transform.childCount == 1 && dove.transform.childCount == 0)
            {
                figlio5.transform.SetParent(dove.transform);
                figlio5.transform.localPosition = Vector3.zero;
            }
        }
    }
}
