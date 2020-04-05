using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Platform))]
public class TimeRestrictor : MonoBehaviour, IsActivated {

	[SerializeField] private enum Mode { FixedTimePressed, MaxTimePressed, MinTimePressed };
	[SerializeField] private Mode mode;

	[SerializeField] private float elapsedTimeInSeconds;
	private Platform activationTarget;
	private bool keepToggled;
	private IEnumerator currentRoutine;

	// Start is called before the first frame update
	void Start() {
		activationTarget = GetComponent<Platform>();
		if (activationTarget == null) Debug.LogError("No platform found!");
		//executing = false;
	}

	/**********************************************************
     *                    ACTIVATION                          *
	 **********************************************************/

	public void Activate() {
		if (currentRoutine == null) {
			switch (mode) {
				case Mode.FixedTimePressed:
					currentRoutine = FixedTimeCoroutine();
					StartCoroutine(currentRoutine);
					break;
				case Mode.MinTimePressed:
					break;
				case Mode.MaxTimePressed:
					break;
				default:
					break;
			}
		}
	}
	public void Deactivate() {
		if (currentRoutine == null) {
			switch (mode) {
				case Mode.FixedTimePressed:
					//Do nothing
					break;
				case Mode.MinTimePressed:
					break;
				case Mode.MaxTimePressed:
					break;
				default:
					break;
			}
		}
	}
	public void ToggleActivated() {
		if (currentRoutine == null) {
			switch (mode) {
				case Mode.FixedTimePressed:
					currentRoutine = FixedTimeCoroutine();
					StartCoroutine(currentRoutine);
					break;
				case Mode.MinTimePressed:
					break;
				case Mode.MaxTimePressed:
					break;
				default:
					break;
			}
		}
	}
	public void setActivated(bool newValue) {
		if (newValue) Activate();
		else Deactivate();
	}

	public bool getActivated() {
		return activationTarget.getActivated();
	}

	private IEnumerator FixedTimeCoroutine() {
		activationTarget.ToggleActivated();
		if (debugDelay) Debug.Log("Toggling for " + elapsedTimeInSeconds + " seconds");
		yield return new WaitForSeconds(elapsedTimeInSeconds);
		activationTarget.ToggleActivated();
		if (debugDelay) Debug.Log("Toggling back");
		currentRoutine = null;
	}

	private IEnumerator MinTime() {
		yield return null;
	}

	private IEnumerator MaxTime() {
		yield return null;
	}



	/**********************************************************
	 *                    DEBUGGING                           *
	 **********************************************************/

	public bool debugDelay;

}
