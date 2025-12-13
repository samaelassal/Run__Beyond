using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharacterController))]
public class CharacterAnimations : MonoBehaviour
{
    public float runSpeed = 8f;

    CharacterController controller;
    Animator animator;
    Vector2 moveInput;
    bool isFalling;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);
        controller.Move(move * runSpeed * Time.deltaTime);

        // simple ground check
        isFalling = !controller.isGrounded;
        animator.SetBool("isFalling", isFalling);
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public void OnJump(InputValue value)
    {
        if (value.isPressed && controller.isGrounded)
        {
            animator.SetTrigger("Jump");
        }
    }
}
