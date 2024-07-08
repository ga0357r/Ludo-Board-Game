using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;
using Photon.Realtime;

public class NetworkController : MonoBehaviourPunCallbacks
{
    #region Variables

    private static NetworkController instance;
    private bool hasConnectedToServer = false;

    public event Action OnFinishedConnectingToPhotonCloud;
    public event Action OnFinishedCreatingRoom;
    public event Action OnFinishedJoiningRoom;
    #endregion

    #region Properties

    public static NetworkController Instance => instance;
    public bool HasConnectedToServer => hasConnectedToServer;
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

    public void CreateRoom(string roomName, RoomOptions roomOptions = null, TypedLobby typedLobby = null, string[] expectedUsers = null)
    {
        PhotonNetwork.CreateRoom(roomName, roomOptions, typedLobby, expectedUsers);
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
        hasConnectedToServer = true;
        OnFinishedConnectingToPhotonCloud?.Invoke();
    }

    public override void OnCreatedRoom()
    {
        Print.Log("Room Creation Successful");
        OnFinishedCreatingRoom?.Invoke();
    }

    public override void OnJoinedRoom()
    {
        Print.Log("Joined Room Successfully");
        OnFinishedJoiningRoom?.Invoke();
    }

    #endregion
}
