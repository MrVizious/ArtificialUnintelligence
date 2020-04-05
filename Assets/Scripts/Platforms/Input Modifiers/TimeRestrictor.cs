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
		activationTarget.Activate();
	}

	public void Deactivate() {
		activationTarget.Deactivate();
	}
	public void ToggleActivated() {
		StartCoroutine("ToggleActivatedCoroutine");
	}
	private IEnumerator ToggleActivatedCoroutine() {

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
		if (activationTarget.getActivated()) activationTarget.Deactivate();
		else activationTarget.Activate();
		yield return null;
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
		yield return new WaitForSeconds(elapsedTimeInSeconds);
		activationTarget.ToggleActivated();
		currentRoutine = null;
	}

	private IEnumerator MinTime() {
		yield return null;
	}



	/**********************************************************
     *                    DEBUGGING                           *
	 **********************************************************/

	public bool debugDelay;

}
