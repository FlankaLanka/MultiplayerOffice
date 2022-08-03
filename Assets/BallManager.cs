using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class BallManager : NetworkBehaviour
{
    public bool pickedUp = false;
    public GameObject parentPlayer;
    public Transform parentBallPos;
    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(pickedUp)
        {
            CmdTrackBallPosition();
        }
        if (Input.GetKeyDown(KeyCode.T) && pickedUp && parentPlayer.GetComponent<NetworkIdentity>().isLocalPlayer)
        {
            CmdLaunchBall();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!pickedUp)
        {
            if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.R) && other.GetComponent<NetworkIdentity>().isLocalPlayer)
            {
                CmdBallPickUp(other.gameObject);
            }
        }
    }

    [Command(requiresAuthority = false)]
    private void CmdTrackBallPosition()
    {
        transform.position = parentBallPos.position;
    }

    [Command(requiresAuthority = false)]
    private void CmdBallPickUp(GameObject otherPlayer)
    {
        RpcSetBallToPlayer(otherPlayer);
    }

    [Command(requiresAuthority = false)]
    private void CmdLaunchBall()
    {
        RpcLaunchBall();
        pickedUp = false;
        rb.isKinematic = false;
        Vector3 ballDirection = parentPlayer.transform.forward;
        ballDirection.y += 1f;
        rb.AddForce(ballDirection.normalized * 10f, ForceMode.Impulse);
    }

    [ClientRpc]
    private void RpcLaunchBall()
    {
        pickedUp = false;
        rb.isKinematic = false;
    }

    [ClientRpc]
    private void RpcSetBallToPlayer(GameObject otherPlayer)
    {
        parentPlayer = otherPlayer;
        parentBallPos = parentPlayer.transform.Find("BallTransform").transform;
        pickedUp = true;
        rb.isKinematic = true;
    }

}
