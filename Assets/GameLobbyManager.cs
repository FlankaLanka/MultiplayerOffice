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
    [SerializeField] private GameObject logoutCanvas;
<<<<<<< HEAD
    [SerializeField] private VivoxLoginCred voiceChat;
=======
>>>>>>> 7febcf6a220ec30fec5bd883a73064b86d71e86f

    public void JoinGame()
    {
        voiceChat.Login(nameField.text);

        PlayerPrefs.SetString("Name", nameField.text);

        string ipAddress = ipAddressField.text;
        netManager.networkAddress = ipAddress;
        netManager.StartClient();

        LoginCanvas.SetActive(false);
        logoutCanvas.SetActive(true);

    }

    public void HostGame()
    {
        voiceChat.Login(nameField.text);

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

        voiceChat.Logout();
    }

    public void UnhostGame()
    {
        netManager.StopHost();
        LoginCanvas.SetActive(true);
        logoutCanvas.SetActive(false);

        voiceChat.Logout();
    }
}
