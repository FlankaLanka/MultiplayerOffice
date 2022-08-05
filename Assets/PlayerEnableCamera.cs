using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerEnableCamera : NetworkBehaviour
{
    public Renderer videocamPanel;
    static WebCamTexture backCam;
    public byte[] backCamData;
<<<<<<< HEAD
    private int waited = 0;
=======
    private bool waited = false;
>>>>>>> 7febcf6a220ec30fec5bd883a73064b86d71e86f


    //enables game camera and video camera
    void Start()
    {
        if (isLocalPlayer)
        {
            //game cam
            GameObject cam = GameObject.Find("MainCamera");
            cam.transform.parent = transform;
        }

        //video cam
        if (backCam == null)
            backCam = new WebCamTexture();
        if (!backCam.isPlaying)
            backCam.Play();
<<<<<<< HEAD
    }

    private void FixedUpdate()
    {
        if(isLocalPlayer && waited >= 40)
        {
            waited = 0;
=======
        //StartCoroutine(LateStartVideo());

    }

    private void Update()
    {
        if(isLocalPlayer)// && waited)
        {
>>>>>>> 7febcf6a220ec30fec5bd883a73064b86d71e86f
            Texture2D tex = new Texture2D(backCam.width, backCam.height, TextureFormat.RGB24, false);
            tex.SetPixels(backCam.GetPixels());
            tex.Apply();
            backCamData = tex.EncodeToJPG();
            CmdUpdateVideoCam(backCamData);
        }
<<<<<<< HEAD
        else
        {
            waited++;
        }
    }
=======
    }

    private IEnumerator LateStartVideo()
    {
        yield return new WaitForSeconds(3f);
        waited = true;
    }

>>>>>>> 7febcf6a220ec30fec5bd883a73064b86d71e86f
    
    [Command]
    private void CmdUpdateVideoCam(byte[] videodata)
    {
        RpcUpdateVideoCam(videodata);
    }

    [ClientRpc]
    private void RpcUpdateVideoCam(byte[] videodata)
    {
        if(videodata != null)
        {
<<<<<<< HEAD
            Texture2D tex = new Texture2D(6, 6);
=======
            Texture2D tex = new Texture2D(2, 2);
>>>>>>> 7febcf6a220ec30fec5bd883a73064b86d71e86f
            tex.LoadImage(videodata);
            videocamPanel.material.mainTexture = tex;
        }
    }
}
