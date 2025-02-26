using DG.Tweening.Core.Easing;
//using Unity.Android.Gradle.Manifest;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public bool canAct;
    private Animator animator;
    private Rigidbody rb;
    //public Vector2 lastMoveDir;
    public float speed;
    private Vector2 input;

    [Header("Jump")]
    public float jumpForce;
    public Transform transObj;
    public Transform transBody;
    public Transform transShadow;

    public float verticalSpeed;
    public float gravity;
    public bool isGrounded, canShit;
    public GameObject shitPrefab;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        transObj = transform;
    }
    void Update()
    {
        if (!canAct)
        {
            input = Vector2.zero;
            rb.linearVelocity = Vector3.zero;
            animator.SetFloat("MoveMagnitude", 0f);
            return;
        }
       HandleInput();
       HandleAnim();
       CheckGroundHit();
    }
    private void FixedUpdate()
    {
        if (!canAct) return;

        rb.linearVelocity = new Vector3(input.x * speed * Time.fixedDeltaTime, 0f, input.y * speed * Time.fixedDeltaTime);
        if (!isGrounded)
        {
            UpdateVerticlePosition();
        }
    }

    private void HandleInput()
    {
      /*  float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        if ((moveX == 0 && moveY == 0) && (input.x != 0 || input.y != 0))
        {
            lastMoveDir = input;
        }*/
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
        input.Normalize();

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }
    private void HandleAnim()
    {
        /*animator.SetFloat("MoveX", input.x);
        animator.SetFloat("MoveY", input.y);
        animator.SetFloat("LastMoveX", lastMoveDir.x);
        animator.SetFloat("LastMoveY", lastMoveDir.y);*/
        if (isGrounded)
        {
            animator.SetFloat("MoveMagnitude", input.magnitude);
        }
        else
        {
            animator.SetFloat("MoveMagnitude", 0f);
            // jump animation trigger here
        }
    }
    public void Jump()
    {
        if (isGrounded)
        {
            verticalSpeed = jumpForce; 
            isGrounded = false;
        }
    }
    void UpdateVerticlePosition()
    {
        verticalSpeed += gravity * Time.fixedDeltaTime;

        Vector3 deltaPosition = Vector3.up * verticalSpeed * Time.fixedDeltaTime;
        transBody.position += deltaPosition;
        transShadow.position = transObj.position - Vector3.up / 2f;
    }
    void CheckGroundHit()
    {
        if (transBody.position.y < transObj.position.y && !isGrounded)
        {
            transBody.position = transObj.position;
            transShadow.position = transObj.position - Vector3.up / 2f;
            verticalSpeed = 0f; 
            isGrounded = true;
            //canShit = true; /// if the kid ate enough food
        }
        if (canShit && isGrounded)
        {
            GameObject shit = Instantiate(shitPrefab, transform.position, transform.rotation);
            shit.transform.SetParent(FindFirstObjectByType<Ground>().transform);
            canShit = false;
        }
    }
}
