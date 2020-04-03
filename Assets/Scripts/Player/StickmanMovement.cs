using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]

public class StickmanMovement : MonoBehaviour {


	private Rigidbody2D rb;
	private int direction;

	[SerializeField] private float accelerationForce, max_x_Speed, max_y_Speed, jumpForce;
	[SerializeField] private bool grounded;

	void Start() {
		rb = GetComponent<Rigidbody2D>();
	}

	private void FixedUpdate() {
		Run();
	}


	/**********************************************************
	 *                    COLLISIONS                          *
	 **********************************************************/
	void OnCollisionStay2D(Collision2D collisionInfo) {
		//Ground check
		if (collisionInfo.gameObject.tag.Equals("Ground") && collisionInfo.otherCollider.gameObject.tag.Equals("Player")) grounded = true;
		if (debugCollisions) Debug.Log("Collider de objeto: " + collisionInfo.otherCollider.gameObject.tag + " contra collider de tag: " + collisionInfo.gameObject.tag);
	}

	void OnCollisionEnter2D(Collision2D collisionInfo) {
		//Ground check
		if (collisionInfo.gameObject.tag.Equals("Ground") && collisionInfo.otherCollider.gameObject.tag.Equals("Player")) grounded = true;
		if (debugCollisions) Debug.Log("Collider de objeto: " + collisionInfo.otherCollider.gameObject.tag + " contra collider de tag: " + collisionInfo.gameObject.tag);
	}

	void OnCollisionExit2D(Collision2D collisionInfo) {
		//Ground check
		if (collisionInfo.gameObject.tag.Equals("Ground") && collisionInfo.otherCollider.gameObject.tag.Equals("Player")) grounded = false;
	}



	/**********************************************************
	 *                      MOVEMENT                          *
	 **********************************************************/

	private void Run() {
		rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x + direction * accelerationForce * Time.deltaTime, -max_x_Speed, max_x_Speed), Mathf.Clamp(rb.velocity.y, -max_y_Speed, max_y_Speed));
	}

	public void setDirection(int newDirection) {
		direction = newDirection;
	}

	//Checks if the input for jump is pressed, and makes the character jump
	public void Jump() {
		if (grounded) {
			rb.velocity = new Vector2(rb.velocity.x, 0);
			rb.AddForce(new Vector2(0, jumpForce));
		}
	}

	public void Stop() {
		rb.velocity = Vector2.zero;
	}

	/**********************************************************
	 *                      DEBUGGING                         *
	 **********************************************************/

	public bool debugCollisions;
}
