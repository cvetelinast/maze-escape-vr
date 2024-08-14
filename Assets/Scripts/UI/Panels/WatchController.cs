using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WatchController : MonoBehaviour {

    [SerializeField] private Button settingsButton;

    [SerializeField] private Button mapButton;

    [SerializeField] private GameObject settingsPanelGO;

    [SerializeField] private GameObject mapPanelGO;

    [SerializeField] private Button closeButtonSettings;

    [SerializeField] private Button closeButtonMap;

    private void Start()
    {
        settingsButton.onClick.AddListener(() => OnButtonClicked(true));
        mapButton.onClick.AddListener(() => OnButtonClicked(false));

        ToggleShowPanel(false, mapPanelGO);
        ToggleShowPanel(false, settingsPanelGO);

        closeButtonSettings.onClick.AddListener(() => OnCloseButtonClicked(true));
        closeButtonMap.onClick.AddListener(() => OnCloseButtonClicked(false));
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
            ToggleShowPanel(false, mapPanelGO);
            ToggleShowPanel(newVisibility, settingsPanelGO);
        }
        else
        {
            bool newVisibility = !mapPanelGO.activeSelf;
            ToggleShowPanel(false, settingsPanelGO);
            ToggleShowPanel(newVisibility, mapPanelGO);
        }
    }

    private void ToggleShowPanel(bool shouldShow, GameObject panelGO)
    {
        if (panelGO.activeSelf != shouldShow)
        {
            panelGO.SetActive(shouldShow);
        }
    }

    private void OnCloseButtonClicked(bool isSettingsPanel)
    {
        if (isSettingsPanel)
        {
            ToggleShowPanel(false, settingsPanelGO);
        }
        else
        {
            ToggleShowPanel(false, mapPanelGO);
        }
    }
}
