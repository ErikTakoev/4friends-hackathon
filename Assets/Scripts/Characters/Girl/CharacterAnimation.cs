using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    public Sprite[] IdleSprites;
    public ParticleSystem HitStars;
    int indexSprite;

    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetHitAnimation()
    {
        HitStars.Play();
    }

    private void FixedUpdate()
    {
        spriteRenderer.sprite = IdleSprites[indexSprite];


        indexSprite++;
        indexSprite = indexSprite % IdleSprites.Length;

    }
}
