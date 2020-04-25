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
        if(currentCollisions.Count == 0)
        {
            pos.x += 3.5f * Time.fixedDeltaTime;
        }
        else
        {
            pos.x += 2.5f * Time.fixedDeltaTime;
        }
        if(currentCollisions.Count == 0)
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
}
