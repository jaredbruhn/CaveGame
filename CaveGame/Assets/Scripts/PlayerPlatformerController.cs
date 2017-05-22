using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformerController : PhysicsObject {

    public float maxSpeed = 10;
    public float jumpTakeOffSpeed = 10;
    public float fallGravity = 6;

    float initialScale;
    float savedSpeed;
    float initialGravityModifier;

    public bool headClearForward, playerClearForward, canMoveBackward, playerPositionError;

    private SpriteRenderer spriteRenderer;
    Component DustAnim;
    private Animator animator;

    public float radius = 0.5f;
    private bool contact;
    Vector2 collisionPoint;
    // Use this for initialization
    void Awake () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        DustAnim = GetComponentInChildren<DustParticleAnimation>();
        initialGravityModifier = gravityModifier;
        initialScale = transform.localScale.x;  //x and y should be the same
        headClearForward = true; playerClearForward = true; canMoveBackward = true;
        RecalculateMovementValues();
    }

    protected override void CorrectCollider()
    {
        contact = false;
        Vector2 XYPos = new Vector2(transform.position.x, transform.position.y);
        foreach (Collider2D col in Physics2D.OverlapCircleAll(XYPos, radius, 1 << LayerMask.NameToLayer("Default"), transform.position.z, transform.position.z))
        {
            //Vector3 contactPoint = col.bounds.ClosestPoint(transform.position);
            //Vector3 v = transform.position - new Vector3(collisionPoint.x, collisionPoint.y, transform.position.z);
            //transform.position += Vector3.ClampMagnitude(v, Mathf.Clamp(radius - v.magnitude, 0, radius));
            //transform.position = Vector3(collisionPoint.x, collisionPoint.y, transform.position.z);
            contact = true;
            //print("Collider: " + col + "Contact Point: " + contactPoint);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = contact ? Color.cyan : Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
        Gizmos.DrawWireSphere(collisionPoint, .1f);
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

        if (Input.GetButtonDown("Forward"))
        {
            if (headClearForward)
            {
                if (!playerClearForward) //check feet collision
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y + (0.5f * transform.localScale.y), transform.position.z + 1);
                }
                else
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);
                }
                transform.localScale -= new Vector3(1,1,1);
                RecalculateMovementValues();
            }
        }
        if (Input.GetButtonDown("Backward"))
        {
            if (canMoveBackward)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);
                transform.localScale += new Vector3(1,1,1);
                RecalculateMovementValues();
            }

        }
    }

    void RecalculateMovementValues()
    {
        radius = transform.localScale.x * 0.1f;
        gravityModifier = transform.localScale.x * 0.5f;
        initialGravityModifier = gravityModifier;
        maxSpeed = transform.localScale.x * 3;
        jumpTakeOffSpeed = transform.localScale.x * 4;
        fallGravity = transform.localScale.x * 2;
    }

    void ChangeMaxSpeed(float newSpeedScale)
    {
        savedSpeed = maxSpeed;
        maxSpeed = newSpeedScale * maxSpeed;
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
            gravityModifier = initialGravityModifier;

        if (move.x > 0.01f)
        {
            animator.SetBool("lookLeft", false);
            if (grounded && move.x > 0.99f)
            {
                print("right particle");
                DustAnim.GetComponent<DustParticleAnimation>().EnableDust(true);
            }
            else
                DustAnim.GetComponent<DustParticleAnimation>().EnableDust(false);
        }
        else if (move.x < -0.01f)
        {
            animator.SetBool("lookLeft", true);
            if (grounded && move.x < -0.99f)
            {
                print("right particle");
                DustAnim.GetComponent<DustParticleAnimation>().EnableDust(true);
            }
            else
                DustAnim.GetComponent<DustParticleAnimation>().EnableDust(false);
        }
        else 
        {
            DustAnim.GetComponent<DustParticleAnimation>().EnableDust(false);
        }

        animator.SetBool("grounded", grounded);
        animator.SetFloat("velocityX", velocity.x / maxSpeed); //took out Abs
        animator.SetFloat("velocityY", velocity.y / maxSpeed); //took out Abs

        targetVelocity = move * maxSpeed;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.transform.position.z == transform.position.z)
        {
            foreach (ContactPoint2D hit in coll.contacts)
            {
                //print(hit.point);
                collisionPoint = hit.point;
            }
        }
    }
}
