using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;


[RequireComponent(typeof(StickmanMovement))]

public class StickmanControllerManual : MonoBehaviour {

	private StickmanMovement movement;

	void Start() {
		movement = GetComponent<StickmanMovement>();
	}

	void Update() {
		if (Input.GetButtonDown("Jump")) {
			if (debugJumpButton) Debug.Log("Jump button pressed at time " + Time.time);
			movement.Jump();
		}
		int horizontalAxis = (int) Input.GetAxisRaw("Horizontal");
		movement.setDirection(horizontalAxis);
		if (debugHorizontalAxis && horizontalAxis != lastHorizontalAxis) Debug.Log("Horizontal axis input changed! Now it is " + horizontalAxis);
		lastHorizontalAxis = horizontalAxis;
	}


	/**********************************************************
	 *                    DEBUGGING                           *
	 **********************************************************/

	public bool debugHorizontalAxis;
	public bool debugJumpButton;
	private int lastHorizontalAxis;
}
