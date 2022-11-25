using System;
using System.Collections;
using System.Collections.Generic;
using Riptide;
using Riptide.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public enum ServerToClientId : ushort
{
    playerSpawned = 1,
    foodUpdate = 2,
    foodReady = 3,
}

public enum ClientToServerId : ushort
{
    name = 1,
    message = 2,
    foodDataJson = 3,
}

public class NetworkManager : Singleton<NetworkManager>
{
    public Client Client { get; private set; }

    public string ip;
    public ushort port;

    public TMP_Text statusText;

    public UnityEvent onConnected;
    public UnityEvent onDisconnected;
    
    private void Start()
    {
        RiptideLogger.Initialize(Debug.Log, Debug.Log, Debug.LogWarning, Debug.LogError, false);
        Client = new Client();
        Client.Connected += DidConnect;
        Client.ConnectionFailed += FailedToConnect;
        Client.ClientDisconnected += CustomerLeft;
        Client.Disconnected += DidDisconnect;
        Connect();
    }

    public void Connect()
    {
        Client.Connect($"{ip}:{port}");
        statusText.text += "Connecting...\n";
    }

    private void FixedUpdate()
    {
        Client.Update();
    }

    protected override void OnApplicationQuit()
    {
        Client.Disconnect();
    }

    #region Event

    /// <summary>
    /// Если приконнектились
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void DidConnect(object sender, EventArgs e)
    {
        UIManager.Instance.SendName();
        statusText.text += "Connected\n";
        onConnected?.Invoke();
    }

    private void FailedToConnect(object sender, EventArgs e)
    {
        statusText.text += "Failed To Connect\n";
        TryConnect();
    }

    private void CustomerLeft(object sender, ClientDisconnectedEventArgs e)
    {
        
    }

    private void DidDisconnect(object sender, EventArgs e)
    {
        statusText.text += "Did Disconnect\n";
        onDisconnected?.Invoke();
        TryConnect();
    }

    #endregion

    private void TryConnect()
    {
        Connect();
    }
}