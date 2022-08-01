using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class NetworkManagerOffice : NetworkManager
{
    [SerializeField] private Transform cam;

    public override void OnStopClient()
    {
        base.OnStopClient();
        cam.parent = null;
    }

    public override void OnStopHost()
    {
        base.OnStopHost();
        cam.parent = null;
    }

    public override void OnClientConnect()
    {
        base.OnClientConnect();
        
    }
}
