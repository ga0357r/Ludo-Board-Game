using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManagerUI : MonoBehaviour
{
    [SerializeField] private Button serverButton;
    [SerializeField] private Button hostButton;
    [SerializeField] private Button clientButton;

    private void Awake()
    {
        serverButton.onClick.AddListener(StartServer);
        hostButton.onClick.AddListener(StartHost);
        clientButton.onClick.AddListener(StartClient);
    }

    private void StartServer()
    {
        NetworkManager.Singleton.StartServer();
        
    }

    private void StartHost()
    {
        NetworkManager.Singleton.StartHost();
    }

    private void StartClient()
    {
        NetworkManager.Singleton.StartClient();
        
    }
}
