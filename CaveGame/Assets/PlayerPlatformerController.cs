using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformerController : PhysicsObject {

    public float maxSpeed = 10;
    public float jumpTakeOffSpeed = 10;
    public float fallGravity = 6;

    float savedSpeed;

    private SpriteRenderer spriteRenderer;
    private Animator animator;

	// Use this for initialization
	void Awake () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    protected override void InputAction()
    {
        if (Input.GetButtonDown("Attack"))
        {
            animator.SetTrigger("attack");
        }

        if (Input.GetAxis("Vertical") > 0)
        {
            animator.SetBool("lookUp", true);
            animator.SetBool("lookDown", false);
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            animator.SetBool("lookUp", false);
            animator.SetBool("lookDown", true);
        }
        else
        {
            animator.SetBool("lookUp", false);
            animator.SetBool("lookDown", false);
        }
    }

    void ChangeMaxSpeed(float newSpeed)
    {
        savedSpeed = maxSpeed;
        maxSpeed = newSpeed;
    }

    void RevertSpeed()
    {
        maxSpeed = savedSpeed;
    }

    protected override void ComputeVelocity()
    {

        Vector2 move = Vector2.zero;

        move.x = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown ("Jump") && grounded)
        {
            velocity.y = jumpTakeOffSpeed;
        }
        else if (Input.GetButtonUp("Jump"))
        {
            if (velocity.y > 0)
                velocity.y = velocity.y * 0.5f;
        }

        if (velocity.y < 0)
            gravityModifier = fallGravity;
        else
            gravityModifier = 1;


        if (move.x > 0.01f)
        {
            animator.SetBool("lookLeft", false);
        }
        else if (move.x < -0.01f)
        {
            animator.SetBool("lookLeft", true);
        }

        animator.SetBool("grounded", grounded);
        animator.SetFloat("velocityX", velocity.x / maxSpeed); //took out Abs
        animator.SetFloat("velocityY", velocity.y / maxSpeed); //took out Abs

        targetVelocity = move * maxSpeed;
    }
}
