using UnityEngine;
using UnityEngine.UI;

public class MapController : MonoBehaviour {

    [SerializeField] private Transform playerTransform;

    [SerializeField] private Transform mapCameraTransform;

    [SerializeField] private Camera cam;

    [SerializeField] private RawImage mapImage;

    [SerializeField] private RectTransform renderTextureRect;

    private static readonly int playerPositionPropId = Shader.PropertyToID("_PlayerPosition");

    public void Start()
    {
        //cam.enabled = false;
        Material circleMaterial = new Material(Shader.Find("Unlit/GameMapShader"));
        mapImage.material = circleMaterial;
        UpdatePlayerPosition();
    }

    public void Update()
    {
        if (cam.enabled)
        {
            UpdatePlayerPosition();
        }
    }

    public void AlignMapToMaze(Vector3 position, float height)
    {
        cam.transform.position = new Vector3(position.x, height, position.z);
    }

    public void ShowMap()
    {
        Debug.Log("Show map");
        cam.enabled = true;
        mapImage.gameObject.SetActive(true);
    }

    public void HideMap()
    {
        Debug.Log("Hide map");
        cam.enabled = false;
        mapImage.gameObject.SetActive(false);
    }

    public void UpdatePlayerPosition()
    {
        Vector3 playerWorldPosition = playerTransform.position;
        Vector2 viewportPosition = cam.WorldToViewportPoint(playerWorldPosition);
        mapImage.material.SetVector(playerPositionPropId,
           new Vector4(viewportPosition.x, viewportPosition.y, 0, 0));
    }
}
