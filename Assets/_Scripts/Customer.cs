using System.Collections;
using System.Collections.Generic;
using Riptide;
using Unity.VisualScripting;
using UnityEngine;

public class Customer : Singleton<Customer>
{
    public List<Order> orders = new List<Order>();
    public string name;
    public string cardNumber;
    
    public List<FoodData> foodDatas = new List<FoodData>();
    #region Messages

    /// <summary>
    /// Принять сообщение о спавне префаба клиента
    /// </summary>
    /// <param name="message"></param>
    [MessageHandler((ushort)ServerToClientId.playerSpawned)]
    private static void SpawnClient(Message message)
    {
        //Spawn(message.GetUShort(), message.GetString());
    }

    /// <summary>
    /// Принять сообщение с едой
    /// </summary>
    /// <param name="message">Сообщение</param>
    [MessageHandler((ushort)ServerToClientId.foodUpdate)]
    private static void FoodDataJsonReceived(Message message)
    {
        List<FoodData> foodDatas = JsonUtility.FromJson<List<FoodData>>(message.GetString());
        Instance.foodDatas = foodDatas;
        foreach (var data in foodDatas)
        {
            LogHelper.Log(() => data.name);
            LogHelper.Log(() => data.price);
        }

        Debug.Log("hui");
    }

    #endregion
}