using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IsActivated {

	void Activate();
	void Deactivate();
	void ToggleActivated();
	void setActivated(bool newValue);

}
