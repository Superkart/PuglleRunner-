using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinPickup : MonoBehaviour
{
   public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.GetAudioManager().PlaySoundEffect("CoinCollect");
            //increment the coin cointer here
            GameManager.GetDataManager().UpdateCoins(1);
            //destroy the coin here
            GameObject.Destroy(this.gameObject);

            return;
        }
    }
   
}
