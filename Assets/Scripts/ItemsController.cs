using UnityEngine;
using static ColorsGenerator;

public class ItemsController : MonoBehaviour {

    [SerializeField] private Transform orangeWorldTransform;

    [SerializeField] private Transform blueLagoonTransform;

    [SerializeField] private Transform jungleTransform;

    [SerializeField] private Transform barbieTransform;

    [SerializeField] private Transform darkTransform;

    public void Initialize(MazeColorScheme mazeColorScheme)
    {
        switch (mazeColorScheme)
        {
            case MazeColorScheme.GARFIELD:
                InitializeObjectVisibilities(isGarfieldVisible: true);
                break;
            case MazeColorScheme.BLUE_LAGOON:
                InitializeObjectVisibilities(isBlueLagoonVisible: true);
                break;
            case MazeColorScheme.JUNGLE:
                InitializeObjectVisibilities(isJungleVisible: true);
                break;
            case MazeColorScheme.BARBIE:
                InitializeObjectVisibilities(isBarbieVisible: true);
                break;
            case MazeColorScheme.DARTH_VADER:
                InitializeObjectVisibilities(isDarthVaderVisible: true);
                break;
        }
    }

    private void InitializeObjectVisibilities(bool isGarfieldVisible = false, bool isBlueLagoonVisible = false,
        bool isJungleVisible = false, bool isBarbieVisible = false, bool isDarthVaderVisible = false)
    {
        orangeWorldTransform.gameObject.SetActive(isGarfieldVisible);
        blueLagoonTransform.gameObject.SetActive(isBlueLagoonVisible);
        jungleTransform.gameObject.SetActive(isJungleVisible);
        barbieTransform.gameObject.SetActive(isBarbieVisible);
        darkTransform.gameObject.SetActive(isDarthVaderVisible);
    }
}
