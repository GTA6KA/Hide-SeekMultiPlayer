using Photon.Pun;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private PhotonView PV;
    [SerializeField] private GameObject[] _hunterPlayers;
    private void Awake() => PV = GetComponent<PhotonView>();
    private void Start()
    {
        if (PV.IsMine) CreateController();
    }

    private void CreateController()
    {
        Transform spawnPoint = SpawnManager.Instance.GetSpawnPoint();

        PhotonNetwork.Instantiate(_hunterPlayers[Random.Range(0, _hunterPlayers.Length)].name, spawnPoint.position, spawnPoint.rotation);

    }
}
