using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public MiniGame.PowerActionController PowerActionController;

    public CharacterAnimation characterAnimation;
    Rigidbody2D rigidbody2D;

    float velocityY = 0;
    float velocityX = 4;
    bool isMovementPaused = false;
    List<GameObject> currentCollisions = new List<GameObject>();
    bool doubleJump = true;

    private event Action<Vector3> OnMove;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        PowerActionController.actionProvider.OnWin += ActionProvider_OnWin;
        PowerActionController.actionProvider.OnLose += SetHitStars;
    }


    private void ActionProvider_OnWin(bool power)
    {
        if (power)
            velocityX = 5;

        characterAnimation.Run();
    }

    public void SetHitStars()
    {
        velocityX = 1;
        characterAnimation.SetHitAnimation();
        characterAnimation.Run();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(currentCollisions.Count != 0)
            {
                doubleJump = true;
                velocityY = 7.5f;

                characterAnimation.Jump();
            }
            else if(doubleJump)
            {
                doubleJump = false;
                velocityY = 5f;
                characterAnimation.Jump();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "MeteorArt")
        {
            return;
        }

        Debug.Log("END GAME");

        if(collision.gameObject.name == "WinGame")
        {
            GameObject.FindObjectOfType<BlackScreen>().WinGame();
        }
        else
            GameObject.FindObjectOfType<BlackScreen>().EndGame();
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
            pos.x += (velocityX + 0.5f) * Time.fixedDeltaTime;
        }
        else
        {
            pos.x += velocityX  * Time.fixedDeltaTime;
        }
        if(currentCollisions.Count == 0)
            pos.y -= 5f * Time.fixedDeltaTime;
        else
        {
            //characterAnimation.Run();
        }

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

        velocityX = Mathf.MoveTowards(velocityX, 4f, Time.fixedDeltaTime);
    }

    
    void OnCollisionEnter2D(Collision2D col)
    {
        // Add the GameObject collided with to the list.
        currentCollisions.Add(col.gameObject);
        EnableGravity eg;
        col.gameObject.TryGetComponent<EnableGravity>(out eg);
        if (eg != null)
            eg.Gravity();
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
