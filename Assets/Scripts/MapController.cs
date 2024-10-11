using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour {

    [SerializeField] private Transform mapCameraTransform;

    [SerializeField] private Camera camera;

    public void Start()
    {
        camera.enabled = false;
    }

    public void AlignMapToMaze(Vector3 position, float height)
    {
        camera.transform.position = new Vector3(position.x, height, position.z);
    }

    public void ShowMap()
    {
        camera.enabled = true;
    }

    public void HideMap()
    {
        camera.enabled = false;
    }
}
