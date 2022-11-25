using System;
using Riptide;
using Riptide.Utils;
using UnityEngine;

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

    private void Start()
    {
        
    }

    public void Connect()
    {
        RiptideLogger.Initialize(Debug.Log, Debug.Log, Debug.LogWarning, Debug.LogError, false);
        Client = new Client();
        Client.Connected += DidConnect;
        Client.ConnectionFailed += FailedToConnect;
        Client.ClientDisconnected += CustomerLeft;
        Client.Disconnected += DidDisconnect;
    }

    private void FixedUpdate()
    {
        Client.Update();
    }

    protected override void OnApplicationQuit()
    {
        Client.Disconnect();
    }

    public void Connect(string _ip, ushort _port)
    {
        Client.Connect($"{_ip}:{_port}");
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
    }

    private void FailedToConnect(object sender, EventArgs e)
    {
        
    }

    private void CustomerLeft(object sender, ClientDisconnectedEventArgs e)
    {
        
    }

    private void DidDisconnect(object sender, EventArgs e)
    {
        
    }

    #endregion
}