using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using TMPro;

public static class ScriptDialogo
{
    public static Canvas nonno = Dati.canvas;
    public static GameObject padre = Dati.sfondoDialogo;
    public static TMP_FontAsset fontDialogo = Dati.fontDialogo;
    
    public static bool isPrinting = false;

    public static async Task stampa(string text){
        if(!isPrinting) {
           await realStampa(text);
        }else{
            while(isPrinting) {
                await Task.Delay(50); //il await è qua
            }
           await realStampa(text);
        }
    }

    public static async Task realStampa(string text) {
            isPrinting = true;
            GameObject istanza = Object.Instantiate(padre, nonno.transform);
            GameObject testoObj = new GameObject("Testo");
            TextMeshProUGUI  testo = testoObj.AddComponent<TextMeshProUGUI>();
            testo.font = fontDialogo;
            testoObj.transform.SetParent(istanza.transform, false);
            testo.color = Color.white;
            testo.fontSize = 5;
            testo.margin = new Vector4(10, 10, 10, 10);
            RectTransform rect = testo.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(padre.GetComponent<RectTransform>().rect.width, padre.GetComponent<RectTransform>().sizeDelta.y);
            char[] charTesto = text.ToCharArray();
            for(int i = 0; i < charTesto.Length; i++) {
                testo.text = testo.text += charTesto[i].ToString();
                await Task.Delay(50);
            }
            await Task.Delay(1000);
            Object.Destroy(testo);
            Object.Destroy(istanza);
            isPrinting = false;
    }

}
