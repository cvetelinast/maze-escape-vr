using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayController : MonoBehaviour {

    [SerializeField] private Transform playerTransform;

    [SerializeField] private MazeGenerator mazeGenerator;

    [SerializeField] private Transform baseCubeTransform;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform.position = new Vector3(0f, 0f, 0f);
        playerTransform.rotation = Quaternion.Euler(0f, 45f, 0f);
        mazeGenerator.Initialize();
        mazeGenerator.SetupFloor(baseCubeTransform);
        mazeGenerator.GenerateMaze();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
