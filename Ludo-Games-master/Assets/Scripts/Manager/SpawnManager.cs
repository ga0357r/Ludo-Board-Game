using UnityEngine;
using Unity.Netcode;

public class SpawnManager : Singleton<SpawnManager>
{
    [SerializeField] private SpawnData boardSpawnData;

    public void SpawnObjects()
    {
        if (NetworkManager.Singleton.IsServer)
        {
            SpawnBoard();
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void SpawnBoard()
    {
        // Spawn Board
        GameObject board = Instantiate<GameObject>(boardSpawnData.Parent);
        NetworkObject networkObject = board.GetComponent<NetworkObject>();
        
        if (networkObject != null)
        {
            networkObject.Spawn();
        }


        // Spawn Children
        foreach (GameObject child in boardSpawnData.Children)
        {
            GameObject currentChild = Instantiate<GameObject>(child);
            NetworkObject childNetworkObject = currentChild.GetComponent<NetworkObject>();

            if (childNetworkObject != null)
            {
                childNetworkObject.Spawn();
                currentChild.transform.SetParent(board.transform, false);
            }
        }
    }
}
