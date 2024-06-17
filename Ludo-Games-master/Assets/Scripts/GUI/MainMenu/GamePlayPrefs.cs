using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayPrefs : MonoBehaviour
{
    [SerializeField] private MainMenuUIManager mainMenuUIManager;
    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject playerCountToggle;
    [SerializeField] private GameObject tokenSelectionToggle;

    private void OnEnable()
    {
        ShowMenu();
    }

    private void ShowMenu()
    {
        if (mainMenuUIManager.IsHost == true)
        {
            // do smth
            // Show Host Menu

            // enable all ui settings
            playButton.SetActive(true);
            playerCountToggle.SetActive(true);
            tokenSelectionToggle.SetActive(true);

            // make sure text is "Create Game"
            playButton.transform.GetChild(0).GetComponent<Text>().text = StringHelpers.CreateGame;

        }
        else if (mainMenuUIManager.IsHost == false)
        {
            // do smth
            // Show Client Menu
        }
        else
        {
            Debug.LogError($"Failure mainMenuUIManager.IsHost is {mainMenuUIManager.IsHost} ");
        }
    }


}
