using System.Collections;
using System.Collections.Generic;
using Riptide;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Customer : Singleton<Customer>
{
    #region Messages

    
    [MessageHandler((ushort)ServerToClientId.foodReady)]
    private static void FoodReady(Message message)
    {
        ShopLogic.Instance.readyText.text = "ГОТОВ";
        ShopLogic.Instance.ClearBasket();
        ShopLogic.Instance.payButton.interactable = true;
    }

    /// <summary>
    /// Принять сообщение с едой
    /// </summary>
    /// <param name="message">Сообщение</param>
    [MessageHandler((ushort)ServerToClientId.foodUpdate)]
    private static void FoodDataJsonReceived(Message message)
    {
        List<FoodData> foodDatas = JsonHelper.FromJsonList<FoodData>(message.GetString());
        ShopLogic.Instance.SetFood(foodDatas);
    }

    #endregion
}