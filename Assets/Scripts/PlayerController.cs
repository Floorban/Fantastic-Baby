using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public Vector2 lastMoveDir;
    public float speed;
    public Rigidbody rb;
    private Vector2 input;

    void Update()
    {
       HandleInput();
       HandleAnim();
    }
    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector3(input.x * speed * Time.fixedDeltaTime, 0f, input.y * speed * Time.fixedDeltaTime);
    }
    private void HandleInput()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        if ((moveX == 0 && moveY == 0) && (input.x != 0 || input.y != 0))
        {
            lastMoveDir = input;
        }
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
        input.Normalize();
    }
    private void HandleAnim()
    {
        animator.SetFloat("MoveX", input.x);
        animator.SetFloat("MoveY", input.y);
        animator.SetFloat("MoveMagnitude", input.magnitude);
        animator.SetFloat("LastMoveX", lastMoveDir.x);
        animator.SetFloat("LastMoveY", lastMoveDir.y);
    }
}
