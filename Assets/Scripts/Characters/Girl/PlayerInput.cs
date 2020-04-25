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
            velocityY = 20f;
        }

        Vector3 pos = transform.localPosition;
        pos.x += 0.1f;
        pos.y -= 0.1f;

        if (velocityY > 0)
        {
            float v = velocityY * Time.fixedDeltaTime;

            float y = transform.localPosition.y + v;
            pos.y = y;
            velocityY -= 0.7f;
            if (velocityY < 0)
                velocityY = 0;
        }
        rigidbody2D.MovePosition(pos);
    }
}
