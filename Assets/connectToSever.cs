using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class connectToSever : MonoBehaviourPunCallbacks
{
    [SerializeField] int gameVersion;
    void Start()
    {
        PhotonNetwork.GameVersion = gameVersion.ToString();

        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        SceneManager.LoadScene("Lobby");
    }
}
