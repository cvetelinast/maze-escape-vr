using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ColorsGenerator;

public class Preferences {

    private static readonly string LEVEL_PREFS_KEY = "LEVEL";
    private static readonly string MAX_LEVEL_PREFS_KEY = "MAX_LEVEL";
    private static readonly string COINS_PREFS_KEY = "COINS";
    private static readonly string COLOR_SCHEME_PREFS_KEY = "COLOR_SCHEME";

    public static void SetLevel(int level)
    {
        PlayerPrefs.SetInt(LEVEL_PREFS_KEY, level);
    }

    public static int GetLevel() =>
         PlayerPrefs.GetInt(LEVEL_PREFS_KEY, 0);

    public static void SetMaxLevel(int level)
    {
        PlayerPrefs.SetInt(MAX_LEVEL_PREFS_KEY, level);
    }

    public static int GetMaxLevel() =>
         PlayerPrefs.GetInt(MAX_LEVEL_PREFS_KEY, 0);

    public static void SetCoins(int coins)
    {
        PlayerPrefs.SetInt(COINS_PREFS_KEY, coins);
    }

    public static int GetCoins() =>
         PlayerPrefs.GetInt(COINS_PREFS_KEY, 0);

    public static void SetMazeColorScheme(MazeColorScheme mazeColorScheme)
    {
        PlayerPrefs.SetString(COLOR_SCHEME_PREFS_KEY, mazeColorScheme.ToString());
    }

    public static MazeColorScheme GetMazeColorScheme()
    {
        var mazeColorSchemeStr = PlayerPrefs.GetString(
            COLOR_SCHEME_PREFS_KEY, MazeColorScheme.Blue.ToString());
        return (MazeColorScheme)System.Enum.Parse(typeof(MazeColorScheme), mazeColorSchemeStr);
    }
}
