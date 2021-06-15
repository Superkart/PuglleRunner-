using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public static bool gameOver;
    public GameObject gameOverPanel;
    public GameObject Player;


    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;
        gameOverPanel.SetActive(false); 
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver)
        {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
            GameManager.GetDataManager().SaveData();
            GameManager.GetAudioManager().StopBackgroundMusic();
        }
        
    }

    public void Replay()
    {
        GameManager.GetAudioManager().PlaySfx();
        gameOver = false;
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void ReturnToMainMenu()
    {
        GameManager.GetAudioManager().PlaySfx();
        gameOver = false;
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

}
