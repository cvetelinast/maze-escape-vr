using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorsGenerator : MonoBehaviour {

    public enum MazeColorScheme {
        // Light colors
        Orange,
        Blue,
        Green,
        Pink,

        // Dark colors
        Black

    }

    [SerializeField] private MazeColorScheme colorScheme = MazeColorScheme.Orange;

    [SerializeField] private List<Color> colors = new List<Color>();

    private System.Random rand = new System.Random();

    private Color baseColor;

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
                MazeColorScheme.Orange => new Color(1.0f, 0.5f, 0.0f),
                MazeColorScheme.Blue => new Color(0.0f, 0.0f, 1.0f),
                MazeColorScheme.Green => new Color(0.0f, 1.0f, 0.0f),
                MazeColorScheme.Pink => new Color(1.0f, 0.75f, 0.8f),
                MazeColorScheme.Black => new Color(0.0f, 0.0f, 0.0f),
                _ => new Color(1.0f, 1.0f, 1.0f),
            };

            for (int i = 0; i < 20; i++)
            {
                colors.Add(GetRandomColor(baseColor));
            }

        }

        return colors;
    }

    public void CalculateBaseColor()
    {
        this.baseColor = colorScheme switch
        {
            MazeColorScheme.Orange => new Color(1.0f, 0.5f, 0.0f),
            MazeColorScheme.Blue => new Color(0.0f, 0.0f, 1.0f),
            MazeColorScheme.Green => new Color(0.0f, 1.0f, 0.0f),
            MazeColorScheme.Pink => new Color(1.0f, 0.75f, 0.8f),
            MazeColorScheme.Black => new Color(0.0f, 0.0f, 0.0f),
            _ => new Color(1.0f, 1.0f, 1.0f),
        };
        colors.Add(this.baseColor);
    }

    public Color GetNextColorForColorScheme()
    {
        return GetRandomColor(this.baseColor);
    }

    public Color GetFloorColor()
    {
        return this.baseColor;
    }

    private Color GetRandomColor(Color baseColor)
    {
        float r = Mathf.Clamp01(baseColor.r + (float)(rand.NextDouble() * 0.4 - 0.2));
        float g = Mathf.Clamp01(baseColor.g + (float)(rand.NextDouble() * 0.4 - 0.2));
        float b = Mathf.Clamp01(baseColor.b + (float)(rand.NextDouble() * 0.4 - 0.2));

        return new Color(r, g, b);
    }
}
