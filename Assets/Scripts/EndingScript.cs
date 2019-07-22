using UnityEngine;

public class EndingScript : MonoBehaviour
{
    public static int CalculateScore()
    {
        int checkscore = LevelVariables.EnemiesKilled * 10;
        return checkscore;
    }

    public static bool ok()
    {
        if (CalculateScore() == LevelVariables.Score)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
