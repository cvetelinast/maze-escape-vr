using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WatchController : MonoBehaviour {

    [SerializeField] private Button settingsButton;

    [SerializeField] private Button mapButton;

    [SerializeField] private GameObject settingsPanelGO;

    [SerializeField] private GameObject mapPanelGO;

    [SerializeField] private Button closeButtonSettings;

    [SerializeField] private Button closeButtonMap;

    [SerializeField] private MapController mapController;

    private IEnumerator mapPanelTimerCoroutine;

    private void Start()
    {
        settingsButton.onClick.AddListener(() => OnButtonClicked(true));
        mapButton.onClick.AddListener(() => OnButtonClicked(false));

        ToggleShowMapPanel(false, mapPanelGO);
        ToggleShowSettingsPanel(false, settingsPanelGO);

        closeButtonSettings.onClick.AddListener(() => OnCloseButtonClicked(true));
        closeButtonMap.onClick.AddListener(() => OnCloseButtonClicked(false));

        mapController.HideMap();
    }

    private void OnDestroy()
    {
        settingsButton.onClick.RemoveAllListeners();
        mapButton.onClick.RemoveAllListeners();
    }

    private void OnButtonClicked(bool isSettingsPanel)
    {
        if (isSettingsPanel)
        {
            bool newVisibility = !settingsPanelGO.activeSelf;
            ToggleShowSettingsPanel(newVisibility, settingsPanelGO);
            ToggleShowMapPanel(false, mapPanelGO);

            mapController.HideMap();
        }
        else
        {
            bool newVisibility = !mapPanelGO.activeSelf;
            ToggleShowSettingsPanel(false, settingsPanelGO);
            ToggleShowMapPanel(newVisibility, mapPanelGO);

            mapController.ShowMap();
        }
    }

    private void ToggleShowSettingsPanel(bool shouldShow, GameObject panelGO)
    {
        if (panelGO.activeSelf != shouldShow)
        {
            panelGO.SetActive(shouldShow);
        }
    }

    private void ToggleShowMapPanel(bool shouldShow, GameObject panelGO)
    {
        if (shouldShow && Preferences.GetCoins() <= 2)
        {
            Debug.LogWarning("Not enough coins to show map panel.");
            return;
        }

        if (shouldShow)
        {
            int coinCount = Preferences.GetCoins();
            int newCoinCount = coinCount - 3;
            Preferences.SetCoins(newCoinCount);

            // Start timer
            if (mapPanelTimerCoroutine != null)
            {
                StopCoroutine(mapPanelTimerCoroutine);
            }
            mapPanelTimerCoroutine = MapPanelTimerCoroutine();
            StartCoroutine(mapPanelTimerCoroutine);
        }
        else
        {
            // Stop timer if hiding
            if (mapPanelTimerCoroutine != null)
            {
                StopCoroutine(mapPanelTimerCoroutine);
                mapPanelTimerCoroutine = null;
            }
        }

        if (panelGO.activeSelf != shouldShow)
        {
            panelGO.SetActive(shouldShow);
        }
    }

    private IEnumerator MapPanelTimerCoroutine()
    {
        yield return new WaitForSeconds(15f);
        ToggleShowMapPanel(false, mapPanelGO);
    }

    private void OnCloseButtonClicked(bool isSettingsPanel)
    {
        if (isSettingsPanel)
        {
            ToggleShowSettingsPanel(false, settingsPanelGO);
        }
        else
        {
            ToggleShowMapPanel(false, mapPanelGO);
        }
    }
}
