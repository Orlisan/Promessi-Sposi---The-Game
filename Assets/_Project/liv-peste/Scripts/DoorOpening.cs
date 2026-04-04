using UnityEngine;
using System.Collections;

public class DoorOpening : MonoBehaviour
{
    public GameObject player;
    public float distanzaMassima;
    bool statoApertura = false;
    float rotazioneIniziale;
    bool èInApertura = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   void Awake()
    {
      rotazioneIniziale = transform.eulerAngles.z;
    }
    void OnMouseDown()
    {
        if(!èInApertura && Mathf.Abs(transform.position.x - player.transform.position.x) <= distanzaMassima && Mathf.Abs(transform.position.z - player.transform.position.z) <= distanzaMassima)
        {
             StartCoroutine(aprichiudi());
        }
       
    }
   
    IEnumerator aprichiudi()
    {
        èInApertura = true;
        long counter = 0;
        while(counter != 90)
        {
            yield return new WaitForSeconds(0.005f);
            if(statoApertura)
            {
                transform.Rotate(0,0,-1);
            }else
            {
                transform.Rotate(0, 0, 1);
                
            }
            counter++;
        }
        if(statoApertura && transform.eulerAngles.z != rotazioneIniziale)
        {
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, rotazioneIniziale);
        }
      
        statoApertura = !statoApertura;
        èInApertura = false;
    }
}
