using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class MenuController : MonoBehaviourPunCallbacks
{
    [SerializeField] InputField nameField;
    int gameMode = 0;
    bool isConnecting;

    [SerializeField] GameObject menuPanel;
    [SerializeField] GameObject lobbyPanel;

    [SerializeField] Text playerName1;
    [SerializeField] Text playerName2;
    [SerializeField] Text gameStartingText;

    private TypedLobby sqlLobby = new TypedLobby("customSqlLobby", LobbyType.SqlLobby);
    private LoadBalancingClient loadBalancingClient;

    void Awake()
    {
        
        PhotonNetwork.AutomaticallySyncScene = true;

    }
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("PlayerName"))
            nameField.text = PlayerPrefs.GetString("PlayerName");
    }

    public void OnSelectGameMode(int index)
    {
        if (index == 0)
            gameMode = 0;
        else if(index == 1)
            gameMode = 1;
        else
            gameMode = 2;
        PhotonManager.gameMode = gameMode;
        Debug.LogError(gameMode);
    }

    public void OnTapPlay()
    {
        if (string.IsNullOrWhiteSpace(nameField.text))
        {
            string str = "Player" +Random.Range(11, 99);
            PlayerPrefs.SetString("PlayerName", str);
        }
        else
            PlayerPrefs.SetString("PlayerName", nameField.text);

        
        Connect();
    }

    void Connect()
    {
        isConnecting = true;

        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.NickName = PlayerPrefs.GetString("PlayerName");
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            PhotonNetwork.ConnectUsingSettings();
        }
    }

	public override void OnConnectedToMaster()
	{
		
		if (isConnecting)
		{
            PhotonNetwork.NickName = PlayerPrefs.GetString("PlayerName");
            JoinRandom(gameMode.ToString(), 2);
        }
	}

	public override void OnJoinRandomFailed(short returnCode, string message)
	{
        //PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 2 });
        //PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 2 });
        string level = gameMode.ToString();
        CreateRoom(null, 2, level);
    }

	public override void OnDisconnected(DisconnectCause cause)
	{
		isConnecting = false;

	}

	public override void OnJoinedRoom()
	{
        lobbyPanel.SetActive(true);
        menuPanel.SetActive(false);

        photonView.RPC("UpdateLobbyList", RpcTarget.All);
    }
    public void CreateRoom(string name, byte maxPlayers, string level)
    {
        ExitGames.Client.Photon.Hashtable customPropreties = new ExitGames.Client.Photon.Hashtable();
        customPropreties["Scene"] = level;

        RoomOptions roomOptions = new RoomOptions() { CustomRoomProperties = customPropreties, IsVisible = true, IsOpen = true, MaxPlayers = (byte)maxPlayers, CleanupCacheOnLeave = false };

        roomOptions.CustomRoomPropertiesForLobby = new string[]
        {
             "Scene",
        };

        PhotonNetwork.CreateRoom(null, roomOptions);
    }

    public void JoinRandom(string level, byte maxPlayers)
    {
        ExitGames.Client.Photon.Hashtable customPropreties = new ExitGames.Client.Photon.Hashtable();
        customPropreties["Scene"] = level;
        PhotonNetwork.JoinRandomRoom(customPropreties, maxPlayers);
    }

    void OnPhotonRandomJoinFailed()
    {
        string level = gameMode.ToString();
        CreateRoom(null, 2, level);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdateLobbyList();
    }

    [PunRPC]
    public void UpdateLobbyList()
    {
        playerName1.text = PhotonNetwork.CurrentRoom.GetPlayer(1).NickName;
        playerName2.text = PhotonNetwork.PlayerList.Length == 2 ? PhotonNetwork.CurrentRoom.GetPlayer(2).NickName : "";

        if (PhotonNetwork.PlayerList.Length == 2)
        {
            gameStartingText.gameObject.SetActive(true);
            string scene = "GameScene " + gameMode.ToString();
            Debug.LogError(scene);
            if (PhotonNetwork.IsMasterClient)
                photonView.RPC("ChangeScene", RpcTarget.All, scene);
        }
        else
        {
            gameStartingText.gameObject.SetActive(false);
        }
    }

    [PunRPC]
    public void ChangeScene(string scene)
    {
        PhotonNetwork.LoadLevel(scene);
    }
}
