using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    //Game Data

    int Coins;
    int Score = 0;
    int Distance;
    const string PP_HIGHSCORE_KEY = "highscore";


    public void SetDistance(int dist)
    {
        Distance = dist;
    }

    public int GetDistance()
    {
        return Distance;
    }
    public void UpdateCoins(int value)
    {
        Coins += value;
    }

    public int GetCoins()
    {
        return Coins;
    }

    public int GetScore()
    {
        Score = Coins + Distance;
        return Score;
    }

    public void SaveData()
    {

        if (!PlayerPrefs.HasKey(PP_HIGHSCORE_KEY))
        {
            PlayerPrefs.SetInt(PP_HIGHSCORE_KEY, Score);
        }
        else
        {
            int prevHs = PlayerPrefs.GetInt(PP_HIGHSCORE_KEY);

            if (Score > prevHs)
            {
                PlayerPrefs.SetInt(PP_HIGHSCORE_KEY, Score);
            }
        }
        PlayerPrefs.Save();
    }

    public int GetHighScore()
    {
        if (PlayerPrefs.HasKey(PP_HIGHSCORE_KEY))
        {
            return PlayerPrefs.GetInt(PP_HIGHSCORE_KEY);
        }

        return 0;
    }



}
