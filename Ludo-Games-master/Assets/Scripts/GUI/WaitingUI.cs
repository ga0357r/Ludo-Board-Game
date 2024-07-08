using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaitingUI : MonoBehaviour
{
    [SerializeField] private MainMenuUIManager mainMenuUIManager;
    [SerializeField] private GameObject createdRoom;
    [SerializeField] private Text createdRoomBodyText;

    //[SerializeField] private GameObject joiningRoom;

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
            // disable joiningRoom waiting UI
            createdRoom.SetActive(true);
            // Call the method to check the number of connected clients
            //InvokeRepeating(NetworkController.Instance.GetConnectedClientsNumber, 1.0f, 1.0f); // Check every 1 second
        }

        else
        {
            // disable createdRoom waiting UI
            //createdRoom.SetActive(true);
            // enable client waiting UI TODO
        }

    }
}
