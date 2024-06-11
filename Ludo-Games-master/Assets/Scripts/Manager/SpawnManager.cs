using UnityEngine;
using Unity.Netcode;

public class SpawnManager : Singleton<SpawnManager>
{
    [SerializeField] private SpawnData spawnData;

    public void SpawnNetworkObjects()
    {
        if (NetworkManager.Singleton.IsServer)
        {
            SpawnObjectsOverNetwork();
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void SpawnObjectsOverNetwork()
    {
        foreach (var parentWithChildren in spawnData.ParentsWithChildren)
        {
            // Spawn Parent
            GameObject parentObject = Instantiate(parentWithChildren.parent);
            NetworkObject parentNetworkObject = parentObject.GetComponent<NetworkObject>();

            if (parentNetworkObject != null)
            {
                parentNetworkObject.Spawn();
            }

            // Spawn Children
            foreach (GameObject child in parentWithChildren.children)
            {
                GameObject childObject = Instantiate(child);
                NetworkObject childNetworkObject = childObject.GetComponent<NetworkObject>();

                if (childNetworkObject != null)
                {
                    childNetworkObject.Spawn();
                    childObject.transform.SetParent(parentObject.transform, false);
                }
            }
        }
    }
}
