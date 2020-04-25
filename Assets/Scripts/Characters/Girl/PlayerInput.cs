using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    CharacterAnimation characterAnimation;
    Rigidbody2D rigidbody2D;

    float velocityY = 0;
    bool isMovementPaused = false;

    private void Awake()
    {
        characterAnimation = GetComponent<CharacterAnimation>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isMovementPaused)
        {
            rigidbody2D.velocity = Vector2.zero;
            return;
        }

        if(Input.GetMouseButtonDown(0))
        {
            velocityY = 40f;
        }

        Vector3 pos = transform.localPosition;
        pos.x += 3f * Time.fixedDeltaTime;
        pos.y -= 5f * Time.fixedDeltaTime;

        if (velocityY > 0)
        {
            float v = velocityY * Time.deltaTime;

            float y = transform.localPosition.y + v;
            pos.y = y;
            velocityY -= 1.7f;
            if (velocityY < 0)
                velocityY = 0;
        }
        rigidbody2D.MovePosition(pos);
    }

    public void PauseMovement(bool pause)
    {
        rigidbody2D.gravityScale = pause ? 0 : 1;
        isMovementPaused = pause;
    }
}
