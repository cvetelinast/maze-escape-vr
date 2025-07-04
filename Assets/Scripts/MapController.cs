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

    /// <summary>
    /// Методът AlignMapToMaze позиционира камерата на картата спрямо лабиринта, така че да се вижда отгоре
    /// param N - размерът на лабиринта (N x N).
    /// param mazeHeight - височината на лабиринта, която ще се използва за позициониране на камерата
    /// param mazeBaseThickness - дебелината на основата на лабиринта, потъваща под координатната равнина XZ
    public void AlignMapToMaze(float N, float mazeHeight, float mazeBaseThickness)
    {
        float h = Mathf.Sqrt(3f) * (N + 2f) / 2f;

        // Позиция - тъй като лабиринтът се издига на определена височина, а целта ни е да го
        // виждаме отгоре, добавяме към височината стойността на mazeHeight.
        // В противен случай бихме виждали само основата на лабиринта.
        Vector3 position = new Vector3(N / 2f, h + mazeHeight, N / 2f);

        // Пресметнатата матрица на ротация
        Matrix4x4 rotationMatrix = new Matrix4x4
        {
            m00 = 0f, m01 = 0f, m02 = -1f, m03 = 0f,
            m10 = -1f, m11 = 0f, m12 = 0f, m13 = 0f,
            m20 = 0f, m21 = 1f, m22 = 0f, m23 = 0f,
            m30 = 0f, m31 = 0f, m32 = 0f, m33 = 1f
        };

        Quaternion currentCameraRotation = cam.transform.rotation;
        Quaternion newRotation = rotationMatrix.rotation;

        // Задаване на позиция и ротация. Ротациите в Unity се задават чрез кватерниори и са абсолютни,
        // така че трябва да комбинираме текущата ротация с новата.
        cam.transform.SetPositionAndRotation(position, newRotation * currentCameraRotation);

        // Задаване на FOV
        cam.fieldOfView = 60f;
        cam.farClipPlane = h + mazeHeight + mazeBaseThickness;
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
