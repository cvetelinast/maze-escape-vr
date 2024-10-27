using UnityEngine;
using UnityEngine.UI;

public class MapController : MonoBehaviour {

    [SerializeField] private Transform playerTransform;

    [SerializeField] private Transform mapCameraTransform;

    [SerializeField] private Camera camera;

    [SerializeField] private RawImage mapImage;

    [SerializeField] private RectTransform renderTextureRect;

    private static readonly int playerPositionPropId = Shader.PropertyToID("_PlayerPosition");

    public void Start()
    {
        camera.enabled = false;
        Material circleMaterial = new Material(Shader.Find("Unlit/GameMapShader"));
        mapImage.material = circleMaterial;
        UpdatePlayerPosition();
    }

    public void Update()
    {
        UpdatePlayerPosition();
    }

    public void AlignMapToMaze(Vector3 position, float height)
    {
        camera.transform.position = new Vector3(position.x, height, position.z);
    }

    public void ShowMap()
    {
        camera.enabled = true;
        mapImage.gameObject.SetActive(true);
    }

    public void HideMap()
    {
        camera.enabled = false;
        mapImage.gameObject.SetActive(false);
    }

    public void UpdatePlayerPosition()
    {
        Vector3 playerWorldPosition = playerTransform.position;
        Vector2 viewportPosition = camera.WorldToViewportPoint(playerWorldPosition);
        mapImage.material.SetVector(playerPositionPropId,
           new Vector4(viewportPosition.x, viewportPosition.y, 0, 0));
    }
}
