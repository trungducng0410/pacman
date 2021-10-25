using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGameManager : MonoBehaviour
{
    public string saveScoreKey = "High Score";
    public string saveTimeKey = "Best Time";


    public void SaveScore(int score)
    {
        int saveValue = score;
        string loadValue = PlayerPrefs.GetString(saveScoreKey);
        if (!saveValue.Equals(loadValue))
        {
            PlayerPrefs.SetInt(saveScoreKey, saveValue);
            PlayerPrefs.Save();
        }
    }

    public void SaveTime(float time)
    {
        float saveValue = time;
        string loadValue = PlayerPrefs.GetString(saveTimeKey);
        if (!saveValue.Equals(loadValue))
        {
            PlayerPrefs.SetFloat(saveTimeKey, saveValue);
            PlayerPrefs.Save();
        }
    }

}
