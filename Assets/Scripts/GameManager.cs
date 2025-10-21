using UnityEngine;

public static class GameManager
{
    private static float _totalMoneyEarned;
    private static float _currentLevelMoneyEarned;
    private static float _prevLevelMoneyEarned;

    // Call to get currently recorded total money
    public static float GetTotalMoney()
    {
        return _totalMoneyEarned;
    }

    // Call at the end of level
    public static void UpdateTotalMoney()
    {
        _totalMoneyEarned += _currentLevelMoneyEarned;
        _prevLevelMoneyEarned = _currentLevelMoneyEarned;
    }

    public static void ResetCurrentLevelMoney()
    {
        _currentLevelMoneyEarned = 0f;
    }
    
    public static void IncrementCurrentLevelMoney(float increment)
    {
        _currentLevelMoneyEarned += increment;
        Debug.Log($"Current Level Money is {_currentLevelMoneyEarned}");
    }

    public static void ResetGameManager()
    {
        Debug.Log($"Game Manager Reset");
        _totalMoneyEarned = 0f;
        _currentLevelMoneyEarned = 0f;
    }

    public static float GetCurrentLevelMoney()
    {
        Debug.Log($"GetCurrentLevelMoney Called {_currentLevelMoneyEarned}");
        return _currentLevelMoneyEarned;
    }

    public static float SubtractRent(float rent)
    {
        _totalMoneyEarned -= rent;
        Debug.Log($"Subtracted Rent {rent}");
        return _totalMoneyEarned;
    }

    public static bool LevelPassed()
    {
        Debug.Log($"Level Passed Checked");
        return _totalMoneyEarned > 0;
    }

    public static void ResetToLevelStart()
    {
        Debug.Log($"ResetToLevelStart Called");   
        _totalMoneyEarned -= _prevLevelMoneyEarned;
        _prevLevelMoneyEarned = 0;
    }
}
