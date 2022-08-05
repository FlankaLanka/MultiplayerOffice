using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VivoxUnity;
using System;
using System.ComponentModel;
using UnityEngine.UI;

public class VivoxLoginCred : MonoBehaviour
{
    Client client;
    private Uri server = new Uri("https://mt1s.www.vivox.com/api2");
    private string issuer = "franky2445-of38-dev";
    private string domain = "mt1s.vivox.com";
    private string tokenKey = "bonk662";
    private TimeSpan timeSpan = TimeSpan.FromSeconds(90);
    //private AsyncCallback loginCallback;

    private ILoginSession loginSession;
    private IChannelSession channelSession;
    

    public InputField tmp_Input_ChannelName;
    public InputField tmp_Input_LoginName;

    private void Awake()
    {
        client = new Client();
        client.Uninitialize();
        client.Initialize();
        DontDestroyOnLoad(this);
    }


    private void OnApplicationQuit()
    {
        client.Uninitialize();
    }

    private void Start()
    {
        //loginCallback = new AsyncCallback(LoginResult);
    }

    public void Login(string username)
    {
        AccountId accountId = new AccountId(issuer, username, domain);
        loginSession = client.GetLoginSession(accountId);
        BindLoginCallbackListeners(true, loginSession);
        loginSession.BeginLogin(server, loginSession.GetLoginToken(tokenKey, timeSpan), ar =>
        {
            try
            {
                loginSession.EndLogin(ar);
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }
        });
    }

    public void Logout()
    {
        loginSession.Logout();
        BindLoginCallbackListeners(true, loginSession);
    }

    public void BindLoginCallbackListeners(bool bind, ILoginSession loginSesh)
    {
        if (bind)
        {
            loginSesh.PropertyChanged += LoginStatus;
        }
        else
        {
            loginSesh.PropertyChanged -= LoginStatus;
        }
    }


    public void LoginStatus(object sender, PropertyChangedEventArgs loginArgs)
    {
        var source = (ILoginSession)sender;
        switch(source.State)
        {
            case LoginState.LoggingIn:
                Debug.Log("LoggingIn");
                break;
            case LoginState.LoggedIn:
                Debug.Log("LoggedIn");
                break;
            default:
                break;
        }
    }


    public void Bind_Channel_Callback_Listeners(bool bind, IChannelSession channelSesh)
    {
        if (bind)
        {
            channelSesh.PropertyChanged += On_Channel_Status_Changed;
        }
        else
        {
            channelSesh.PropertyChanged -= On_Channel_Status_Changed;
        }
    }

    public void JoinChannel(string channelName)
    {

        ChannelId channelId = new ChannelId(issuer, channelName, domain, ChannelType.NonPositional);
        channelSession = loginSession.GetChannelSession(channelId);
        Bind_Channel_Callback_Listeners(true, channelSession);

        channelSession.BeginConnect(true, true, true, channelSession.GetConnectToken(tokenKey, timeSpan), ar =>
        {
            try
            {
                channelSession.EndConnect(ar);
            }
            catch (Exception e)
            {
                Bind_Channel_Callback_Listeners(false, channelSession);
                Debug.Log(e.Message);
            }
        });
    }

    public void Leave_Channel()
    {
        string channelName = channelSession.Channel.Name;
        channelSession.Disconnect();
        loginSession.DeleteChannelSession(new ChannelId(issuer, channelName, domain));
    }

    public void On_Channel_Status_Changed(object sender, PropertyChangedEventArgs channelArgs)
    {
        IChannelSession source = (IChannelSession)sender;

        switch (source.ChannelState)
        {
            case ConnectionState.Connecting:
                Debug.Log("Channel Connecting");
                break;
            case ConnectionState.Connected:
                Debug.Log($"{source.Channel.Name} Connected");
                break;
            case ConnectionState.Disconnecting:
                Debug.Log($"{source.Channel.Name} disconnecting");
                break;
            case ConnectionState.Disconnected:
                Debug.Log($"{source.Channel.Name} disconnected");
                break;
        }
    }
}
