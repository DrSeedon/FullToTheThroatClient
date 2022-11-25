using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class Logging : MonoBehaviour
{
    public TMP_InputField LoginField;
    public TMP_InputField PasswordField;
    public UnityEvent LoggingEvent;
    public string login;
    public string passvord;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Loggin()
    {
        if (LoginField.text == login && PasswordField.text == passvord)
        {
            LoggingEvent?.Invoke();
        }
    }
}
