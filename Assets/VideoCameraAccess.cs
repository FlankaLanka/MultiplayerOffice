using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoCameraAccess : MonoBehaviour
{
    static WebCamTexture backCam;

    void Start()
    {
        if (backCam == null)
            backCam = new WebCamTexture();
        GetComponent<Renderer>().material.mainTexture = backCam;
        if (!backCam.isPlaying)
            backCam.Play();
    }
}
