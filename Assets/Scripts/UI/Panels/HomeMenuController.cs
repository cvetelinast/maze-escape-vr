using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeMenuController : MonoBehaviour {

    [SerializeField] private Button playButton;

    [SerializeField] private Button allLevelsButton;

    [SerializeField] private Button exitButton;

    [SerializeField] private AllLevelsScreenController allLevelsScreenController;

    [SerializeField] private TextMeshProUGUI levelText;

    [SerializeField] private TextMeshProUGUI coinsCountText;

    private void Start()
    {
        playButton?.onClick.AddListener(() =>
        {
            Preferences.SetLevel(Preferences.GetMaxUnlockedLevel());
            SceneManager.LoadScene(Constants.GAMEPLAY_SCENE);
        });

        allLevelsButton?.onClick.AddListener(() =>
        {
            allLevelsScreenController?.Show();
        });

        exitButton?.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(Constants.MENU_SCENE);
        });

        LoadPreferences();
    }

    private void OnDestroy()
    {
        playButton?.onClick.RemoveAllListeners();
        allLevelsButton?.onClick.RemoveAllListeners();
        exitButton?.onClick.RemoveAllListeners();
    }

    public void OnPlayerCollideWithCoin(int coinCount)
    {
        coinsCountText.text = coinCount.ToString();
    }

    private void LoadPreferences()
    {
        int maxLevel = Preferences.GetMaxUnlockedLevel();
        levelText.text = "Level " + maxLevel;

        int coins = Preferences.GetCoins();
        coinsCountText.text = coins.ToString();
    }
}
