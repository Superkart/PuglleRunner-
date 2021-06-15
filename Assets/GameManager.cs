using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    private GameObject player;
    int playerStartScore;
    DataManager datmgrRef;
    //All Managers in Game
    public static DataManager m_DataManager;
    [SerializeField]
    public static AudioManager m_AudioManager;
    public static DataManager GetDataManager()
    {
        if (m_DataManager == null)
        {
           m_DataManager =  GameObject.Find("DataManager").GetComponent<DataManager>();
        }
        return m_DataManager;
    }
    public static AudioManager GetAudioManager()
    {
        if (m_AudioManager == null)
        {
            m_AudioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        }

        return m_AudioManager;
    }

    private void Start()
    {
        GetAudioManager().PlayBackgroundMusic();


        player = GameObject.Find("Player");
        playerStartScore = Mathf.RoundToInt(player.transform.position.z);
        datmgrRef = GameManager.GetDataManager();
    }

    // Update is called once per frame
    void Update()
    {
        int currPlayerDistance = Mathf.RoundToInt(player.transform.position.z);
        int currDistance = currPlayerDistance - playerStartScore;
        datmgrRef.SetDistance(currDistance);
    }
}

