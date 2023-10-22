using Photon.Pun;
using System.IO;
using UnityEngine;
public class PlayerManager : MonoBehaviour
{
    private PhotonView PV;
    [SerializeField] private GameObject[] _players; 

    private void Awake() => PV = GetComponent<PhotonView>();
    private void Start()
    {
        if (PV.IsMine) CreateController();
    }

    private void CreateController()
    {
        Transform spawnPoint = SpawnManager.Instance.GetSpawnPoint();

        PhotonNetwork.Instantiate(_players[Random.Range(0, _players.Length)].name, spawnPoint.position, spawnPoint.rotation);
        
    }
}
