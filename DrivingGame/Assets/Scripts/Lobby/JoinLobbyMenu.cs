using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JoinLobbyMenu : MonoBehaviour
{
    [SerializeField] private NetworkManagerLobby networkManager = null;

    [Header("UI")]
    [SerializeField] private GameObject landingPagePanel = null;
    [SerializeField] private TMP_InputField ipAddressInputField = null;
    [SerializeField] private Button joinButton = null;

    private void OnEnable()
    {
        NetworkManagerLobby.OnClientConnected += NetworkManagerLobby_OnClientConnected;
        NetworkManagerLobby.OnClientDisconnected += NetworkManagerLobby_OnClientDisconnected;
    }
    private void OnDisable()
    {
        NetworkManagerLobby.OnClientConnected -= NetworkManagerLobby_OnClientConnected;
        NetworkManagerLobby.OnClientDisconnected -= NetworkManagerLobby_OnClientDisconnected;
    }
    private void NetworkManagerLobby_OnClientDisconnected()
    {
        joinButton.interactable = true;
    }

    private void NetworkManagerLobby_OnClientConnected()
    {
        joinButton.interactable = true;
        gameObject.SetActive(false);
        landingPagePanel.SetActive(false);
    }
    public void JoinLobby()
    {
        string ipAddress = ipAddressInputField.text;
        networkManager.networkAddress = ipAddress;
        networkManager.StartClient();
        joinButton.interactable = false;
    }
}
