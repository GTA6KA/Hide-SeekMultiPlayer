using Photon.Pun;
using System.IO;
using UnityEngine;
public class PlayerManager : MonoBehaviour
{
    private PhotonView PV;

    private void Awake() => PV = GetComponent<PhotonView>();
    private void Start()
    {
        if (PV.IsMine) CreateController(); 
    }

    private void CreateController()
    {
        Transform spawnPoint = SpawnManager.Instance.GetSpawnPoint();

        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController"), spawnPoint.position, spawnPoint.rotation);
    }
}
