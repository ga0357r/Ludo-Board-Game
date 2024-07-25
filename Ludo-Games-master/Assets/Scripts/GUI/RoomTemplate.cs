using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;



public class RoomTemplate : MonoBehaviour
{
    public Text RoomNameText;
    public Text RoomPlayersText;
    public Button JoinRoomButton;

    private string roomName;

    #region Monobehaviour Callbacks

    public void Start()
    {
        JoinRoomButton.onClick.AddListener(OnClickJoinRoomButton);
        NetworkController.Instance.OnFinishedJoiningRoom += OnRoomFull;
    }

    private void OnDisable()
    {
        JoinRoomButton.onClick.RemoveListener(OnClickJoinRoomButton);
    }
    #endregion


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

    private bool IsRoomIsFull()
    {
        if (PhotonNetwork.CurrentRoom == null)
        {
            Print.Error("Client not part of any room!");
            return false;
        }

        // Check if the room is full
        if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers) return true;
        return false;
    }

    private void OnRoomFull()
    {
        if (IsRoomIsFull())
        {
            SceneController.Instance.LoadSceneAsync(Scenes.GamePlay);
        }
    }
}