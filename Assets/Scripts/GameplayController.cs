using System;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayController : MonoBehaviour {

    [SerializeField] private Transform playerTransform;

    [SerializeField] private MazeGenerator mazeGenerator;

    [SerializeField] private Transform baseCubeTransform;

    [SerializeField] private PlayerCollideController playerCollideController;

    [SerializeField] private AudioManager audioManager;

    [SerializeField] private MapController mapController;

    [SerializeField] private SkyboxController skyboxController;

    [SerializeField] private Transform xrRootTransform;

    [SerializeField] private HomeMenuController homeMenuController;

    [SerializeField] private NavMeshSurface surface;

    // Start is called before the first frame update
    void Start()
    {

#if UNITY_EDITOR
        xrRootTransform.rotation = Quaternion.identity;
#endif
        mazeGenerator.Initialize();
        mazeGenerator.SetupFloor(baseCubeTransform);
        mazeGenerator.GenerateMaze();
        mazeGenerator.InitializeFinishGO();

        skyboxController.SetupSkybox(mazeGenerator.GetColorScheme());

        playerTransform.position = mazeGenerator.GetInitialPosition();
        playerTransform.rotation = Quaternion.Euler(0f, 45f, 0f);

        playerCollideController.OnPlayerCollideWithFinishLine += OnPlayerCollideWithFinishLine;
        playerCollideController.OnPlayerCollideWithCoin += OnPlayerCollideWithCoin;

        audioManager.SetupBackgroundAudioResource(mazeGenerator.GetColorScheme());
        audioManager.PlayBackgroundMusic();

        Vector2 mazeSize = mazeGenerator.GetMazeSize();
        mapController.AlignMapToMaze(mazeSize.x, 2f, 1f);

        surface.BuildNavMesh();
    }

    private void OnPlayerCollideWithFinishLine(GameObject finishGO)
    {
        Debug.Log("Finish line reached");
        finishGO.SetActive(false);
        audioManager.PlayAudioFinishLine();

        int levelIndex = Preferences.GetLevel();
        int maxUnlockedLevel = Preferences.GetMaxUnlockedLevel();

        if (levelIndex == maxUnlockedLevel)
        {
            int maxLevel = Math.Min(maxUnlockedLevel + 1, Constants.MAX_LEVEL);
            Preferences.SetMaxUnlockedLevel(maxLevel);
        }

        SceneManager.LoadScene(Constants.MENU_SCENE);
    }

    private void OnPlayerCollideWithCoin(GameObject coinGO)
    {
        Debug.Log("Coin reached");
        coinGO.SetActive(false);
        audioManager.PlayAudioCollectCoin();
        int coinCount = Preferences.GetCoins();
        int newCoinCount = coinCount + 1;
        Preferences.SetCoins(newCoinCount);

        homeMenuController.OnPlayerCollideWithCoin(newCoinCount);
    }
}
