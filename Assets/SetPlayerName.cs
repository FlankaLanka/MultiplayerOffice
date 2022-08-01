using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class SetPlayerName : NetworkBehaviour
{
    [SyncVar(hook = nameof(SetName))]
    private string playerName;
    public Material grn;

    private void SetName(string oldname, string newname)
    {
        GetComponentInChildren<Text>().text = newname;
    }


    // Start is called before the first frame update
    public override void OnStartLocalPlayer()
    {
        CmdChangeName();
    }

    [Command(requiresAuthority = false)]
    private void CmdChangeName()
    {
        RpcChangeName();
    }

    [ClientRpc]
    private void RpcChangeName()
    {
        GetComponentInChildren<Text>().text = playerName;
    }

    /*
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.N))
        {
            //ChangeColor();
            if(isLocalPlayer)
            {
                //GetComponent<MeshRenderer>().material = grn;
                CmdChangeColor();
            }
        }
    }

    [Command(requiresAuthority = false)]
    private void CmdChangeColor()
    {
            RpcChangeColorx();
        
    }

    [ClientRpc]
    private void RpcChangeColorx()
    {
            GetComponent<MeshRenderer>().material = grn;
        
    }
    */

}
