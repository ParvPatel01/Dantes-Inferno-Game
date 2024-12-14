using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public CharacterController2D controller;
	public Rigidbody2D rb;
	public TrailRenderer trail;
	public Animator animator;

	[Header("Dash Settings")]
    public float dashForce = 500f; // force multiplier for dashing
    public float dashDuration = 0.2f; // How long the dash lasts
    public float dashCooldown = 3f; // Time before the player can dash again
	public float dashBarValue = 100f;

	public float runSpeed = 40f;

	float horizontalMove = 0f;
	bool jump = false;
	bool crouch = false;
	private bool isDashing = false;
    private bool canDash = true;

	// Sound Effects
	[SerializeField] private AudioClip jumpSound;
	[SerializeField] private AudioClip dashSound;


	// Update is called once per frame
	void Update () {
 		if (!isDashing) // Allow normal input only when not dashing
        {
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        }

		if(Input.GetButtonDown("Jump"))
		{
			jump = true;
			animator.SetBool("IsJumping", true);
			SoundManager.instance.PlaySound(jumpSound);
		}

		if (Input.GetButtonDown("Crouch"))
		{
			crouch = true;
		} else if (Input.GetButtonUp("Crouch"))
		{
			crouch = false;
		}

		if (Input.GetButtonDown("Dash") && canDash && horizontalMove != 0)
        {
            StartCoroutine(Dash());
        }


	}

	public void OnLanding()
	{
		animator.SetBool("IsJumping", false);
	}

	void FixedUpdate ()
	{
		if (!isDashing)
        {
            // Normal movement
            controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
			jump = false;	
        }
	}

	private IEnumerator Dash()
    {
        canDash = false; // Disable dashing
        isDashing = true; // Prevent other actions
		trail.emitting = true; // Enable trail renderer
		dashBarValue = 0f; // Reset dash bar value
		SoundManager.instance.PlaySound(dashSound);

       float dashDirection = Mathf.Sign(horizontalMove); // Determine dash direction
        rb.velocity = new Vector2(0, rb.velocity.y); // Reset horizontal velocity
        rb.AddForce(new Vector2(dashDirection * dashForce, 0), ForceMode2D.Impulse); // Apply dash force

        yield return new WaitForSeconds(dashDuration); // Wait for dash duration
        isDashing = false; // Re-enable normal movement

        yield return new WaitForSeconds(dashCooldown); // Wait for cooldown
        canDash = true; // Re-enable dashing
		dashBarValue = 100f; // Reset dash bar value
		trail.emitting = false; // Disable trail renderer
    }
}