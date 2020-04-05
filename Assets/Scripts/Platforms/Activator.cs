using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Platform))]
public class Activator : MonoBehaviour {

	[SerializeField] private enum Mode { StartStop, KeepPressed, Burst };
	[SerializeField] private KeyCode key;

	[SerializeField] private Mode mode;

	private IsActivated activationTarget;



	// Start is called before the first frame update
	void Start() {
		activationTarget = GetComponent<Delayer>();
		if (activationTarget == null) activationTarget = GetComponent<Platform>();
	}

	// Update is called once per frame
	void Update() {
		switch (mode) {
			case Mode.StartStop:
				if (Input.GetKeyDown(key)) {
					if (debugActivation) Debug.Log("Toggling activation of Start/Stop platform", this);
					activationTarget.ToggleActivated();
				}
				break;
			case Mode.KeepPressed:
				if (Input.GetKeyDown(key)) {
					if (debugActivation) Debug.Log("Toggling activation of KeepPressed platform", this);
					activationTarget.ToggleActivated();
				} else if (Input.GetKeyUp(key)) {
					if (debugActivation) Debug.Log("Toggling activation of KeepPressed platform", this);
					activationTarget.ToggleActivated();
				}
				break;
			case Mode.Burst:
				if (Input.GetKeyDown(key)) {
					if (debugActivation) Debug.Log("Activating bursting platform", this);
					activationTarget.Activate();
				}
				break;
			default:
				Debug.LogError("Something happened and no mode was chosen for the activator!", this);
				break;
		}
	}

	/**********************************************************
     *                    DEBUGGING                           *
	 **********************************************************/

	public bool debugActivation;
}
