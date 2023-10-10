using UnityEngine;
using Photon.Pun;
using TMPro;
using System.Collections.Generic;
using Photon.Realtime;
using Unity.VisualScripting;

public class Launcher : MonoBehaviourPunCallbacks
{
    public static Launcher Instance;
    private void Awake() => Instance = this;


    [SerializeField] TMP_InputField _roomNameInputField;
    [SerializeField] TMP_Text _errorText;
    [SerializeField] TMP_Text _roomNameText;
    [Space]
    [SerializeField] Transform _roomListContent;
    [SerializeField] GameObject _roomListItemPregab;
    private void Start()
    {
        Debug.Log("Connecting to Master");
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master");
        PhotonNetwork.JoinLobby();
    }
    public override void OnJoinedLobby()
    {
        MenuManager.Instance.OpenMenu("title");
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
            Instantiate(_roomListItemPregab, _roomListContent).GetComponent<RoomListItem>().SetUp(roomList[i]);
        }
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
}
