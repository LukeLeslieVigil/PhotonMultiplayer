using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossSpawn : MonoBehaviour
{
    GameObject player;

    public GameObject boss;

    int bossCount = 0;

    private void Update()
    {
        CheckForPlayer();
    }

    void CheckForPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {            
            if (bossCount <= 0)
            {
                Instantiate(boss);
                bossCount++;
            }                    
        }
    }
}
