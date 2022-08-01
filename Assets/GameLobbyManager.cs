using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class GameLobbyManager : MonoBehaviour
{
    [SerializeField] private NetworkManager netManager;
    [SerializeField] private GameObject LoginCanvas;
    [SerializeField] private Button joinButton;
    [SerializeField] private Button hostButton;
    [SerializeField] private InputField nameField;
    [SerializeField] private InputField ipAddressField;
    [SerializeField] private PlayerName playerName;
    [SerializeField] private GameObject logoutCanvas;

    private void Start()
    {
        nameField.onValueChanged.AddListener(delegate { UpdateName(); });
    }

    private void UpdateName()
    {
        playerName.pname = nameField.text;
    }

    public void JoinGame()
    {
        PlayerPrefs.SetString("Name", nameField.text);

        string ipAddress = ipAddressField.text;
        netManager.networkAddress = ipAddress;
        netManager.StartClient();

        LoginCanvas.SetActive(false);
        logoutCanvas.SetActive(true);
    }

    public void HostGame()
    {
        PlayerPrefs.SetString("Name", nameField.text);

        netManager.StartHost();

        LoginCanvas.SetActive(false);
        logoutCanvas.SetActive(true);
    }

    public void UnjoinGame()
    {
        netManager.StopClient();
        LoginCanvas.SetActive(true);
        logoutCanvas.SetActive(false);
    }

    public void UnhostGame()
    {
        netManager.StopHost();
        LoginCanvas.SetActive(true);
        logoutCanvas.SetActive(false);
    }
}
