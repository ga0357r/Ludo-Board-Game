using Photon.Pun;
using Photon.Pun.Demo.Asteroids;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaitingUI : MonoBehaviour
{
    [SerializeField] private MainMenuUIManager mainMenuUIManager;
    [SerializeField] private GameObject createdRoom;
    [SerializeField] private Text createdRoomBodyText;
    [SerializeField] private GameObject joiningRoom;
    [SerializeField] private GameObject roomTemplatePrefab;
    [SerializeField] private Transform availableRooms;

    private Dictionary<string, RoomInfo> cachedRoomList = new Dictionary<string, RoomInfo>();
    private Dictionary<string, GameObject> roomListEntries = new Dictionary<string, GameObject>();

    #region Monobehaviour Callbacks
    private void OnEnable()
    {
        ShowUI();
    }

    private void OnDisable()
    {
        NetworkController.Instance.OnFinishedRoomListUpdate -= DisplayAvailableRooms;
    }
    #endregion


    /// <summary>
    /// Waiting For Players To Join UI
    /// </summary>
    public void ShowUI()
    {
        if (mainMenuUIManager.CreatedRoom == null)
        {
            Print.Error("CreatedRoom is null");
            return;
        }

        if (mainMenuUIManager.CreatedRoom == true)
        {
            joiningRoom.SetActive(false);
            createdRoom.SetActive(true);
        }

        else
        {
            createdRoom.SetActive(false);
            joiningRoom.SetActive(true);
            JoinLobby();
        }

    }

    private void JoinLobby()
    {
        NetworkController.Instance.JoinLobby();
        NetworkController.Instance.OnFinishedRoomListUpdate += DisplayAvailableRooms;
    }

    private void DisplayAvailableRooms(List<RoomInfo> roomList)
    {
        // TODO Spawn roomTemplatePrefab
        Print.Log("Spawn roomTemplatePrefab Now");
        ClearRoomListView();
        UpdateCachedRoomList(roomList);
        UpdateRoomListView();
    }

    

    private void UpdateRoomListView()
    {
        foreach (RoomInfo info in cachedRoomList.Values)
        {
            GameObject entry = Instantiate(roomTemplatePrefab, availableRooms);
            entry.transform.localScale = Vector3.one;
            entry.GetComponent<RoomListEntry>().Initialize(info.Name, (byte)info.PlayerCount, (byte)info.MaxPlayers);
            roomListEntries.Add(info.Name, entry);
        }
    }

    private void UpdateCachedRoomList(List<RoomInfo> roomList)
    {
        foreach (RoomInfo info in roomList)
        {
            // Remove room from cached room list if it got closed, became invisible or was marked as removed
            if (!info.IsOpen || !info.IsVisible || info.RemovedFromList)
            {
                if (cachedRoomList.ContainsKey(info.Name))
                {
                    cachedRoomList.Remove(info.Name);
                }

                continue;
            }

            // Update cached room info
            if (cachedRoomList.ContainsKey(info.Name))
            {
                cachedRoomList[info.Name] = info;
            }
            // Add new room info to cache
            else
            {
                cachedRoomList.Add(info.Name, info);
            }
        }
    }

    private void ClearRoomListView()
    {
        foreach (GameObject entry in roomListEntries.Values)
        {
            Destroy(entry.gameObject);
        }

        roomListEntries.Clear();
    }
}
