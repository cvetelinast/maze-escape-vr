using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeMenuController : MonoBehaviour {

    [SerializeField] private Button playButton;

    [SerializeField] private bool showAllLevelsButton = true;

    [SerializeField] private Button allLevelsButton;

    [SerializeField] private AllLevelsScreenController allLevelsScreenController;

    [SerializeField] private TextMeshProUGUI levelText;

    [SerializeField] private TextMeshProUGUI coinsCountText;

    // Start is called before the first frame update
    void Start()
    {
        playButton.onClick.AddListener(() =>
        {
            Preferences.SetLevel(Preferences.GetMaxUnlockedLevel());
            SceneManager.LoadScene(Constants.GAMEPLAY_SCENE);
        });

        if (showAllLevelsButton)
        {
            allLevelsButton.onClick.AddListener(() =>
            {
                allLevelsScreenController.Show();
            });
        }

        LoadPreferences();
    }

    private void OnDestroy()
    {
        playButton.onClick.RemoveAllListeners();
        if (showAllLevelsButton)
        {
            allLevelsButton.onClick.RemoveAllListeners();
        }
    }

    private void LoadPreferences()
    {
        int maxLevel = Preferences.GetMaxUnlockedLevel();
        levelText.text = "Level " + maxLevel;

        int coins = Preferences.GetCoins();
        coinsCountText.text = coins.ToString();
    }
}
