using UnityEngine;
using System.Collections;
public class CicloGiornoNotte : MonoBehaviour
{

    public Light luce;

    public Material giorno;
    public Material notte;
    public Material alba;
    public Material tramonto;

    public float rotazione = 0f;
    public bool èGiorno = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RenderSettings.skybox = giorno;
        RenderSettings.skybox = Instantiate(RenderSettings.skybox);
        StartCoroutine(Ciclo());
    }
    IEnumerator Ciclo() {
        while(true) {
            yield return new WaitForSeconds(0.1f);
            luce.transform.Rotate(Vector3.right, 0.1f);
            RenderSettings.skybox.SetFloat("_Rotation", RenderSettings.skybox.GetFloat("_Rotation") + 0.1f);
            rotazione += 0.1f;
            if(rotazione >= 75f && rotazione <= 76f) {
                if(èGiorno) {
                    RenderSettings.skybox = tramonto;
                    luce.intensity = 0.5f;
                }else {
                    RenderSettings.skybox = alba;
                    luce.intensity = 0.5f;
                }
                RenderSettings.skybox = Instantiate(RenderSettings.skybox);
                 DynamicGI.UpdateEnvironment();
            }
            if(rotazione >= 90 && rotazione <= 91) {
                èGiorno = !èGiorno;
                if(!èGiorno) {
                    RenderSettings.skybox = notte;
                    luce.intensity = 0.2f;
                    RenderSettings.skybox.SetFloat("_Rotation", 0f);
                }else{
                    RenderSettings.skybox = giorno;
                    luce.intensity = 1.0f;
                    RenderSettings.skybox.SetFloat("_Rotation", 0f);
                }
                rotazione = 0f;
                RenderSettings.skybox = Instantiate(RenderSettings.skybox);
                DynamicGI.UpdateEnvironment();
            }
            
        }
       
    }
   
}
