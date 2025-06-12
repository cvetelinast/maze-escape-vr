using UnityEngine;

public class Preferences {

    private static readonly string CURRENT_LEVEL_PREFS_KEY = "CURRENT_LEVEL";
    private static readonly string MAX_UNLOCKED_LEVEL_PREFS_KEY = "MAX_UNLOCKED_LEVEL";
    private static readonly string COINS_PREFS_KEY = "COINS";

    public static void SetLevel(int level)
    {
        PlayerPrefs.SetInt(CURRENT_LEVEL_PREFS_KEY, level);
    }

    public static int GetLevel() =>
         PlayerPrefs.GetInt(CURRENT_LEVEL_PREFS_KEY, 1);

    public static void SetMaxUnlockedLevel(int level)
    {
        PlayerPrefs.SetInt(MAX_UNLOCKED_LEVEL_PREFS_KEY, level);
    }

    public static int GetMaxUnlockedLevel() =>
         PlayerPrefs.GetInt(MAX_UNLOCKED_LEVEL_PREFS_KEY, 13);

    public static void SetCoins(int coins)
    {
        PlayerPrefs.SetInt(COINS_PREFS_KEY, coins);
    }

    public static int GetCoins() =>
         PlayerPrefs.GetInt(COINS_PREFS_KEY, 10);
}
