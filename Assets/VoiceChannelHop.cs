using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using VivoxUnity;

public class VoiceChannelHop : MonoBehaviour
{
    [SerializeField] private string channelName;
    private VivoxLoginCred voiceManager;

    private void Start()
    {
        voiceManager = FindObjectOfType<VivoxLoginCred>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && other.GetComponent<NetworkIdentity>().isLocalPlayer)
            voiceManager.JoinChannel(channelName);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && other.GetComponent<NetworkIdentity>().isLocalPlayer)
            voiceManager.Leave_Channel();
    }
}
