using UnityEngine;

public static class Print
{
    private static readonly bool isEnabled = true;

    public static void Log(string message)
    {
        if (isEnabled)
        {
            Debug.Log($"<color=white>{message}</color>");
        }
    }

    public static void Warning(string message)
    {
        if (isEnabled)
        {
            Debug.LogWarning($"<color=yellow>{message}</color>");
        }
    }

    public static void Error(string message)
    {
        if (isEnabled)
        {
            Debug.LogError($"<color=red>{message}</color>");
        }
    }
}