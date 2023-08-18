using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterScript : MonoBehaviour
{
    [SerializeField] private float MoveSpeed = 10;
    public float JumpSpeed = 20;
    [SerializeField] private Transform HealtUI;
    [SerializeField] private HealtSystem CharacterHealt;

    public event EventHandler<InputBool> InputStartOrStop;
    public class InputBool : EventArgs
    {
        public bool InputEnabled;
    }

    [HideInInspector] public CharacterSkinSO CharacterSkin;
    private PlayerInput Inputs;
    public Rigidbody2D PlayerRigidbody;
    public BoxCollider2D PlayerBodyCollider;
    private Animator PlayerAnim;
    private Vector2 MoveInput;
    private bool IsGrounded;
    public GameManager Gamemanager;
    private CheckpointSystem Checkpointsystem;
    private bool IsJump;
    private float CoyoteTime = 0.1f;
    private float CoyoteTimeCounter;
    private bool CoyoteJump;
    private bool CoyoteJumpReady;

    void Start()
    {
        Inputs = GetComponent<PlayerInput>();
        PlayerRigidbody = GetComponent<Rigidbody2D>();
        PlayerBodyCollider = transform.GetChild(1).GetComponent<BoxCollider2D>();
        PlayerAnim = transform.GetChild(0).GetComponent<Animator>();
        //PlayerAnim.runtimeAnimatorController = CharacterSkin.PlayerAnimator;
        Gamemanager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        Checkpointsystem = GameObject.FindGameObjectWithTag("Checkpoints").GetComponent<CheckpointSystem>();
        CharacterHealt.Healt = Mathf.Clamp(CharacterHealt.Healt, 0, 3);
        CoyoteJump = true;
        CoyoteJumpReady = true;
    }

    void Update()
    {
        Run();
        Flip();
        Jump();
        CoyoteTimer();
    }

    public void SetMoveInput(float value)
    {
        MoveInput = new Vector2(value, PlayerRigidbody.velocity.y);
    }

    void OnMove(InputValue value)
    {
        MoveInput = value.Get<Vector2>();
    }
    void OnPause()
    {
        Gamemanager.Pause();
    }

    private void Run()
    {
        Vector2 PlayerVelocity = new Vector2(MoveInput.x * MoveSpeed, PlayerRigidbody.velocity.y);
        PlayerRigidbody.velocity = PlayerVelocity;
        PlayerAnim.SetFloat("Speed", PlayerRigidbody.velocity.magnitude);
    }

    private void Flip()
    {
        if (MoveInput.x > 0)
            transform.localScale = new Vector2(-1, 1);
        if (MoveInput.x < 0)
            transform.localScale = new Vector2(1, 1);
    }

    void OnJump(InputValue value)
    {
        if (!IsGrounded && CoyoteJump && CoyoteJumpReady)
        {
            PlayerRigidbody.velocity = new Vector2(PlayerRigidbody.velocity.x, 0f);
            PlayerRigidbody.velocity += new Vector2(0f, JumpSpeed);
            PlayerAnim.SetBool("Jump", true);
            CoyoteTimeCounter = 0f;
        }
        if (value.isPressed)
        {
            /*PlayerRigidbody.velocity += new Vector2(0f, JumpSpeed);
            PlayerAnim.SetBool("Jump", true);*/
            IsJump = true;
        }
        else if (!value.isPressed)
            IsJump = false;
    }

    private void Jump()
    {
        if (IsJump && IsGrounded)
        {
            PlayerRigidbody.velocity = new Vector2(PlayerRigidbody.velocity.x, 0f);
            PlayerRigidbody.velocity += new Vector2(0f, JumpSpeed);
            PlayerAnim.SetBool("Jump", true);
            CoyoteJumpReady = false;
        }
    }
    private void CoyoteTimer()
    {
        CoyoteTimeCounter -= Time.deltaTime;
        if (CoyoteTimeCounter < 0f)
        {
            CoyoteJump = false;
        }
        else if (CoyoteTimeCounter > 0f)
        {
            CoyoteJump = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.collider.CompareTag("Enemy") || collision.gameObject.CompareTag("MapOff"))
        //{
        //    StartCoroutine(Damage());
        //}
        //if (collision.collider.CompareTag("Trap"))
        //{
        //    StartCoroutine(Damage());
        //}
        //if (collision.collider.CompareTag("EnemyKill") && PlayerRigidbody.velocity.y < -0.1)
        //{
        //    collision.transform.GetComponentInParent<EnemyMovement>().Kill();
        //    PlayerRigidbody.velocity = new Vector2(PlayerRigidbody.velocity.x, 0f);
        //    PlayerRigidbody.velocity += new Vector2(0f, (JumpSpeed * 0.75f));
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.CompareTag("Checkpoint"))
        //{
        //    collision.transform.GetChild(0).GetComponent<Animator>().SetTrigger("CheckTrigger");
        //    collision.transform.parent.GetComponent<CheckpointSystem>().SetLastCheckpoint(collision.transform);
        //    Destroy(collision.transform.GetComponent<BoxCollider2D>());
        //    Destroy(collision.transform.GetChild(0).GetComponent<Animator>(), 2);
        //}

        //if (collision.CompareTag("FinishMap") && PlayerBodyCollider.IsTouchingLayers(LayerMask.GetMask("Finish")))
        //{
        //    InputStartStop();
        //    Gamemanager.Finish();
        //}
        //if (collision.CompareTag("Coin"))
        //{
        //    Gamemanager.CoinCollect();
        //    Destroy(collision.gameObject);
        //}
        //if (collision.CompareTag("EnemyKill") && PlayerRigidbody.velocity.y < -0.1)
        //{
        //    collision.transform.GetComponentInParent<EnemyMovement>().Kill();
        //    PlayerRigidbody.velocity = new Vector2(PlayerRigidbody.velocity.x, 0f);
        //    PlayerRigidbody.velocity += new Vector2(0f, (JumpSpeed * 0.75f));
        //}
    }

    public IEnumerator Damage()
    {
        PlayerBodyCollider.isTrigger = true;
        PlayerRigidbody.constraints = RigidbodyConstraints2D.FreezePositionY;
        InputStartStop();
        CharacterHealt.Damage(1);
        HealtUI.GetChild(CharacterHealt.Healt).GetComponent<Animator>().SetTrigger("Damage");
        if (CharacterHealt.Healt > 0)
        {
            PlayerAnim.SetTrigger("Dead");
            yield return new WaitForSeconds(1);
            PlayerBodyCollider.isTrigger = false;
            PlayerRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
            InputStartStop();
            //Last checkpoint
            transform.position = Checkpointsystem.GetLastCheckpoint().position;
        }
        else
        {
            PlayerAnim.SetTrigger("Dead");
            yield return new WaitForSeconds(1);
            //character Dead
            Gamemanager.Died();
        }
    }

    public void InputStartStop()
    {
        if (Inputs.isActiveAndEnabled)
        {
            Inputs.enabled = false;
            InputStartOrStop?.Invoke(this, new InputBool { InputEnabled = false });
            IsJump = false;
        }
        else if (!Inputs.isActiveAndEnabled)
        {
            Inputs.enabled = true;
            InputStartOrStop?.Invoke(this, new InputBool { InputEnabled = true });
            IsJump = false;
        }
    }

    public void BounceJump()
    {
        PlayerRigidbody.velocity = new Vector2(PlayerRigidbody.velocity.x, 0f);
        PlayerRigidbody.velocity += new Vector2(0f, (JumpSpeed * 2f));
    }

    public void TriggerEnterGround()
    {
        PlayerAnim.SetBool("Jump", false);
        IsGrounded = true;
        CoyoteJumpReady = true;
    }

    public void TriggerExitGround()
    {
        PlayerAnim.SetBool("Jump", true);
        IsGrounded = false;
        CoyoteTimeCounter = CoyoteTime;
    }
}