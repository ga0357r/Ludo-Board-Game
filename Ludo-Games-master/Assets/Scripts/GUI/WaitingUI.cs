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

    private void OnEnable()
    {
        ShowUI();
    }

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
            DisplayAvailableRooms();
        }

    }

    private void DisplayAvailableRooms()
    {
        //TODO
    }
}
