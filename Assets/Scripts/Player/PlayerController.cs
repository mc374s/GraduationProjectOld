using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Character2D))]
public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private Character2D character2D;

    private Vector2 moveVector;
    public Vector2 MoveVector { get { return moveVector; } set { moveVector = value; } }

    [SerializeField]
    private float gravity = -30;
    [SerializeField]
    private float horzontalSpeed = 10;

    [SerializeField]
    private float jumpHeight = 20;
    [SerializeField]
    private float jumpHoldIncrement = 2;
    [SerializeField]
    private int jumpCounterMax = 2;
    [SerializeField]
    private float jumpTime = 0.35f;
    private float jumpTimer = 0;
    private bool isJumping = false;
    private int jumpCounter = 0;

    [SerializeField]
    private float dodgeRollSpeed = 10f;
    private float dodgeRollVelocityX = 0;

    public Transform leftPosition;
    public Transform rightPosition;
    [HideInInspector]
    public Transform attackEffectPosition;
    public bool IsFacingLeft { get { return character2D.spriteFaceLeft; } }

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

    }

    private void FixedUpdate()
    {
        character2D.Move(moveVector * Time.fixedDeltaTime);

        animator.SetFloat(hashHorizontalSpeed, character2D.Velocity.x);
        animator.SetFloat(hashVerticalSpeed, character2D.Velocity.y);
        animator.SetBool(hashGrounded, character2D.IsGrounded);
    }


    public void HorizatalMovment()
    {
        moveVector.x = Input.GetAxis("Horizontal") * horzontalSpeed;

    }
    public void FacingUpdate()
    {
        if ((moveVector.x < 0 && !character2D.spriteFaceLeft || moveVector.x > 0 && character2D.spriteFaceLeft))
        {
            character2D.Flip();
        }
    }


    public void VerticalMovment()
    {
        moveVector.y += gravity * Time.deltaTime;
        if (character2D.IsGrounded && moveVector.y < 0)
        {
            moveVector.y = 0;
        }
    }

    public void Jump()
    {
        if (character2D.IsGrounded)
        {
            jumpCounter = 0;
        }
        if (Input.GetButtonDown("Jump") && ++jumpCounter < jumpCounterMax)
        {
            isJumping = true;
            jumpTimer = jumpTime;
            moveVector.y = 0;
            moveVector.y = Mathf.Sqrt(-2f * gravity * jumpHeight);
        }
    }
    public void JumpUpdate()
    {
        if (Input.GetButton("Jump") && isJumping)
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
        if (Input.GetButtonUp("Jump"))
        {
            jumpTimer = 0;
            isJumping = false;
        }
    }

    public void DodgeRoll()
    {
        if (Input.GetButtonDown("Roll"))
        {
            dodgeRollVelocityX = character2D.spriteFaceLeft ? -dodgeRollSpeed : dodgeRollSpeed;
            animator.SetTrigger(hashDodgeRoll);
        }
    }
    public void DodgeRollUpdate()
    {
        moveVector.x = dodgeRollVelocityX;
    }

    public void ResetMoveVector()
    {
        moveVector = Vector2.zero;
    }

    public void Attack()
    {
        if (Input.GetButtonDown("Attack"))
        {
            ResetMoveVector();
            animator.SetTrigger(hashAttack);
        }
        if (Input.GetButtonDown("Skill"))
        {
            Debug.Log("Skill");
            animator.SetTrigger(hashUsingSkill);
            if (Input.GetAxis("Horizontal") > 0)
            {
                animator.SetInteger(hashSkillType, 2);
            }
            else if (Input.GetAxis("Vertical") > 0)
            {
                animator.SetInteger(hashSkillType, 3);
            }
            else
            {
                animator.SetInteger(hashSkillType, 1);
            }
        }
        if (Input.GetButtonDown("Action"))
        {
            Debug.Log("Action");
        }
    }

    public void OnHurt()
    {

    }

    public void OnDie()
    {

    }

    public void Respawn()
    {

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
