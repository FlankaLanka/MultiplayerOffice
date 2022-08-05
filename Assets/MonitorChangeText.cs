using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class MonitorChangeText : NetworkBehaviour
{
    [SerializeField] private Text subjectText;
    [SerializeField] private InputField subjectInput;
    [SerializeField] private Button subjectButton;
    private bool activeInScene = false;

    [SyncVar(hook = nameof(SetSubject))]
    public string subject;

    private void SetSubject(string oldsubject, string newsubject)
    {
        subjectText.text = newsubject;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            if (activeInScene)
            {
                subjectInput.gameObject.SetActive(false);
                subjectButton.gameObject.SetActive(false);
                activeInScene = false;
            }
            else
            {
                subjectInput.gameObject.SetActive(true);
                subjectButton.gameObject.SetActive(true);
                activeInScene = true;
            }
        }
    }


    public void SendSubjectToServer()
    {
        CmdChangeText(subjectInput.text);
    }

    [Command(requiresAuthority = false)]
    private void CmdChangeText(string s)
    {
        subject = s;
    }
}
