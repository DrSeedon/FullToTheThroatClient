using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FoodElement : MonoBehaviour
{
    public TMP_Text titleText;
    public FoodData foodData;
    public Button addFood;
    public Button removeFood;

    private void Start()
    {
        if (addFood != null) addFood.onClick.AddListener(AddToBasket);
        if (removeFood != null) removeFood.onClick.AddListener(RemoveFromBasket);
    }

    public void AddToBasket()
    {
        ShopLogic.Instance.AddToBasket(foodData);
    }

    public void RemoveFromBasket()
    {
        ShopLogic.Instance.RemoveFromBasket(foodData);
    }

    public virtual void SetData(FoodData data)
    {
        this.foodData = data;
        titleText.text = data.name;
    }
}
