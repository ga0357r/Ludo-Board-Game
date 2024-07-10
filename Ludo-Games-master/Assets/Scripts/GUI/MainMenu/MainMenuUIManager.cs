using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUIManager : MonoBehaviour {

	[SerializeField] private GameObject gamePlayPreference;
	[SerializeField] private GameObject networkPreferences;
	[SerializeField] private QuestionDialog quitDialog;

	[SerializeField] private TokensRadioGroup tokensRadioGroup;
	[SerializeField] private PlayerCountRadioGroup playerCountRadioGroup;
	[SerializeField] private GameObject waitingUI;
	[SerializeField] private GameObject quickPlayObj;
	[SerializeField] private GameObject settingsObj;
	[SerializeField] private GameObject aboutObj;

	private int playerCount = 2;
	private Token.TokenType selectedToken = Token.TokenType.Blue;

	private bool? createdRoom = null;

	public bool? CreatedRoom => createdRoom;
	public int PlayerCount => playerCount;

    void Start ()
	{
		tokensRadioGroup.onTokenTypeSelected += ((Token.TokenType type) => selectedToken = type);
		playerCountRadioGroup.onPlayerCountSelected += ((int count) => playerCount = count);

		networkPreferences.SetActive(false);
		quickPlayObj.SetActive(false);
		settingsObj.SetActive(false);
		aboutObj.SetActive(false);

		NetworkController.Instance.OnFinishedConnectingToPhotonCloud += OnConnectedToPhotonCloudServer;
		NetworkController.Instance.OnFinishedCreatingRoom += OnFinishedCreatingRoom;
    }

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Escape))
		{
			if (gamePlayPreference.activeSelf) 
			{
				gamePlayPreference.SetActive (false);
                networkPreferences.SetActive(true);
                quickPlayObj.SetActive(true);
            } 
			else 
			{
				quitDialog.ShowDialog ("Are you sure want to quit?", () => Application.Quit (), null);
			}
		}
	}

	public void OnClickQuickPlayButton()
	{
		NetworkController.Instance.JoinRandomRoom(); 
	}

	public void OnClickCreateGameButton()
	{
		createdRoom = true;
		gamePlayPreference.SetActive(true);
		quickPlayObj.SetActive (false);
		networkPreferences.SetActive (false);
		waitingUI.SetActive(false);
    }

	public void OnClickJoinGameButton()
	{
		createdRoom = false;
        gamePlayPreference.SetActive(false);
        quickPlayObj.SetActive(false);
        networkPreferences.SetActive(false);
        waitingUI.SetActive(true);
    }

    public void OnVSComputer ()
	{
		gamePlayPreference.SetActive (true);
	}

	public void OnPlay ()
	{
		Token.TokenPlayer[] players = new Token.TokenPlayer[playerCount];
		Token.TokenType[] types = new Token.TokenType[playerCount];

		for (int i = 0; i < playerCount; i++)
		{
			players[i] = Token.TokenPlayer.Computer;
			types[i] = (Token.TokenType)i;

			if (types[i] == selectedToken)
			{
				players[i] = Token.TokenPlayer.Human;
			}
		}

		if ((int)selectedToken >= playerCount)
		{
			players[playerCount - 1] = Token.TokenPlayer.Human;
			types[playerCount - 1] = selectedToken;
		}

		GameMaster gm = GameMaster.instance;
		gm.SelectedTokens = types;
		gm.SelectedTokenPlayers = players;

		SceneManager.LoadScene ("GamePlay");
	}

	private void OnConnectedToPhotonCloudServer()
	{
		networkPreferences.SetActive (true);
		quickPlayObj.SetActive (true);
        settingsObj.SetActive(true);
        aboutObj.SetActive(true);
    }
	
	private void OnFinishedCreatingRoom()
	{
        gamePlayPreference.SetActive(false);
		waitingUI.SetActive(true);
    }

    private void OnDisable()
    {
        NetworkController.Instance.OnFinishedConnectingToPhotonCloud -= OnConnectedToPhotonCloudServer;
        NetworkController.Instance.OnFinishedCreatingRoom -= OnFinishedCreatingRoom;
    }

	public void ToggleGamePlayPrefs(bool enable)
	{
		gamePlayPreference.SetActive(enable);
	}

}
