using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerActionComponent : MonoBehaviour
{
    [SerializeField] private GameObject animationLine;
    [SerializeField] private AnimationCurve animationCurve;
    [SerializeField] private AnimationCurve speedAnimationCurve;
    [SerializeField] private float speed;

    private Transform animtransform => animationLine.transform;
    
    private float minimumAngle = 120;
    private float maximumAngle = -120;

    private float animationTimer;
    private float speedTimer;

    private float angel = 120;

    public bool IsStoped = true;
    
    public void OnUpdate()
    {
        if (IsStoped)
        {
            return;
        }

        Ticker();
        Mover();
        AngelEvaluate();
    }

    private void Mover()
    {
        animtransform.localRotation = Quaternion.Euler(0, 0, angel);
    }

    private void Ticker()
    {
        animationTimer += Time.deltaTime;
        speedTimer += Time.deltaTime;
    }

    private void AngelEvaluate()
    {
        angel = animationCurve.Evaluate(animationTimer * speed);
    }

    public void StartAction()
    {
        IsStoped = false;
    }

    public void StopAction()
    {
        IsStoped = true;
    }

    public float GetValue()
    {
        return angel;
    }
}
