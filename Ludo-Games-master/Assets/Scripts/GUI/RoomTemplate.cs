using UnityEngine;
using UnityEngine.UI;



public class RoomTemplate : MonoBehaviour
{
    public Text RoomNameText;
    public Text RoomPlayersText;
    public Button JoinRoomButton;

    private string roomName;

    public void Start()
    {
        JoinRoomButton.onClick.AddListener(OnClickJoinRoomButton);
    }

    public void Initialize(string name, byte currentPlayers, byte maxPlayers)
    {
        roomName = name;
        RoomNameText.text = name;
        RoomPlayersText.text = currentPlayers + " / " + maxPlayers;
    }

    private void OnClickJoinRoomButton()
    {
        NetworkController.Instance.LeaveLobby();
        NetworkController.Instance.JoinRoom(roomName);
    }

    private void OnDisable()
    {
        JoinRoomButton.onClick.RemoveListener(OnClickJoinRoomButton);
    }
}