using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformHiding : Platform {

	[SerializeField] private bool activated;

	// Start is called before the first frame update
	new void Start() {
		base.Start();
		setActivated(activated);
	}

	public void Activate() {
		if (setEnabledCollider(true)) {
			activated = true;
			if (debugGraphic) ChangeSpriteOpacity(1f);
		}
	}
	public void Deactivate() {
		if (setEnabledCollider(false)) {
			activated = false;
			if (debugGraphic) ChangeSpriteOpacity(0.2f);
		}
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

	//Changes opacity of the sprites to see if they are activated or not
	public bool debugGraphic;
	public bool testActivation;

	private void Update() {
		if (testActivation && Input.GetKeyDown(KeyCode.C)) {
			ToggleActivate();
			Debug.Log("Alpha of sprite color: " + sprite.color.a);
		}
	}

}