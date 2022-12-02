using System;
using System.Collections;
using System.Collections.Generic;
using Riptide;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ShopLogic : Singleton<ShopLogic>
{
    public List<FoodData> foodData = new List<FoodData>();
    public List<GameObject> foodDataGameObjects = new List<GameObject>();
    public Order order = new Order();
    
    public List<FoodData> foodDataBasket = new List<FoodData>();
    public List<GameObject> foodDataBasketGameObjects = new List<GameObject>();
    public List<GameObject> foodDataOrderGameObjects = new List<GameObject>();

    public GameObject prefabDataElement;
    public List<GameObject> parentDataElement;
    
    public GameObject prefabDataElementBasket;
    public GameObject parentDataElementBasket;
    
    public GameObject prefabDataElementOrder;
    public GameObject parentDataElementOrder;

    public TMP_Text readyText;
    public UnityEvent orderStart;
    public UnityEvent orderComplete;
    public UnityEvent orderIssued;
    public TMP_Text orderNumberText;
    public TMP_Text totalText;
    public TMP_Text totalText2;
    public TMP_Text countBasketText;
    public Button basketButton;

    public Button payButton;

    private void Start()
    {
        payButton.onClick.AddListener(SendFoodDataJson);
    }

    public void SetFood(List<FoodData> datas)
    {
        foodData.Clear();
        foreach (var data in datas)
        {
            if (data.isAvailable)
                foodData.Add(data);
        }
        CreateElements();
    }

    public void ClearBasket()
    {
        order.orderRows.Clear();
        UpdateBasket();
    }
    
    
    protected virtual void CreateElements()
    {
        foreach (var foodDataGameObject in foodDataGameObjects)
        {
            Destroy(foodDataGameObject);
        }
        foodDataGameObjects.Clear();
        
        foreach (var data in foodData)
        {
            var obj = Instantiate(prefabDataElement, parentDataElement[data.idСategory].transform);
            obj.SetActive(true);
            var dataElement = obj.GetComponent<FoodElement>();
            dataElement.SetData(data);
            foodDataGameObjects.Add(obj);
        }
    }

    public int AddToBasket(FoodData data)
    {
        bool isExist = false;
        int newCount = 1;
        foreach (var orderRow in order.orderRows)
        {
            if (orderRow.foodData == data)
            {
                isExist = true;
                orderRow.count++;
                newCount = orderRow.count;
            }
        }
        
        if(!isExist)
            order.orderRows.Add(new OrderRow(data, newCount));

        UpdateBasket();
        return newCount;
    }

    public int CountCalculate(FoodData data)
    {
        foreach (var orderRow in order.orderRows)
        {
            if (orderRow.foodData == data)
            {
                return orderRow.count;
            }
        }
        return 0;
    }

    public int RemoveFromBasket(FoodData data)
    {
        int newCount = 0;
        bool isDelete = false;
        OrderRow orderRowDeleted = new OrderRow();
        
        foreach (var orderRow in order.orderRows)
        {
            if (orderRow.foodData == data)
            {
                if (orderRow.count <= 1)
                {
                    isDelete = true;
                    orderRowDeleted = orderRow;
                }
                else
                {
                    orderRow.count--;
                    newCount = orderRow.count;
                }
            }
        }
        
        if(isDelete)
            order.orderRows.Remove(orderRowDeleted);
        
        UpdateBasket();
        return newCount;
    }

    private void CalculatePrises()
    {
        order.totalPrice = 0;
        foreach (var orderRow in order.orderRows)
        {
            orderRow.totalPriceRow = orderRow.count * orderRow.foodData.price;
            order.totalPrice += orderRow.totalPriceRow;
        }
        totalText.text = order.totalPrice + " руб.";
        totalText2.text = order.totalPrice + " руб.";
    }


    private void UpdateBasket()
    {
        
        foreach (var gm in foodDataBasketGameObjects)
        {
            Destroy(gm);
        }
        foodDataBasketGameObjects.Clear();

        CalculatePrises();
        int count = 0;
        foreach (var orderRow in order.orderRows)
        {
            var obj = Instantiate(prefabDataElementBasket, parentDataElementBasket.transform);
            obj.SetActive(true);
            var dataElement = obj.GetComponent<BasketElement>();
            dataElement.SetData(orderRow.foodData, orderRow.count);
            foodDataBasketGameObjects.Add(obj);
            count += orderRow.count;
        }

        countBasketText.text = count.ToString();
        basketButton.interactable = true;
        if (count == 0)
        {
            countBasketText.text = "";
            basketButton.interactable = false;
        }
    }

    private void CreateOrderElements()
    {
        foreach (var gm in foodDataOrderGameObjects)
        {
            Destroy(gm);
        }

        foodDataOrderGameObjects.Clear();

        foreach (var orderRow in order.orderRows)
        {
            var obj = Instantiate(prefabDataElementOrder, parentDataElementOrder.transform);
            obj.SetActive(true);
            var dataElement = obj.GetComponent<BasketElement>();
            dataElement.SetData(orderRow.foodData, orderRow.count);
            foodDataOrderGameObjects.Add(obj);
        }
        
    }

    public bool isOrderWait = false;
    public void SendFoodDataJson()
    {
        if(order.orderRows.Count == 0)
            return;
        SendMessageOrderWait();
        isOrderWait = true;
        payButton.interactable = false;
        readyText.text = "Ожидайте";
        orderStart?.Invoke();
    }

    public void SendMessageOrderWait(bool isOld = false)
    {
        string jsonString = JsonUtility.ToJson(order, true);
        Message message = Message.Create(MessageSendMode.Reliable, (ushort) ClientToServerId.foodDataJson);
        message.AddString(jsonString);
        message.AddBool(isOld);

        NetworkManager.Instance.Client.Send(message);
        CreateOrderElements();
    }

    [MessageHandler((ushort) ServerToClientId.orderNumberResponse)]
    private static void OrderNumberResponse(Message message)
    {
        Instance.orderNumberText.text = "№ " + message.GetInt();
    }
}
