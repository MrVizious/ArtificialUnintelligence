using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class PlatformStartStopPath : Platform {

	[SerializeField] private PathCreator pathCreator;
	[SerializeField] private float speed;
	[SerializeField] private bool moving;
	private float distanceTravelled;

	new void Start() {
		transform.position = pathCreator.path.GetPoint(0);
		distanceTravelled = 0f;
	}

	private void Move() {
		distanceTravelled += speed * Time.deltaTime;
		transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, EndOfPathInstruction.Reverse);
	}

	/**********************************************************
     *                    DEBUGGING                           *
	 **********************************************************/

	public bool testActivation;

	private void Update() {
		if (testActivation && Input.GetKeyDown(KeyCode.P)) {
			moving = !moving;
		}
		if (moving) {
			Move();
		}
	}


}
