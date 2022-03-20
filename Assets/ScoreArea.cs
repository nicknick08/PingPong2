using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreArea : MonoBehaviour
{
    public GameManager GameManager;
    public int agentId;
    
    //スコアエリアに進入時
    void OnTriggerEnter(Collider other){
        GameManager.EndEpisode(agentId);
        
    }
}
