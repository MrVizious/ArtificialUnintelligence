using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Platform))]
public class Delayer : MonoBehaviour, IsActivated {

	[SerializeField] private float activateDelayInSeconds, deactivateDelayInSeconds, toggleActivatedDelayInSeconds;
	private Platform platform;

	// Start is called before the first frame update
	void Start() {
		platform = GetComponent<Platform>();
		if (platform == null) Debug.LogError("No platform found!");
	}

	/**********************************************************
     *                    ACTIVATION                          *
	 **********************************************************/

	public void Activate() {
		StartCoroutine("ActivateCoroutine");
	}

	private IEnumerator ActivateCoroutine() {
		if (debugDelay) Debug.Log("STARTING delay of " + activateDelayInSeconds + " seconds for activation of platform", this);
		yield return new WaitForSeconds(activateDelayInSeconds);
		platform.setActivated(true);
		if (debugDelay) Debug.Log("FINISHING delay of " + activateDelayInSeconds + " seconds for activation of platform", this);
	}
	public void Deactivate() {
		StartCoroutine("DeactivateCoroutine");
	}
	private IEnumerator DeactivateCoroutine() {
		if (debugDelay) Debug.Log("STARTING delay of " + deactivateDelayInSeconds + " seconds for deactivation of platform", this);
		yield return new WaitForSeconds(deactivateDelayInSeconds);
		platform.setActivated(false);
		if (debugDelay) Debug.Log("FINISHING delay of " + deactivateDelayInSeconds + " seconds for deactivation of platform", this);
	}
	public void ToggleActivated() {
		StartCoroutine("ToggleActivatedCoroutine");
	}
	private IEnumerator ToggleActivatedCoroutine() {
		if (debugDelay) Debug.Log("STARTING delay of " + toggleActivatedDelayInSeconds + " seconds for toggling activation of platform", this);
		yield return new WaitForSeconds(toggleActivatedDelayInSeconds);
		if (platform.getActivated()) platform.Deactivate();
		else platform.Activate();
		if (debugDelay) Debug.Log("FINISHING delay of " + toggleActivatedDelayInSeconds + " seconds for toggling activation of platform", this);
	}
	public void setActivated(bool newValue) {
		if (newValue) Activate();
		else Deactivate();
	}

	/**********************************************************
     *                    DEBUGGING                           *
	 **********************************************************/

	public bool debugDelay;

}
