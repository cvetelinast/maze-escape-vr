using UnityEngine;

public class MainController : MonoBehaviour {

    [SerializeField] private Material skyboxMaterial;

    void Start()
    {
        RenderSettings.skybox = skyboxMaterial;
    }
}

