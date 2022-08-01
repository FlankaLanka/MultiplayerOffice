using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class EnableProperLogout : NetworkBehaviour
{
    void Start()
    {
        if(isServer)
        {
            GameObject.Find("LogoutCanvas").transform.Find("QuitButton").gameObject.SetActive(false);
        }
        else
        {
            GameObject.Find("LogoutCanvas").transform.Find("QuitHostButton").gameObject.SetActive(false);
        }
    }
}
