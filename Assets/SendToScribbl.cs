using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;

public class SendToScribbl : MonoBehaviour
{
    [SerializeField] private GameObject ActionText;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<NetworkIdentity>().isLocalPlayer)
            ActionText.SetActive(true);
    }

    private void OnTriggerStay(Collider other)
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if (other.gameObject.GetComponent<NetworkIdentity>().isLocalPlayer)
            {
                LaunchScribbl();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<NetworkIdentity>().isLocalPlayer)
        {
            ActionText.SetActive(false);
        }
    }

    private void LaunchScribbl()
    {
        SceneManager.LoadScene("Scribbl");
    }
}
