using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public float Speed = 1f;
    bool pause = false;
    void FixedUpdate()
    {
        if (pause) return;

        transform.localPosition = new Vector3(transform.localPosition.x + Speed * Time.fixedDeltaTime, transform.localPosition.y, transform.localPosition.z);
    }

    public void Pause(bool v)
    {
        pause = v;
    }
}
