using System.Collections.Generic;

public class FoodData
{
    public string name;
    public int price;
}

public class OrderRow
{
    public FoodData foodData = new FoodData();
    public int count;
    public int totalPriceRow;
}

public class Order
{
    public List<OrderRow> orderRows = new List<OrderRow>();
    public int totalPrice;
    public int numberOrder;
    public bool isReady = false;
}