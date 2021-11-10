using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class PlayerManager : MonoBehaviour
{

    PhotonView PV;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    void Start()
    {
        //Checks if the view is the local player then creates a controller
        if (PV.IsMine)
        {
            CreateController();
        }
    }


    void Update()
    {
        
    }

    void CreateController()
    {
        Debug.Log("CreateController Instantiate");
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController"), Vector3.zero, Quaternion.identity);
    }
}
