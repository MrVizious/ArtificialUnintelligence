using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Platform))]
public class TimeRestrictor : MonoBehaviour, IsActivated {

	[SerializeField] private enum Mode { FixedTimePressed, MaxTimePressed, MinTimePressed };
	[SerializeField] private Mode mode;

	[SerializeField] private float elapsedTimeInSeconds;
	private IEnumerator currentRoutine;
	private Platform activationTarget;
	private bool keepActive;

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

		switch (mode) {
			case Mode.FixedTimePressed:
				if (currentRoutine == null) {
					currentRoutine = FixedTimeCoroutine();
					StartCoroutine(currentRoutine);
				}
				break;
			case Mode.MinTimePressed:
				if (currentRoutine == null) {
					currentRoutine = MinTimeCoroutine();
					StartCoroutine(currentRoutine);
				} else keepActive = true;
				break;
			case Mode.MaxTimePressed:
				if (currentRoutine != null) StopCoroutine(currentRoutine);
				currentRoutine = MaxTimeCoroutine();
				StartCoroutine(currentRoutine);
				break;
			default:
				break;
		}

	}
	public void Deactivate() {

		switch (mode) {
			case Mode.FixedTimePressed:
				if (currentRoutine == null) activationTarget.Deactivate();
				break;
			case Mode.MinTimePressed:
				if (currentRoutine == null) activationTarget.Deactivate();
				else keepActive = false;
				break;
			case Mode.MaxTimePressed:
				if (currentRoutine != null) StopCoroutine(currentRoutine);
				currentRoutine = null;
				activationTarget.Deactivate();
				break;
			default:
				break;
		}

	}
	public void ToggleActivated() {

		switch (mode) {
			case Mode.FixedTimePressed:
				if (currentRoutine == null) {
					currentRoutine = FixedTimeCoroutine();
					StartCoroutine(currentRoutine);
				}
				break;
			case Mode.MinTimePressed:
				if (currentRoutine == null) {
					currentRoutine = MinTimeCoroutine();
					StartCoroutine(currentRoutine);
				} else keepActive = !keepActive;
				break;
			case Mode.MaxTimePressed:
				if (currentRoutine == null) {
					currentRoutine = MaxTimeCoroutine();
					StartCoroutine(currentRoutine);
				} else {
					StopCoroutine(currentRoutine);
					currentRoutine = null;
					activationTarget.ToggleActivated();
				}
				break;
			default:
				break;
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
		if (debugTime) Debug.Log("Toggling for " + elapsedTimeInSeconds + " seconds");
		yield return new WaitForSeconds(elapsedTimeInSeconds);
		activationTarget.ToggleActivated();
		if (debugTime) Debug.Log("Toggling back");
		currentRoutine = null;
	}

	private IEnumerator MinTimeCoroutine() {
		keepActive = true;
		activationTarget.ToggleActivated();
		if (debugTime) Debug.Log("Toggling for " + elapsedTimeInSeconds + " seconds");
		yield return new WaitForSeconds(elapsedTimeInSeconds);
		if (!keepActive) {
			activationTarget.ToggleActivated();
			if (debugTime) Debug.Log("Toggling back");
		}
		currentRoutine = null;
	}

	private IEnumerator MaxTimeCoroutine() {
		activationTarget.ToggleActivated();
		if (debugTime) Debug.Log("Toggling for " + elapsedTimeInSeconds + " seconds");
		yield return new WaitForSeconds(elapsedTimeInSeconds);
		activationTarget.ToggleActivated();
		if (debugTime) Debug.Log("Toggling back");
		currentRoutine = null;
	}



	/**********************************************************
	 *                    DEBUGGING                           *
	 **********************************************************/

	public bool debugTime;

}
