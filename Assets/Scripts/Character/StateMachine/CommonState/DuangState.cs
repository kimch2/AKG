﻿using UnityEngine;

public class DuangState : MonoBehaviour
{
    public float DuangSpeed;

    private CharacterHealth health;
    private Animator animator;
    private Rigidbody2D rigid2D;

    private CharacterCommon characterCommon;

    private bool onTheGround;

    void OnEnable()
    {
        onTheGround = false;
    }

    void FixedUpdate()
    {
        rigid2D.velocity = (onTheGround) ? Vector2.zero : new Vector2(characterCommon.FacingRight ? -DuangSpeed : DuangSpeed, 0);
    }

    /// <summary>
    /// Event call back when on the ground.
    /// </summary>
    public void OnTheGround()
    {
		onTheGround = true;

        if (health.Dead)
        {
            animator.SetTrigger("Die");
        }
    }

    void Awake()
    {
        health = GetComponent<CharacterHealth>();
        animator = GetComponent<Animator>();
        rigid2D = GetComponent<Rigidbody2D>();
		characterCommon = GetComponent<CharacterCommon>();
    }
}
