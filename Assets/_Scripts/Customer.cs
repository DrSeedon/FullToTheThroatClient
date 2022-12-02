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
        ShopLogic.Instance.readyText.text = "Заберите заказ";
        ShopLogic.Instance.orderComplete?.Invoke();
        ShopLogic.Instance.isOrderWait = false;
        ShopLogic.Instance.payButton.interactable = true;
    }
    
[MessageHandler((ushort)ServerToClientId.issuedFood)]
    private static void FoodIssued(Message message)
    {
        ShopLogic.Instance.orderIssued?.Invoke();
        ShopLogic.Instance.ClearBasket();
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

        if (ShopLogic.Instance.isOrderWait)
            ShopLogic.Instance.SendMessageOrderWait(true);
    }

    #endregion
}