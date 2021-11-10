using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using System.IO;

public class RoomManager : MonoBehaviourPunCallbacks
{

    public static RoomManager Instance;


    private void Awake()
    {
        if (Instance) //checks if another RoomMananger exists
        {
            Destroy(gameObject); //Destroys the previous manager
            return;
        }
        DontDestroyOnLoad(gameObject); //Doesn't destroy itself if it is the only one
        Instance = this;
    }

    public override void OnEnable()
    {
        base.OnEnable();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if(scene.buildIndex == 1) //We are in the game
        {
            //Instantiate the player manager
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrfab", "PlayerManager"), Vector3.zero, Quaternion.identity);
        }
    }
}
