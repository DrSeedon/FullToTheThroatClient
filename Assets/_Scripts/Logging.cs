using System.Collections;
using System.Collections.Generic;
using Riptide;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class Logging : StaticInstance<Logging>
{
    public TMP_InputField LoginField;
    public TMP_InputField PasswordField;
    public TMP_Text statusText;
    public UnityEvent LoggingEvent;
    
    public class LoginData
    {
        public string login;
        public string password;
        public string name;
    }

    public void EraseFields()
    {
        LoginField.text = "";
        PasswordField.text = "";
        statusText.text = "";
    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SendLogging()
    {
        LoginData loginData = new LoginData();
        loginData.login = LoginField.text;
        loginData.password = PasswordField.text;
        
        string jsonString = JsonUtility.ToJson(loginData, true);
        Message message = Message.Create(MessageSendMode.Reliable, (ushort) ClientToServerId.loggingRequest);
        message.AddString(jsonString);
        NetworkManager.Instance.Client.Send(message);
        Instance.statusText.text = "Подключение";
    }

    [MessageHandler((ushort) ServerToClientId.loggingResponse)]
    private static void FoodDataJsonReceived(Message message)
    {
        bool isCorrect = message.GetBool();
        Instance.statusText.text = isCorrect ? "Пароль верен" : "Неверные данные";
        if (isCorrect)
        {
            Instance.LoggingEvent?.Invoke();
        }
    }

    
    
}
