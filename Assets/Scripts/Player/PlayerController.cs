﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterController2D
{
    [SerializeField]
    private float jumpHoldIncrement = 2;
    [SerializeField]
    private float jumpTime = 0.35f;
    private float jumpTimer = 0;
    private bool isJumping = false;

    [SerializeField]
    private float dodgeRollSpeed = 10f;
    private float dodgeRollVelocityX = 0;

    protected readonly int hashHorizontalSpeed = Animator.StringToHash("horizontalSpeed");
    protected readonly int hashVerticalSpeed = Animator.StringToHash("verticalSpeed");
    protected readonly int hashGrounded = Animator.StringToHash("grounded");
    protected readonly int hashAttack = Animator.StringToHash("attack");
    protected readonly int hashUsingSkill = Animator.StringToHash("usingSkill");
    protected readonly int hashSkillType = Animator.StringToHash("skillType");
    protected readonly int hashDodgeRoll = Animator.StringToHash("roll");

    private void Awake()
    {
        animator = GetComponent<Animator>();
        character2D = GetComponent<Character2D>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        input.Gain();
    }

    private void FixedUpdate()
    {
        character2D.Move(moveVector * Time.fixedDeltaTime);

        animator.SetFloat(hashHorizontalSpeed, character2D.Velocity.x);
        animator.SetFloat(hashVerticalSpeed, character2D.Velocity.y);
        animator.SetBool(hashGrounded, character2D.IsGrounded);
    }


    public override void HorizatalMovment()
    {
        moveVector.x = input.Horizontal * horzontalSpeed;

    }
    public override void FacingUpdate()
    {
        if ((moveVector.x < 0 && !character2D.spriteFaceLeft || moveVector.x > 0 && character2D.spriteFaceLeft))
        {
            character2D.Flip();
        }
    }

    public override void VerticalMovment()
    {
        moveVector.y += gravity * Time.deltaTime;
        if (character2D.IsGrounded && moveVector.y < 0)
        {
            moveVector.y = 0;
        }
    }

    public override void Jump()
    {
        if (character2D.IsGrounded)
        {
            jumpCounter = 0;
        }
        if (input.Jump.Down && ++jumpCounter < jumpCounterMax)
        {
            isJumping = true;
            jumpTimer = jumpTime;
            moveVector.y = 0;
            moveVector.y = Mathf.Sqrt(-2f * gravity * jumpHeight);
        }
    }
    public override void JumpUpdate()
    {
        if (input.Jump.Held && isJumping)
        {
            if (jumpTimer > 0)
            {
                moveVector.y += jumpHoldIncrement;
                jumpTimer -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }
        if (input.Jump.Up)
        {
            jumpTimer = 0;
            isJumping = false;
        }
    }

    public override void DodgeRoll()
    {
        if (input.Roll.Down)
        {
            dodgeRollVelocityX = character2D.spriteFaceLeft ? -dodgeRollSpeed : dodgeRollSpeed;
            animator.SetTrigger(hashDodgeRoll);
        }
    }
    public override void DodgeRollUpdate()
    {
        moveVector.x = dodgeRollVelocityX;
    }
    public override void InvulnerableOn()
    {
        GetComponent<Collider2D>().enabled = false;
    }
    public override void InvulnerableOff()
    {
        GetComponent<Collider2D>().enabled = true;
    }

    public override void ResetMoveVector()
    {
        moveVector = Vector2.zero;
    }

    public override void Attack()
    {
        if (input.Attack.Down)
        {
            ResetMoveVector();
            animator.SetTrigger(hashAttack);
        }
        if (input.Skill.Down)
        {
            animator.SetTrigger(hashUsingSkill);
            if (input.Horizontal > 0)
            {
                animator.SetInteger(hashSkillType, 2);
            }
            else if (input.Vertical > 0)
            {
                animator.SetInteger(hashSkillType, 3);
            }
            else
            {
                animator.SetInteger(hashSkillType, 1);
            }
        }
        if (input.Action.Down)
        {
            Debug.Log("Action");
        }
    }

    public void OnHurt(Damager damager, Damageable damageable)
    {
        //Debug.Log(gameObject.name + " hurt by " + damager.name);
    }

    public override void OnDie()
    {

    }

    public override void Respawn()
    {

    }

    public override void OnAttackHit()
    {
        Debug.Log("AttackHit");
    }

#if UNITY_EDITOR

    float height;
    void OnEnable()
    {
        height = Global.debugUIStartY;
        Global.debugUIStartY += 20;
    }
    void OnGUI()
    {
        if (Global.isDebugMenuOpen)
        {
            GUILayout.BeginArea(new Rect(Global.debugUIStartX, height, 200, 100));
            GUILayout.Label("MoveVector: " + moveVector.ToString());

            GUILayout.EndArea();

        }
    }
    private void OnDrawGizmosSelected()
    {
        //Handles.color = new Color(0, 1.0f, 0, 0.2f);
        //Handles.DrawSolidArc(transform.position, -Vector3.forward, (endpoint - transform.position).normalized, viewFov, viewDistance);

    }

#endif
}
