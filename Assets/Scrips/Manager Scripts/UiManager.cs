using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public TextMeshProUGUI CoinsLabel;
    public Text GameDistanceLabel;
    public TextMeshProUGUI FinalScoreLabel;
    public TextMeshProUGUI HighScoreLabel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (CoinsLabel != null)
        {
            CoinsLabel.text = "Coins " + GameManager.GetDataManager().GetCoins().ToString(); 
        }
        if (GameDistanceLabel != null)
        {
            GameDistanceLabel.text = GameManager.GetDataManager().GetDistance().ToString() + "m";
        }

        if (GameOver.gameOver)
        {
            GameDistanceLabel.text =  GameManager.GetDataManager().GetDistance().ToString();
            FinalScoreLabel.text = "Score" + GameManager.GetDataManager().GetScore().ToString();
            HighScoreLabel.text = "High Score" + GameManager.GetDataManager().GetHighScore();
        }
            
    }
}
