using System.Collections;
using System.Collections.Generic;
using Riptide;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ShopLogic : Singleton<ShopLogic>
{
    public List<FoodData> foodData = new List<FoodData>();
    public Order order = new Order();
    
    public List<FoodData> foodDataBasket = new List<FoodData>();
    public List<GameObject> gameObjectsBasket = new List<GameObject>();

    public GameObject prefabDataElement;
    public GameObject parentDataElement;
    
    public GameObject prefabDataElementBasket;
    public GameObject parentDataElementBasket;

    public TMP_Text readyText;

    public void SetFood(List<FoodData> datas)
    {
        foreach (var data in datas)
        {
            if (data.isAvailable)
                foodData.Add(data);
        }
        CreateElements();
    }
    
    
    protected virtual void CreateElements()
    {
        foreach (var data in foodData)
        {
            var obj = Instantiate(prefabDataElement, parentDataElement.transform);
            obj.SetActive(true);
            var dataElement = obj.GetComponent<FoodElement>();
            dataElement.SetData(data);
        }
    }

    public void AddToBasket(FoodData data)
    {
        bool isExist = false;
        foreach (var orderRow in order.orderRows)
        {
            if (orderRow.foodData == data)
            {
                isExist = true;
                orderRow.count++;

            }
        }

        if (!isExist)
        {
            order.orderRows.Add(new OrderRow(data, 1));
        }
            
        
        foodDataBasket.Add(data);
        UpdateBasket();
    }

    public void RemoveFromBasket(FoodData data)
    {
        foreach (var orderRow in order.orderRows)
        {
            if (orderRow.foodData == data)
            {
                if (orderRow.count <= 0)
                {
                    order.orderRows.Remove(orderRow);
                }
                else
                {
                    orderRow.count--;
                }
            }
        }
        
        
        foodDataBasket.Remove(data);
        UpdateBasket();
    }

    private void UpdateBasket()
    {
        foreach (var gm in gameObjectsBasket)
        {
            Destroy(gm);
        }
        gameObjectsBasket.Clear();
        
        foreach (var data in foodDataBasket)
        {
            var obj = Instantiate(prefabDataElementBasket, parentDataElementBasket.transform);
            obj.SetActive(true);
            var dataElement = obj.GetComponent<FoodElement>();
            dataElement.SetData(data);
            gameObjectsBasket.Add(obj);
        }
    }

    public void SendFoodDataJson()
    {
        string jsonString = JsonHelper.ToJson(foodDataBasket, true);
        Message message = Message.Create(MessageSendMode.Reliable, (ushort) ClientToServerId.foodDataJson);
        message.AddString(jsonString);
        NetworkManager.Instance.Client.Send(message);
    }
}
