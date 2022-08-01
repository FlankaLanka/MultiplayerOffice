using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerEnableCamera : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(isLocalPlayer)
        {
            GameObject cam = GameObject.Find("MainCamera");
            cam.transform.parent = this.transform;
        }
    }
}
