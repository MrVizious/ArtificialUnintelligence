using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformHiding : Platform {



	// Start is called before the first frame update
	new void Start() {
		base.Start();
		setActivated(getActivated());
	}

	public override void setActivated(bool newValue) {
		if (setEnabledCollider(newValue)) {
			base.setActivated(newValue);
			if (debugGraphic) ChangeSpriteOpacity(newValue ? 1f : 0.2f);
		}
	}

	/**********************************************************
     *                    DEBUGGING                           *
	 **********************************************************/

	//Changes opacity of the sprites to see if they are activated or not
	public bool debugGraphic;

}