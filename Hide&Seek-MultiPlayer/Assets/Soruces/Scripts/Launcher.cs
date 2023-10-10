using UnityEngine;
using Photon.Pun;
using TMPro;
using System.Collections.Generic;
using Photon.Realtime;
using Unity.VisualScripting;
using System.Linq;

public class Launcher : MonoBehaviourPunCallbacks
{
    public static Launcher Instance;
    private void Awake() => Instance = this;


    [SerializeField] private TMP_InputField _roomNameInputField;
    [SerializeField] private TMP_Text _errorText;
    [SerializeField] private TMP_Text _roomNameText;
    [Space]
    [SerializeField] private Transform _roomListContent;
    [SerializeField] private GameObject _roomListItemPregab;
    [Space]
    [SerializeField] private Transform _playerListContent;
    [SerializeField] private GameObject _playerListItemPregab;
    [SerializeField] private GameObject _startGameButton;
    private void Start()
    {
        Debug.Log("Connecting to Master");
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master");
        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    public override void OnJoinedLobby()
    {
        MenuManager.Instance.OpenMenu("title");
        PhotonNetwork.NickName = "Player " + Random.Range(0, 1000).ToString("0000");
        Debug.Log("Joined Lobby");
    }
    public void CreateRoom()
    {
        if (string.IsNullOrEmpty(_roomNameInputField.text)) return;
        PhotonNetwork.CreateRoom(_roomNameInputField.text);
        MenuManager.Instance.OpenMenu("loading");
    }
    public override void OnJoinedRoom()
    {
        MenuManager.Instance.OpenMenu("room");
        _roomNameText.text = PhotonNetwork.CurrentRoom.Name;

        Player[] players = PhotonNetwork.PlayerList;

        foreach(Transform child in _playerListContent)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < players.Count(); i++)
        {
            Instantiate(_playerListItemPregab, _playerListContent).GetComponent<PlayerListItem>().SetUp(players[i]);
        }
        _startGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        _startGameButton.SetActive(PhotonNetwork.IsMasterClient); 
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        _errorText.text = "Room Creation Failed: " + message;
        MenuManager.Instance.OpenMenu("error");
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach(Transform trans in _roomListContent)
        {
            Destroy(trans.gameObject);
        }

        for (int i = 0; i < roomList.Count; i++)
        {
            if (roomList[i].RemovedFromList) continue;
            Instantiate(_roomListItemPregab, _roomListContent).GetComponent<RoomListItem>().SetUp(roomList[i]);
        }
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Instantiate(_playerListItemPregab, _playerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer);
    }

    public void JoinRoom(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);
        MenuManager.Instance.OpenMenu("loading"); 
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        MenuManager.Instance.OpenMenu("loading");
    }
    public override void OnLeftRoom()
    {
        MenuManager.Instance.OpenMenu("title");
    }
    public void StartGame()
    {
        PhotonNetwork.LoadLevel("GameScene");
    }
}
