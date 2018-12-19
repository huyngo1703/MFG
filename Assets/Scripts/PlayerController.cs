using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    //movement variables
    public float maxSpeed;

    //jumping variables
    bool grounded = false;
    float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public float jumpHeight;
    public Transform groundCheck;

    Rigidbody2D myRB;
    Animator myAnim;
    bool facingRight;

	// Use this for initialization
	void Start () {
        myRB = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        facingRight = true;
	}

    // Update is called once per frame
    private void Update()
    {
        if (grounded && Input.GetAxis("Jump")>0)
        {
            grounded = false;
            myAnim.SetBool("isGrounded", grounded);
            myRB.AddForce(new Vector2(0,jumpHeight));
        }
    }
    private void FixedUpdate()
    {
        //check if We are grounded - if no, then we are falling
        grounded = Physics2D.OverlapCircle(groundCheck.position,groundCheckRadius,groundLayer);
        myAnim.SetBool("isGrounded", grounded);
        myAnim.SetFloat("verticalSpeed", myRB.velocity.y);

        //move robot
        float move = Input.GetAxis("Horizontal");
        myAnim.SetFloat("speed",Mathf.Abs(move));

        myRB.velocity = new Vector2(move*maxSpeed,myRB.velocity.y);
        if (move > 0 && !facingRight)
        {
            flip();
        }
        else if (move < 0 && facingRight)
        {
            flip();
        }
    }

    void flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
