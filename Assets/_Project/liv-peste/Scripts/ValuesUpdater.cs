using UnityEngine;
using UnityEngine.UI;

public class ValuesUpdater : MonoBehaviour
{
    public Slider sliderPeste;
    public Slider sliderProvvidenza;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        sliderPeste.value = Dati.livelloPeste;
        sliderProvvidenza.value = Dati.livelloProvvidenza;

    }
}
