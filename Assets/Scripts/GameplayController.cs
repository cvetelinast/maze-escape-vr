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
        playerTransform.position = new Vector3(0f, 0f, 0f);
        playerTransform.rotation = Quaternion.Euler(0f, 45f, 0f);

#if UNITY_EDITOR
        xrRootTransform.rotation = Quaternion.identity;
#endif
        mazeGenerator.Initialize();
        mazeGenerator.SetupFloor(baseCubeTransform);
        mazeGenerator.GenerateMaze();
        mazeGenerator.InitializeFinishGO();

        skyboxController.SetupSkybox(mazeGenerator.GetColorScheme());

        playerCollideController.OnPlayerCollideWithFinishLine += OnPlayerCollideWithFinishLine;
        playerCollideController.OnPlayerCollideWithCoin += OnPlayerCollideWithCoin;

        audioManager.SetupBackgroundAudioResource(mazeGenerator.GetColorScheme());
        audioManager.PlayBackgroundMusic();

        mapController.AlignMapToMaze(mazeGenerator.center, mazeGenerator.GetLargestDimension());

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
            Preferences.SetMaxUnlockedLevel(maxUnlockedLevel + 1);
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
