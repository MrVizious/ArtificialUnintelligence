using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Platform))]
public class Delayer : MonoBehaviour, IsActivated {

	[SerializeField] private float delay;
	private IsActivated activationTarget;

	// Start is called before the first frame update
	void Start() {
		activationTarget = GetComponent<TimeRestrictor>();
		if (activationTarget == null) activationTarget = GetComponent<Platform>();
		if (activationTarget == null) Debug.LogError("No platform found!");
	}

	/**********************************************************
     *                    ACTIVATION                          *
	 **********************************************************/

	public void Activate() {
		StartCoroutine("ActivateCoroutine");
	}

	private IEnumerator ActivateCoroutine() {
		if (debugDelay) Debug.Log("STARTING delay of " + delay + " seconds for activation of platform", this);
		yield return new WaitForSeconds(delay);
		activationTarget.setActivated(true);
		if (debugDelay) Debug.Log("FINISHING delay of " + delay + " seconds for activation of platform", this);
	}
	public void Deactivate() {
		StartCoroutine("DeactivateCoroutine");
	}
	private IEnumerator DeactivateCoroutine() {
		if (debugDelay) Debug.Log("STARTING delay of " + delay + " seconds for deactivation of platform", this);
		yield return new WaitForSeconds(delay);
		activationTarget.setActivated(false);
		if (debugDelay) Debug.Log("FINISHING delay of " + delay + " seconds for deactivation of platform", this);
	}
	public void ToggleActivated() {
		StartCoroutine("ToggleActivatedCoroutine");
	}
	private IEnumerator ToggleActivatedCoroutine() {
		if (debugDelay) Debug.Log("STARTING delay of " + delay + " seconds for toggling activation of platform", this);
		yield return new WaitForSeconds(delay);
		if (activationTarget.getActivated()) activationTarget.Deactivate();
		else activationTarget.Activate();
		if (debugDelay) Debug.Log("FINISHING delay of " + delay + " seconds for toggling activation of platform", this);
	}
	public void setActivated(bool newValue) {
		if (newValue) Activate();
		else Deactivate();
	}

	public bool getActivated() {
		return activationTarget.getActivated();
	}

	/**********************************************************
     *                    DEBUGGING                           *
	 **********************************************************/

	public bool debugDelay;

}
