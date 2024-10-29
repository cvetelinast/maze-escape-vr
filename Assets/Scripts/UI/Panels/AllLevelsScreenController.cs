using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllLevelsScreenController : MonoBehaviour {

    [SerializeField] private RectTransform allLevelsRectTransform;

    [SerializeField] private Button closeButton;

    private void Start()
    {
        closeButton.onClick.AddListener(() =>
        {
            Hide();
        });
    }

    private void OnDestroy()
    {
        closeButton.onClick.RemoveAllListeners();
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
