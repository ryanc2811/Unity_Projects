using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoundSystem : NetworkBehaviour
{
    [SerializeField] private Animator animator = null;

    private NetworkManagerLobby room;

    public NetworkManagerLobby Room
    {
        get
        {
            if (room != null) { return room; }
            return room = NetworkManager.singleton as NetworkManagerLobby;
        }
    }

    public  void CoundownEnded()
    {
        animator.enabled = false;
    }
    #region Server
    public override void OnStartServer()
    {
        NetworkManagerLobby.OnServerStopped += CleanUpServer;
        NetworkManagerLobby.OnServerReadied += CheckToStartRound;
    }

    private void CheckToStartRound(NetworkConnection conn)
    {
        if (Room.gamePlayers.Count(x => x.connectionToClient.isReady) != Room.gamePlayers.Count) { return; }

        animator.enabled = true;
        RpcStartCountdown();
    }

    [ServerCallback]
    private void StartRound()
    {
        RpcStartRound();
    }

    [ServerCallback]
    private void OnDestroy() => CleanUpServer();
    [Server]
    private void CleanUpServer()
    {
        NetworkManagerLobby.OnServerStopped -= CleanUpServer;
        NetworkManagerLobby.OnServerReadied -= CheckToStartRound;
    }
    #endregion
    #region Client
    [ClientRpc]
    private void RpcStartRound()
    {
        Debug.Log("Start");
        InputManager.Remove(ActionMapNames.Player);
    }
    [ClientRpc]
    private void RpcStartCountdown()
    {
        
        animator.enabled = true;
    }

    #endregion
}
