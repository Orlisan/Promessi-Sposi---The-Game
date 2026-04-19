using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ValuesUpdater : MonoBehaviour
{
    public Slider sliderPeste;
    public Slider sliderProvvidenza;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject MainSlot;
    public GameObject slot1;
    public GameObject slot2;
    public GameObject slot3;
    public GameObject slot4;
    public GameObject slot5;

    public GameObject PrefabDialogo;
    public Canvas canvas;
    public TMP_FontAsset fontDialogo;
    void Start()
    {
        Dati.MainSlot = MainSlot;
        Dati.Slot1 = slot1;
        Dati.Slot2 = slot2;
        Dati.Slot3 = slot3;
        Dati.Slot4 = slot4;
        Dati.Slot5 = slot5;
        Dati.sfondoDialogo = PrefabDialogo;
        Dati.canvas = canvas;
        Dati.fontDialogo = fontDialogo;
    }

    // Update is called once per frame
    void Update()
    {
        sliderPeste.value = Dati.livelloPeste;
        sliderProvvidenza.value = Dati.livelloProvvidenza;
        //CHI GUARDERÀ QUESTO CODICE SAPPIA CHE SAPEVO POTESSI CICLARE E ITERARE, MA NON AVEVO VOGLIA
        if(MainSlot.transform.childCount > 0) {
            Dati.mainSlot = true;
        }else {
            Dati.mainSlot = false;
        }
        if(slot1.transform.childCount > 0)
        {
            Dati.slot1 = true;
        }
        else
        {
            Dati.slot1 = false;
        }
        if(slot2.transform.childCount > 0)
        {
            Dati.slot2 = true;
        }
        else
        {
            Dati.slot2 = false;
        }
        if(slot3.transform.childCount > 0)
        {
            Dati.slot3 = true;
        }
        else
        {
            Dati.slot3 = false;
        }
        if(slot4.transform.childCount > 0)
        {
            Dati.slot4 = true;
        }
        else
        {
            Dati.slot4 = false;
        }
        if(slot5.transform.childCount > 0)
        {
            Dati.slot5 = true;
        }
        else
        {
            Dati.slot5 = false;
        }
    }
}

