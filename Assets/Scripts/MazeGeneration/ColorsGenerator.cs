using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorsGenerator : MonoBehaviour {

    public enum MazeColorScheme {
        // Light colors
        ORANGE_WORLD,
        BLUE_LAGOON,
        JUNGLE,
        BARBIE,

        // Dark colors
        Black

    }

    [SerializeField] private MazeColorScheme colorScheme = MazeColorScheme.ORANGE_WORLD;

    [SerializeField] private List<Color> colors = new List<Color>();

    private System.Random rand = new System.Random();

    private Color baseColor;

    private readonly Color BASE_PINK_COLOR = new Color(1.0f, 0.75f, 0.8f);
    private readonly Color BASE_ORANGE_COLOR = new Color(1.0f, 0.56f, 0.35f);
    private readonly Color BASE_BLUE_COLOR = new Color(0.0f, 0.95f, 0.95f);
    private readonly Color BASE_GREEN_COLOR = new Color(0.4f, 0.95f, 0.3f);
    private readonly Color BASE_BLACK_COLOR = new Color(0.0f, 0.0f, 0.0f);

    // Start is called before the first frame update
    void Start()
    {
        //GenerateColors(); // For test purposes

    }

    // For test purposes
    public List<Color> GenerateColors()
    {
        foreach (MazeColorScheme colorScheme in System.Enum.GetValues(typeof(MazeColorScheme)))
        {
            Color baseColor = colorScheme switch
            {
                MazeColorScheme.ORANGE_WORLD => BASE_ORANGE_COLOR,
                MazeColorScheme.BLUE_LAGOON => BASE_BLUE_COLOR,
                MazeColorScheme.JUNGLE => BASE_GREEN_COLOR,
                MazeColorScheme.BARBIE => BASE_PINK_COLOR,
                MazeColorScheme.Black => BASE_BLACK_COLOR,
                _ => new Color(1.0f, 1.0f, 1.0f),
            };

            for (int i = 0; i < 20; i++)
            {
                colors.Add(GetNextColorForColorScheme());
            }

        }

        return colors;
    }

    public void CalculateBaseColor()
    {
        this.baseColor = colorScheme switch
        {
            MazeColorScheme.ORANGE_WORLD => BASE_ORANGE_COLOR,
            MazeColorScheme.BLUE_LAGOON => BASE_BLUE_COLOR,
            MazeColorScheme.JUNGLE => BASE_GREEN_COLOR,
            MazeColorScheme.BARBIE => BASE_PINK_COLOR,
            MazeColorScheme.Black => BASE_BLACK_COLOR,
            _ => new Color(1.0f, 1.0f, 1.0f),
        };
        colors.Add(this.baseColor);
    }

    public Color GetNextColorForColorScheme()
    {
        Color color = colorScheme switch
        {
            MazeColorScheme.ORANGE_WORLD => GetRandomOrangeColor(),
            MazeColorScheme.BLUE_LAGOON => GetRandomBlueColor(),
            MazeColorScheme.JUNGLE => GetRandomGreenColor(),
            MazeColorScheme.BARBIE => GetRandomPinkColor(),
            MazeColorScheme.Black => new Color(0.0f, 0.0f, 0.0f),
            _ => new Color(1.0f, 1.0f, 1.0f),
        };
        return color;
    }

    public Color GetFloorColor()
    {
        return this.baseColor;
    }

    private Color GetRandomPinkColor()
    {
        float r = Mathf.Clamp01(BASE_PINK_COLOR.r + (float)(rand.NextDouble() * 0.4 - 0.2));
        float g = Mathf.Clamp01(BASE_PINK_COLOR.g + (float)(rand.NextDouble() * 0.4 - 0.2));
        float b = Mathf.Clamp01(BASE_PINK_COLOR.b + (float)(rand.NextDouble() * 0.4 - 0.2));

        return new Color(r, g, b);
    }

    private Color GetRandomOrangeColor()
    {
        float r = Mathf.Clamp01(BASE_ORANGE_COLOR.r + (float)(rand.NextDouble() * 0.6 + 3.15));
        float g = Mathf.Clamp01(BASE_ORANGE_COLOR.g + (float)(rand.NextDouble() * 0.5 - 0.12));
        float b = Mathf.Clamp01(BASE_ORANGE_COLOR.b + (float)(rand.NextDouble() * 0.2 - 0.5));

        return new Color(r, g, b);
    }

    private Color GetRandomBlueColor()
    {
        float r = Mathf.Clamp01(BASE_BLUE_COLOR.r + (float)(rand.NextDouble() * 0.1 - 0.2));
        float g = Mathf.Clamp01(BASE_BLUE_COLOR.g + (float)(rand.NextDouble() * 0.4 - 0.4));
        float b = Mathf.Clamp01(BASE_BLUE_COLOR.b + (float)(rand.NextDouble() * 0.4 - 0.4));

        return new Color(r, g, b);
    }

    private Color GetRandomGreenColor()
    {
        float r = Mathf.Clamp01(BASE_GREEN_COLOR.r + (float)(rand.NextDouble() * 0.4 - 0.2));
        float g = Mathf.Clamp01(BASE_GREEN_COLOR.g + (float)(rand.NextDouble() * 0.4 - 0.5));
        float b = Mathf.Clamp01(BASE_GREEN_COLOR.b + (float)(rand.NextDouble() * 0.4 - 0.2));

        return new Color(r, g, b);
    }
}
