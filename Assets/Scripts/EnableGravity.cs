using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableGravity : MonoBehaviour
{
    Rigidbody2D Rigidbody2D;
    public float GravityScale = 0.5f;

    private void Awake()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();

        Rigidbody2D.gravityScale = 0;

    }

    public void Gravity()
    {
        Rigidbody2D.gravityScale = GravityScale;
    }
}
