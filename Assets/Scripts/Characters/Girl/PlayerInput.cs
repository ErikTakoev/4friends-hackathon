using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    CharacterAnimation characterAnimation;
    Rigidbody2D rigidbody2D;

    float velocityY = 0;

    private void Awake()
    {
        characterAnimation = GetComponent<CharacterAnimation>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            velocityY = 5;
        }
        rigidbody2D.velocity = new Vector2(4f, velocityY);

        if(velocityY > -5f)
        {
            velocityY -= 0.1f;
            if (velocityY < -5f)
                velocityY = -5f;
        }

    }
}
