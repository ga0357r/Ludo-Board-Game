using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using Photon;
using Photon.Pun;

public class NetworkController : MonoBehaviourPunCallbacks
{
    #region Variables

    private static NetworkController instance;

    #endregion

    #region Properties

    public static NetworkController Instance => instance;

    #endregion

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ConnectToPhotonCloud();
    }

    private void ConnectToPhotonCloud()
    {
        Print.Log("Connecting to Photon Cloud....");
        PhotonNetwork.ConnectUsingSettings();
    }

    public void CreateRoom()
    {
        //networkManager.StartServer();
    }

    public void JoinRoom()
    {
        //networkManager.StartClient();
    }

    public int GetConnectedClientsNumber()
    {
        //if (networkManager != null && NetworkManager.Singleton.IsServer)
        //{
        //    int connectedClients = networkManager.ConnectedClientsList.Count;
        //    return connectedClients;
        //}

        //Print.Error("Network Manager is null or this is not a server");
        //return 0;
        return 0;
    }

    #region Callbacks

    public override void OnConnectedToMaster()
    {
        Print.Log("Connected To Photon Cloud Server");
    }

    #endregion
}
