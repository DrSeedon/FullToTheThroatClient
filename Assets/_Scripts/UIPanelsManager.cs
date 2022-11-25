using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Менеджер UI Панелей
/// </summary>
public class UIPanelsManager : MonoBehaviour
{
    public List<UIPanel> UIPanels;
    public UnityEvent onStart;
    
    /// <summary>
    /// Привязать менеджер к каждой панели
    /// </summary>
    private void Start()
    {
        foreach (var uiPanel in UIPanels)
        {
            uiPanel.uiPanelsManager = this;
        }
        onStart?.Invoke();
    }
/// <summary>
/// Выключить все панели
/// </summary>
    public void HideAll()
    {
        foreach (var uiPanel in UIPanels)
        {
            uiPanel.Hide();
        }
    }
}
