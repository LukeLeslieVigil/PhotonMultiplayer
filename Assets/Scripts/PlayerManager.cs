using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{

    PhotonView PV;
    GameObject controller;

    public int lives;
    public TextMeshProUGUI livesCounter;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
        livesCounter = GetComponentInChildren<TextMeshProUGUI>();
    }

    void Start()
    {
        //Checks if the view is the local player then creates a controller
        if (PV.IsMine)
        {
            CreateController();
        }
        else
        {
            Destroy(livesCounter);
        }
    }

    private void Update()
    {
        if (PV.IsMine)
        {
            livesCounter.text = "Lives: " + lives;
        }
    }

    void CreateController()
    {
        Transform spawnpoint = SpawnManager.Instance.GetSpawnPoint();
        Debug.Log("CreateController Instantiate");
        controller = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController"), spawnpoint.position, spawnpoint.rotation, 0, new object[] { PV.ViewID });
    }

    public void Die()
    {
        PhotonNetwork.Destroy(controller);
        lives--;
        if(lives <= 0)
        {
            PhotonNetwork.LeaveRoom();
            SceneManager.LoadScene("Main Menu");
        }
        else
        {
            CreateController();
        }
    }
}
