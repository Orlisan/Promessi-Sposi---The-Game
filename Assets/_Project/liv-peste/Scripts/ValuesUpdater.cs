using UnityEngine;
using UnityEngine.UI;

public class ValuesUpdater : MonoBehaviour
{
    public Slider sliderPeste;
    public Slider sliderProvvidenza;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject slot1;
    public GameObject slot2;
    public GameObject slot3;
    public GameObject slot4;
    public GameObject slot5;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        sliderPeste.value = Dati.livelloPeste;
        sliderProvvidenza.value = Dati.livelloProvvidenza;
        //CHI GUARDERÀ QUESTO CODICE SAPPIA CHE SAPEVO POTESSI CICLARE E ITERARE, MA NON AVEVO VOGLIA
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

