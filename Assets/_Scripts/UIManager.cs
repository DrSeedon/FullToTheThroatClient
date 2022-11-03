using System;
using System.Collections;
using System.Collections.Generic;
using Riptide;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [Header("Connect")] 
    
    public GameObject connectUI;
    public TMP_InputField usernameField;   
    public TMP_InputField ipField;   
    public TMP_InputField portField;   
    
    public GameObject sendMessageUI;
    public TMP_InputField messageTextField;
    
    
    public void ButtonClickConnectClient()
    {
        usernameField.interactable = false;
        connectUI.SetActive(false);

        NetworkManager.Instance.Connect(ipField.text, Convert.ToUInt16(portField.text));
    }

    public void ConnectedMenu()
    {
        sendMessageUI.SetActive(true);
    }
    
    public void ButtonClickSendMessage()
    {
        SendMessageText();
    }

    public void BackToMain()
    {
        usernameField.interactable = true;
        connectUI.SetActive(true);
        sendMessageUI.SetActive(false);
    }
    
    /// <summary>
    /// Отправляем данные на сервер
    /// </summary>
    public void SendName()
    {
        Message message = Message.Create(MessageSendMode.Reliable, (ushort) ClientToServerId.name);
        message.AddString(usernameField.text);
        NetworkManager.Instance.Client.Send(message);
    }
    /// <summary>
    /// Отправляем текстовые данные на сервер
    /// </summary>
    public void SendMessageText()
    {
        Message message = Message.Create(MessageSendMode.Reliable, (ushort) ClientToServerId.message);
        message.AddString(messageTextField.text);
        NetworkManager.Instance.Client.Send(message);
    }
}
