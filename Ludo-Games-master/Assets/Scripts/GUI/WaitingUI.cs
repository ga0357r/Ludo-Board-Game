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
        Print.Log("DisplayAvailableRooms");
        NetworkController.Instance.DisplayAvailableRooms(roomList, roomTemplatePrefab, availableRooms);
    }
}
