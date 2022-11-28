using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ProfileManager : StaticInstance<ProfileManager>
{
    [FormerlySerializedAs("inputField")] public TMP_InputField inputFieldName;
    public TMP_Text textName;
    public TMP_InputField inputFieldCard;
    public Button button;
    
    void Start()
    {
        button.onClick.AddListener(SaveData);
    }

    public void SetData()
    {
        textName.text = DataManager.Instance.saveData.name;
        inputFieldName.text = DataManager.Instance.saveData.name;
        inputFieldCard.text = DataManager.Instance.saveData.card;
    }

    private void SaveData()
    {
        DataManager.Instance.saveData.name = inputFieldName.text;
        DataManager.Instance.saveData.card = inputFieldCard.text;
        SetData();
    }

}
