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
            case MazeColorScheme.ORANGE_WORLD:
                orangeWorldTransform.gameObject.SetActive(true);
                break;
            case MazeColorScheme.BLUE_LAGOON:
                blueLagoonTransform.gameObject.SetActive(true);
                break;
            case MazeColorScheme.JUNGLE:
                jungleTransform.gameObject.SetActive(true);
                break;
            case MazeColorScheme.BARBIE:
                barbieTransform.gameObject.SetActive(true);
                break;
            case MazeColorScheme.DARK:
                darkTransform.gameObject.SetActive(true);
                break;
        }
    }
}
