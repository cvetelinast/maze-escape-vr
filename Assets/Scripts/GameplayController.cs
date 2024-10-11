using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayController : MonoBehaviour {

    [SerializeField] private Transform playerTransform;

    [SerializeField] private MazeGenerator mazeGenerator;

    [SerializeField] private Transform baseCubeTransform;

    [SerializeField] private PlayerCollideController playerCollideController;

    [SerializeField] private AudioManager audioManager;

    [SerializeField] private MapController mapController;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform.position = new Vector3(0f, 0f, 0f);
        playerTransform.rotation = Quaternion.Euler(0f, 45f, 0f);
        mazeGenerator.Initialize();
        mazeGenerator.SetupFloor(baseCubeTransform);
        mazeGenerator.GenerateMaze();
        mazeGenerator.InitializeFinishGO();

        playerCollideController.OnPlayerCollideWithFinishLine += OnPlayerCollideWithFinishLine;
        playerCollideController.OnPlayerCollideWithCoin += OnPlayerCollideWithCoin;

        audioManager.PlayBackgroundMusic();

        mapController.AlignMapToMaze(mazeGenerator.center, mazeGenerator.GetLargestDimension());
    }

    private void OnPlayerCollideWithFinishLine(GameObject finishGO)
    {
        Debug.Log("Finish line reached");
        finishGO.SetActive(false);
        audioManager.PlayAudioFinishLine();
    }

    private void OnPlayerCollideWithCoin(GameObject coinGO)
    {
        Debug.Log("Coin reached");
        coinGO.SetActive(false);
        audioManager.PlayAudioCollectCoin();

    }
}
