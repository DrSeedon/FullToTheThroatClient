using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FoodElement : MonoBehaviour
{
    public TMP_Text titleText;
    public TMP_Text priceText;
    public TMP_Text weightText;
    public TMP_Text countText;
    public TMP_Text compositionText;
    public FoodData foodData;
    public Button addFood;
    public Button addFood2;
    public Button removeFood;

    public RawImage rawImage;

    public Button infoButton;
    public FoodElement infoElement;

    private void Start()
    {
        if (addFood != null) addFood.onClick.AddListener(AddToBasket);
        if (addFood2 != null) addFood2.onClick.AddListener(AddToBasket);
        if (removeFood != null) removeFood.onClick.AddListener(RemoveFromBasket);
        if (infoButton != null) infoButton.onClick.AddListener(()=>
        {
            infoElement.gameObject.SetActive(true);
            infoElement.SetData(foodData);
        });
    }

    public void AddToBasket()
    {
        UpdateVisual(ShopLogic.Instance.AddToBasket(foodData));
    }

    public void RemoveFromBasket()
    {
        UpdateVisual(ShopLogic.Instance.RemoveFromBasket(foodData));
    }

    private void UpdateVisual(int value)
    {
        if (countText != null) countText.text = value.ToString();
    }

    public virtual void SetData(FoodData data)
    {
        this.foodData = data;
        titleText.text = data.name;
        priceText.text = data.price + " руб.";
        weightText.text = data.weight;
        compositionText.text = "Состав: " + data.composition;
        rawImage.texture = ImageStorage.Instance.textures[data.idImage];
    }
}
