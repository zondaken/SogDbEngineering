using System;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class UIPanelHandler : MonoBehaviour
{
    [SerializeField] private List<UIPanelBase> panels;
    private PanelState _currentPanelState;

    public enum PanelState
    {
        Login = 0,
        MainMenu = 1
    }
    
    private void Awake()
    {
        foreach (var item in panels)
        {
            item.Initialize(this);
        }

        _currentPanelState = PanelState.Login;
        ShowCurrentPanel();
    }

    public void ChangePanel(PanelState newState)
    {
        HideCurrentPanel();
        _currentPanelState = newState;
        ShowCurrentPanel();
    }

    private void HideCurrentPanel()
    {
        panels[(int) _currentPanelState].OnHide();
    }

    private void ShowCurrentPanel()
    {
        panels[(int) _currentPanelState].OnShow();
    }
}