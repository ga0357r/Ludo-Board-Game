using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayPrefs : MonoBehaviour
{
    [SerializeField] private MainMenuUIManager mainMenuUIManager;
    [SerializeField] private Button playButton;
    [SerializeField] private GameObject playerCountToggle;
    [SerializeField] private GameObject tokenSelectionToggle;

    private void OnEnable()
    {
        ShowMenu();
    }

    private void ShowMenu()
    {
        if (mainMenuUIManager.CreatedRoom == true)
        {
            // do smth
            // Show Host Menu

            // enable all ui settings
            playButton.gameObject.SetActive(true);
            playerCountToggle.SetActive(true);
            tokenSelectionToggle.SetActive(true);

            // make sure text is "Create Game"
            playButton.transform.GetChild(0).GetComponent<Text>().text = StringHelpers.CreateRoom;

            //on click button go to ludo board scene
            playButton.onClick.RemoveAllListeners();
            playButton.onClick.AddListener(OnClickCreateGameButton);

        }
        else if (mainMenuUIManager.CreatedRoom == false)
        {
            // do smth
            // Show Client Menu
        }
        else
        {
            Debug.LogError($"Failure mainMenuUIManager.IsHost is {mainMenuUIManager.CreatedRoom} ");
        }
    }

    private void OnClickCreateGameButton()
    {
        string roomName = "";
        roomName =  "Room " + Random.Range(1000, 10000);
        byte maxPlayers = (byte)mainMenuUIManager.PlayerCount;
        maxPlayers = (byte)Mathf.Clamp(maxPlayers, 2, 4);
        RoomOptions roomOptions = new RoomOptions { MaxPlayers = maxPlayers, PlayerTtl = 10000 };
        NetworkController.Instance.CreateRoom(roomName, roomOptions);
        gameObject.SetActive(false);
    }
}
