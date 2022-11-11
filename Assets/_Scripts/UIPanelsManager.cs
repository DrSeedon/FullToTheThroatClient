using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
/// <summary>
/// Менеджер UI Панелей
/// </summary>
public class UIPanelsManager : MonoBehaviour
{
    public List<UIPanel> UIPanels;
    /// <summary>
    /// Привязать менеджер к каждой панели
    /// </summary>
    private void Start()
    {
        foreach (var uiPanel in UIPanels)
        {
            uiPanel.uiPanelsManager = this;
        }
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
