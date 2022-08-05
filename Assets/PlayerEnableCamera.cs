using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerEnableCamera : NetworkBehaviour
{
    public Renderer videocamPanel;
    static WebCamTexture backCam;
    public byte[] backCamData;
    private int waited = 0;


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
    }

    private void FixedUpdate()
    {
        if(isLocalPlayer && waited >= 40)
        {
            waited = 0;
            Texture2D tex = new Texture2D(backCam.width, backCam.height, TextureFormat.RGB24, false);
            tex.SetPixels(backCam.GetPixels());
            tex.Apply();
            backCamData = tex.EncodeToJPG();
            CmdUpdateVideoCam(backCamData);
        }
        else
        {
            waited++;
        }
    }
    
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
            Texture2D tex = new Texture2D(6, 6);
            tex.LoadImage(videodata);
            videocamPanel.material.mainTexture = tex;
        }
    }
}
