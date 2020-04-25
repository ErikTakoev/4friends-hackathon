using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerActionProvider : MonoBehaviour
{
    private PowerActionComponent powerActionComponent;


    private float perfectHitDelta = 15;
    
    private float winHit = 60;

    public event Action<bool> OnWin;
    public event Action OnLose;

    public void Init(PowerActionComponent actionComponent)
    {
        powerActionComponent = actionComponent;
    }

    public void OnUpdate()
    {
        if (Input.GetMouseButtonUp(0) && !powerActionComponent.IsStoped)
        {
            ResultAction();
        }
    }


    private void ResultAction()
    {
        powerActionComponent.StopAction();
        
        var value = powerActionComponent.GetValue();

        value = Mathf.Abs(value);
        
        if (value < winHit)
        {
            Win(value);
        }
        else
        {
            Lose();
        }
    }

    private void Win(float angle)
    {
        OnWin?.Invoke(angle <= perfectHitDelta);
    }

    private void Lose()
    {
        OnLose?.Invoke();
    }
}
