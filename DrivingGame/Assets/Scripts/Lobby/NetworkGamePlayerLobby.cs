using Mirror;
using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NetworkGamePlayerLobby : NetworkBehaviour
{

    [SyncVar]
    private string displayName = "Loading...";

    private NetworkManagerLobby room;

    public NetworkManagerLobby Room
    {
        get
        {
            if (room!=null){ return room; }
            return room = NetworkManager.singleton as NetworkManagerLobby;
        }
    }
    public override void OnStartClient()
    {
        DontDestroyOnLoad(gameObject);
        Room.gamePlayers.Add(this);
    }

    public override void OnNetworkDestroy()
    {
        Room.gamePlayers.Remove(this);
    }
    [Server]
    public void SetDisplayName(string displayName)
    {
        this.displayName = displayName;
    }
}
