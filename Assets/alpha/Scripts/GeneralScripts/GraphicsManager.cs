using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GraphicsManager : MonoBehaviour
{
    void Start()
    {
        var urpAsset = QualitySettings.renderPipeline as UniversalRenderPipelineAsset;
        if (urpAsset != null)
        {
            urpAsset.msaaSampleCount = 2; 
        }
        else
        {
            Debug.LogError("URP Asset non trovato!");
        }
    }
}
