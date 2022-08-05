using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class NetworkManagerOffice : NetworkManager
{
    [SerializeField] private Transform cam;
    [SerializeField] private Transform ballspawn1;
    [SerializeField] private Transform ballspawn2;
    [SerializeField] private GameObject scribblePanel;

    private GameObject ball1;
    private GameObject ball2;

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

    public override void OnStartServer()
    {
        base.OnStartServer();
        ball1 = Instantiate(spawnPrefabs.Find(prefab => prefab.name == "Ball"));
        //ball2 = spawnPrefabs.Find(prefab => prefab.name == "Ball");
        NetworkServer.Spawn(ball1);
        ball1.transform.position = ballspawn1.position;
        //NetworkServer.Spawn(ball2);
<<<<<<< HEAD
=======


>>>>>>> 7febcf6a220ec30fec5bd883a73064b86d71e86f
    }
}
