using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    CharacterAnimation characterAnimation;
    Rigidbody2D rigidbody2D;

    float velocityY = 0;
    bool isMovementPaused = false;
    List<GameObject> currentCollisions = new List<GameObject>();
    bool doubleJump = true;

    private event Action<Vector3> OnMove;

    private void Awake()
    {
        characterAnimation = GetComponent<CharacterAnimation>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(currentCollisions.Count != 0)
            {
                doubleJump = true;
                velocityY = 7.5f;
            }
            else if(doubleJump)
            {
                doubleJump = false;
                velocityY = 5f;
            }
        }
    }

    void FixedUpdate()
    {
        if (isMovementPaused)
        {
            rigidbody2D.velocity = Vector2.zero;
            return;
        }

        

        Vector3 pos = transform.localPosition;
        if(currentCollisions.Count == 0)
        {
            pos.x += 3.5f * Time.fixedDeltaTime;
        }
        else
        {
            pos.x += 3.0f * Time.fixedDeltaTime;
        }
        if(currentCollisions.Count == 0)
            pos.y -= 5f * Time.fixedDeltaTime;

        if (velocityY > 0)
        {
            float v = velocityY * Time.fixedDeltaTime;

            float y = transform.localPosition.y + v;
            pos.y = y;
            velocityY -= 17f * Time.fixedDeltaTime;
            if (velocityY < 0)
                velocityY = 0;
        }
        rigidbody2D.MovePosition(pos);
        OnMove(pos);
    }

    
    void OnCollisionEnter2D(Collision2D col)
    {
        // Add the GameObject collided with to the list.
        currentCollisions.Add(col.gameObject);
    }

    void OnCollisionExit2D(Collision2D col)
    {
        // Remove the GameObject collided with from the list.
        currentCollisions.Remove(col.gameObject);
    }

    public void PauseMovement(bool pause)
    {
        rigidbody2D.gravityScale = pause ? 0 : 1;
        isMovementPaused = pause;
    }

    public void SubscribeForMove(Action<Vector3> subscriber)
    {
        OnMove += subscriber;
    }
}
