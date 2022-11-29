using System;
using System.Collections.Generic;

[Serializable]
public class FoodData
{
    public string name;
    public int price;
    public bool isAvailable = false;
    public int idImage;
    public string weight;
    public string composition;
    public int idСategory;
}
[Serializable]
public class OrderRow
{
    public FoodData foodData = new FoodData();
    public int count;
    public int totalPriceRow;

    public OrderRow(FoodData data, int count)
    {
        foodData = data;
        this.count = count;
    }
    
    public OrderRow()
    {
    }
}

[Serializable]
public class Order
{
    public List<OrderRow> orderRows = new List<OrderRow>();
    public int totalPrice;
    public int numberOrder;
    public bool isReady = false;
    public ushort idClient;
}