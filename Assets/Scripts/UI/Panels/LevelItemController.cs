using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static ColorsGenerator;

public class LevelItemController : MonoBehaviour {

    [SerializeField] private Image image;

    [SerializeField] RectTransform grayOverlayRectTransform;

    [SerializeField] private int levelIndex;

    [SerializeField] private TextMeshProUGUI title;

    [SerializeField] private Button playLevelButton;

    private MazeColorScheme colorScheme;

    bool isLevelUnlocked = false;

    // Start is called before the first frame update
    void Start()
    {
        colorScheme = GetMazeColorSchemeForLevel(levelIndex);

        Color color = CalculateBaseColor(colorScheme);
        image.color = color;

        string titleText = "Level " + levelIndex;
        title.text = titleText;

        isLevelUnlocked = levelIndex <= Preferences.GetMaxUnlockedLevel();

        if (!isLevelUnlocked)
        {
            grayOverlayRectTransform.gameObject.SetActive(true);
        }
        else
        {
            playLevelButton.onClick.AddListener(() =>
            {
                Preferences.SetLevel(levelIndex);
                SceneManager.LoadScene(Constants.GAMEPLAY_SCENE);
            });
        }
    }
}
