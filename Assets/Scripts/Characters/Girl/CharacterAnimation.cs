using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    public ParticleSystem HitStars;
    // 0 - Run, 1 - Jump, 2 - Idle, 3 - Attack
    public Animator Animator;

    private void Awake()
    {
        Run();
    }

    public void Run()
    {
        Animator.SetInteger("State", 0);
    }

    public void Attack()
    {
        Animator.SetInteger("State", 3);
    }

    public void Idle()
    {
        Animator.SetInteger("State", 2);
    }

    public void Jump()
    {
        Animator.SetInteger("State", 1);
    }

    public void Jump_Loop()
    {
        Animator.SetInteger("State", 4);
    }

    public void SetHitAnimation()
    {
        HitStars.Play();
    }
}
