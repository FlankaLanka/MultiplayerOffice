using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class SetPlayerName : NetworkBehaviour
{
    [SyncVar(hook = nameof(SetName))]
    public string playerName;
    [SerializeField] private Text nametag;
    
    private void SetName(string oldname, string newname)
    {
        nametag.text = newname;
    }

    public void Start()
    {
        if(isLocalPlayer)
        {
            CmdChangeName(PlayerPrefs.GetString("Name"));
        }
    }

    [Command(requiresAuthority = false)]
    private void CmdChangeName(string pname)
    {
        playerName = pname;
    }
}
