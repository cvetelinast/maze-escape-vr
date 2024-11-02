using System.Collections.Generic;
using UnityEngine;

public class ColorsGenerator : MonoBehaviour {

    public enum MazeColorScheme {
        GARFIELD,
        BLUE_LAGOON,
        JUNGLE,
        BARBIE,
        DARTH_VADER
    }

    [SerializeField] public MazeColorScheme colorScheme { get; private set; }

    [SerializeField] private List<Color> colors = new List<Color>();

    private System.Random rand = new System.Random();

    private Color baseColor;

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
                MazeColorScheme.GARFIELD => Constants.BASE_ORANGE_COLOR,
                MazeColorScheme.BLUE_LAGOON => Constants.BASE_BLUE_COLOR,
                MazeColorScheme.JUNGLE => Constants.BASE_GREEN_COLOR,
                MazeColorScheme.BARBIE => Constants.BASE_PINK_COLOR,
                MazeColorScheme.DARTH_VADER => Constants.BASE_BLACK_COLOR,
                _ => new Color(1.0f, 1.0f, 1.0f),
            };

            for (int i = 0; i < 20; i++)
            {
                colors.Add(GetNextColorForColorScheme());
            }

        }

        return colors;
    }

    public void SetupBaseColor()
    {
        int level = Preferences.GetLevel();
        colorScheme = GetMazeColorSchemeForLevel(level);
        this.baseColor = CalculateBaseColor(colorScheme);
        colors.Add(this.baseColor);
    }

    public static Color CalculateBaseColor(MazeColorScheme colorScheme)
    {
        return colorScheme switch
        {
            MazeColorScheme.GARFIELD => Constants.BASE_ORANGE_COLOR,
            MazeColorScheme.BLUE_LAGOON => Constants.BASE_BLUE_COLOR,
            MazeColorScheme.JUNGLE => Constants.BASE_GREEN_COLOR,
            MazeColorScheme.BARBIE => Constants.BASE_PINK_COLOR,
            MazeColorScheme.DARTH_VADER => Constants.BASE_BLACK_COLOR,
            _ => new Color(1.0f, 1.0f, 1.0f),
        };
    }

    public static MazeColorScheme GetMazeColorSchemeForLevel(int level)
    {
        return level switch
        {
            1 => MazeColorScheme.BARBIE,
            2 => MazeColorScheme.BLUE_LAGOON,
            3 => MazeColorScheme.JUNGLE,
            4 => MazeColorScheme.GARFIELD,
            5 => MazeColorScheme.DARTH_VADER,

            6 => MazeColorScheme.BLUE_LAGOON,
            7 => MazeColorScheme.JUNGLE,
            8 => MazeColorScheme.GARFIELD,
            9 => MazeColorScheme.BARBIE,
            10 => MazeColorScheme.DARTH_VADER,

            11 => MazeColorScheme.JUNGLE,
            12 => MazeColorScheme.BLUE_LAGOON,
            13 => MazeColorScheme.BARBIE,
            14 => MazeColorScheme.GARFIELD,
            15 => MazeColorScheme.DARTH_VADER,

            _ => throw new System.NotImplementedException(),
        };
    }

    public Color GetNextColorForColorScheme()
    {
        Color color = colorScheme switch
        {
            MazeColorScheme.GARFIELD => GetRandomOrangeColor(),
            MazeColorScheme.BLUE_LAGOON => GetRandomBlueColor(),
            MazeColorScheme.JUNGLE => GetRandomGreenColor(),
            MazeColorScheme.BARBIE => GetRandomPinkColor(),
            MazeColorScheme.DARTH_VADER => GetRandomDarkColor(),
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
        float r = Mathf.Clamp01(Constants.BASE_PINK_COLOR.r + (float)(rand.NextDouble() * 0.4 - 0.2));
        float g = Mathf.Clamp01(Constants.BASE_PINK_COLOR.g + (float)(rand.NextDouble() * 0.4 - 0.2));
        float b = Mathf.Clamp01(Constants.BASE_PINK_COLOR.b + (float)(rand.NextDouble() * 0.4 - 0.2));

        return new Color(r, g, b);
    }

    private Color GetRandomOrangeColor()
    {
        float r = Mathf.Clamp01(Constants.BASE_ORANGE_COLOR.r + (float)(rand.NextDouble() * 0.6 + 3.15));
        float g = Mathf.Clamp01(Constants.BASE_ORANGE_COLOR.g + (float)(rand.NextDouble() * 0.5 - 0.12));
        float b = Mathf.Clamp01(Constants.BASE_ORANGE_COLOR.b + (float)(rand.NextDouble() * 0.2 - 0.5));

        return new Color(r, g, b);
    }

    private Color GetRandomBlueColor()
    {
        float r = Mathf.Clamp01(Constants.BASE_BLUE_COLOR.r + (float)(rand.NextDouble() * 0.1 - 0.2));
        float g = Mathf.Clamp01(Constants.BASE_BLUE_COLOR.g + (float)(rand.NextDouble() * 0.4 - 0.4));
        float b = Mathf.Clamp01(Constants.BASE_BLUE_COLOR.b + (float)(rand.NextDouble() * 0.4 - 0.4));

        return new Color(r, g, b);
    }

    private Color GetRandomGreenColor()
    {
        float r = Mathf.Clamp01(Constants.BASE_GREEN_COLOR.r + (float)(rand.NextDouble() * 0.4 - 0.2));
        float g = Mathf.Clamp01(Constants.BASE_GREEN_COLOR.g + (float)(rand.NextDouble() * 0.4 - 0.5));
        float b = Mathf.Clamp01(Constants.BASE_GREEN_COLOR.b + (float)(rand.NextDouble() * 0.4 - 0.2));

        return new Color(r, g, b);
    }

    private Color GetRandomDarkColor()
    {
        float r = Mathf.Clamp01((float)(rand.NextDouble() * 0.4 - 0.2));
        float g = Mathf.Clamp01((float)(rand.NextDouble() * 0.4 - 0.2));
        float b = Mathf.Clamp01((float)(rand.NextDouble() * 0.4 - 0.2));

        return new Color(r, g, b);
    }
}
