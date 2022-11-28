using System;
using System.Collections;
using System.Collections.Generic;
using Riptide;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : StaticInstance<UIManager>
{
    [Header("Connect")] 
    
    public string usernameField;

    public TMP_InputField ipInput;
    public TMP_InputField portInput;
    
    private void Start()
    {
        
    }

    public void ShowData()
    {
        ipInput.text = DataManager.Instance.saveData.ip;
        portInput.text = DataManager.Instance.saveData.port.ToString();
    }

    public void ConnectClient()
    {
        DataManager.Instance.saveData.ip = ipInput.text;
        DataManager.Instance.saveData.port = ushort.Parse(portInput.text);
    }

    public void ButtonClickSendMessage()
    {
        SendMessageText();
        SendFoodDataJson();
    }

    /// <summary>
    /// Отправляем данные на сервер
    /// </summary>
    public void SendName()
    {
        Message message = Message.Create(MessageSendMode.Reliable, (ushort) ClientToServerId.name);
        message.AddString(usernameField);
        NetworkManager.Instance.Client.Send(message);
    }
    /// <summary>
    /// Отправляем текстовые данные на сервер
    /// </summary>
    public void SendMessageText()
    {
        Message message = Message.Create(MessageSendMode.Reliable, (ushort) ClientToServerId.message);
        //message.AddString(messageTextField.text);
        NetworkManager.Instance.Client.Send(message);
    }
    /// <summary>
    /// Отправляем данные еды на сервер
    /// </summary>
    public void SendFoodDataJson()
    {
        FoodData foodData = new FoodData();
        //foodData.name = foodNameInputField.text;
        //foodData.price = Convert.ToInt32(foodCountInputField.text);
        string jsonString = JsonUtility.ToJson(foodData, true);
        
        Message message = Message.Create(MessageSendMode.Reliable, (ushort) ClientToServerId.foodDataJson);
        message.AddString(jsonString);
        NetworkManager.Instance.Client.Send(message);
    }
    
}
