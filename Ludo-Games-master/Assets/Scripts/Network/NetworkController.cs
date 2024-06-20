using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class NetworkController : Singleton<NetworkController>
{
    [SerializeField] public NetworkManager networkManager;

    public NetworkManager NetworkManager => networkManager;

    public void StartServer()
    {
        networkManager.StartServer();
    }

    public void StartHost()
    {
        networkManager.StartHost();
    }

    public void StartClient()
    {
        networkManager.StartClient();
    }

    public int GetConnectedClientsNumber()
    {
        if (networkManager != null && NetworkManager.Singleton.IsServer)
        {
            int connectedClients = networkManager.ConnectedClientsList.Count;
            return connectedClients;
        }

        Print.Error("Network Manager is null or this is not a server");
        return 0;
    }
}
