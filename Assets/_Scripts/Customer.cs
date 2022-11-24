using System.Collections;
using System.Collections.Generic;
using Riptide;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Customer : Singleton<Customer>
{
    public List<Order> orders = new List<Order>();
    public string name;
    public string cardNumber;

    public List<FoodData> foodDatas = new List<FoodData>();
    #region Messages

    
    [MessageHandler((ushort)ServerToClientId.foodReady)]
    private static void FoodReady(Message message)
    {
        List<FoodData> foodDatas = JsonHelper.FromJsonList<FoodData>(message.GetString());

        if (ShopLogic.Instance.foodDataBasket == foodDatas)
            ShopLogic.Instance.readyText.text = "ГОТОВ";

    }

    /// <summary>
    /// Принять сообщение с едой
    /// </summary>
    /// <param name="message">Сообщение</param>
    [MessageHandler((ushort)ServerToClientId.foodUpdate)]
    private static void FoodDataJsonReceived(Message message)
    {
        List<FoodData> foodDatas = JsonHelper.FromJsonList<FoodData>(message.GetString());
        Instance.foodDatas = foodDatas;
        ShopLogic.Instance.SetFood(foodDatas);
    }

    #endregion
}