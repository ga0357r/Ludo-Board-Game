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
}
