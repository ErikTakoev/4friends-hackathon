using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    public Sprite[] IdleSprites;
    int indexSprite;

    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        spriteRenderer.sprite = IdleSprites[indexSprite];


        indexSprite++;
        indexSprite = indexSprite % IdleSprites.Length;

    }
}
