using UnityEngine;
using UnityEngine.Rendering;

public class MainController : MonoBehaviour {

    [SerializeField] private Material skyboxMaterial;

    void Start()
    {
        RenderSettings.skybox = skyboxMaterial;
        //GraphicsSettings.useScriptableRenderPipelineBatching = false;
    }
}

