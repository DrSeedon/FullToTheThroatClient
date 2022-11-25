using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChangeImage : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    //смена спрайта при клике, как кнопка
    public Sprite spriteOn;
    public Sprite spriteOff;
    public Image image;
    void Start()
    {
        image.sprite = spriteOff;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        image.sprite = spriteOn;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        image.sprite = spriteOff;
    }

    void Update()
    {

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.sprite = spriteOff;
    }
}
