using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeMenuController : MonoBehaviour {

    [SerializeField] private Button playButton;

    // Start is called before the first frame update
    void Start()
    {
        playButton.onClick.AddListener(() =>
        {
            UniduxMaze.Dispatch(new OnPlayButtonClicked());
            SceneManager.LoadScene("GameplayScene");
        });

        LoadPreferences();
    }

    private void OnDestroy()
    {
        playButton.onClick.RemoveAllListeners();
    }

    private void LoadPreferences()
    {
        Debug.Log("LoadPreferences ... ");
    }

}
