using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
public class MenuManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private InputField _createInput;
    [SerializeField] private InputField _joinInput;
    public void CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 20;
        PhotonNetwork.CreateRoom(_createInput.text, roomOptions);
    }
    public void JoinRoom() => PhotonNetwork.JoinRoom(_joinInput.text);
    public override void OnJoinedRoom() => PhotonNetwork.LoadLevel("GameScene");
}
