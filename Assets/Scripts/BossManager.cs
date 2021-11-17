using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BossManager : MonoBehaviour
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

    Transform target;

    public GameObject FindClosestPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        Vector3 position = GameObject.FindGameObjectWithTag("Enemy").transform.position;

        if (players.Length == 0)
        {
            Debug.LogWarning("No players found!");
            return null;
        }

        GameObject closest;

        if (players.Length == 1)
        {
            closest = players[0];
            target = closest.transform;
            return closest;
        }

        closest = players
            .OrderBy(go => (position - go.transform.position).sqrMagnitude)
            .First();

        //target.transform.position = closest.transform.position;

        return closest;
    }
}
