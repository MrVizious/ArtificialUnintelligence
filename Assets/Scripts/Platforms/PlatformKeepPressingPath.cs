using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class PlatformKeepPressingPath : Platform {

	[SerializeField] private PathCreator pathCreator;
	[SerializeField] private float speed;
	[SerializeField] private bool activated;
	private float distanceTravelled;

	new void Start() {
		transform.position = pathCreator.path.GetPoint(0);
		distanceTravelled = 0f;
	}

	private void Move() {
		distanceTravelled += speed * Time.deltaTime;
		transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, EndOfPathInstruction.Reverse);
	}

	public void Activate() {
		activated = true;

	}
	public void Deactivate() {
		activated = false;
	}
	public void ToggleActivate() {
		setActivated(!activated);
	}

	public void setActivated(bool newValue) {
		if (newValue) Activate();
		else Deactivate();
	}

	/**********************************************************
     *                    DEBUGGING                           *
	 **********************************************************/

	public bool testActivation;

	private void Update() {
		if (testActivation) {
			if (Input.GetKeyDown(KeyCode.O)) {
				ToggleActivate();
			}
			if (Input.GetKeyUp(KeyCode.O)) {
				ToggleActivate();
			}
		}
		if (activated) Move();
	}


}
