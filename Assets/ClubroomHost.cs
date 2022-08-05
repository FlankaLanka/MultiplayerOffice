using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ClubroomHost : NetworkBehaviour
{
    private Transform videoPlane;
    private bool hostingClubroom = false;

    private void Start()
    {
        videoPlane = transform.GetChild(0);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C) && isServer && isLocalPlayer)
        {
            if(!hostingClubroom)
            {
                CmdHostClubroomSettings();
<<<<<<< HEAD
                transform.position = new Vector3(-5.67000008f, 2.73000002f, 30.25f);
=======
                transform.position = new Vector3(-13.1707382f, 1.0799998f, 5.30400372f);
>>>>>>> 7febcf6a220ec30fec5bd883a73064b86d71e86f
                transform.eulerAngles = new Vector3(0, 90, 0);
                GetComponent<PlayerMove>().isHosting = true;
                hostingClubroom = true;
            }
            else
            {
                CmdUnHostClubroomSettings();
                GetComponent<PlayerMove>().isHosting = false;
                hostingClubroom = false;
            }
        }
    }

    [Command]
    private void CmdHostClubroomSettings()
    {
        RpcHostClubroomSettings();
    }

    [Command]
    private void CmdUnHostClubroomSettings()
    {
        RpcUnHostClubroomSettings();
    }

    [ClientRpc]
    private void RpcHostClubroomSettings()
    {
        videoPlane.localScale = new Vector3(0.5f, 1, 0.5f);
    }

    [ClientRpc]
    private void RpcUnHostClubroomSettings()
    {
        videoPlane.localScale = new Vector3(0.1f, 1, 0.1f);
    }
}
