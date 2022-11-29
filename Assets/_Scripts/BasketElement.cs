using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BasketElement : MonoBehaviour
{
    public TMP_Text titleText;
    public TMP_Text priceText;
    public TMP_Text countText;
    public TMP_Text totalText;
    public FoodData foodData;
    public Button addFood;
    public Button removeFood;

    public RawImage rawImage;

    private void Start()
    {
        if (addFood != null) addFood.onClick.AddListener(AddToBasket);
        if (removeFood != null) removeFood.onClick.AddListener(RemoveFromBasket);
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
        //countText.text = value.ToString();
    }

    public virtual void SetData(FoodData data, int count)
    {
        this.foodData = data;
        titleText.text = data.name;
        //priceText.text = data.price.ToString();
        countText.text = count.ToString();
        totalText.text = data.price * count + " руб.";
        rawImage.texture = ImageStorage.Instance.textures[data.idImage];
    }
}
