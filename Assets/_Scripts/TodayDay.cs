using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TodayDay : MonoBehaviour
{
    private TMP_Text dayText;
    // Start is called before the first frame update
    void Start()
    {
        dayText = GetComponent<TMP_Text>();
        dayText.text = DateTime.Now.ToString("dddd");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
