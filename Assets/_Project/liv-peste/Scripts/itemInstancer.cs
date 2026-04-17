using UnityEngine;

public static class itemInstancer
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    

    // Update is called once per frame
   public static void Istanzia(GameObject obj) { 
        if(!Dati.mainSlot) {
            Object.Instantiate(obj, Dati.MainSlot.transform);
        }else if(!Dati.slot1) {
            Object.Instantiate(obj, Dati.Slot1.transform);
        }else if(!Dati.slot2) {
            Object.Instantiate(obj, Dati.Slot2.transform);
        }else if(!Dati.slot3) {
            Object.Instantiate(obj, Dati.Slot3.transform);
        }else if(!Dati.slot4) {
            Object.Instantiate(obj, Dati.Slot4.transform);
        }else if(!Dati.slot5) {
            Object.Instantiate(obj, Dati.Slot5.transform);
        }
   
    }
}
