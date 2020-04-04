using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformKeepPressing : Platform {

	[SerializeField] private bool activated;
	private Collider2D collider;
	private SpriteRenderer sprite;

	// Start is called before the first frame update
	void Start() {
		collider = GetComponent<Collider2D>();
		sprite = GetComponent<SpriteRenderer>();
		setActivated(activated);
	}

	public void Activate() {
		activated = true;
		collider.enabled = activated;
		if (debugGraphic) {
			Color tmp = sprite.color;
			tmp.a = 1f;
			sprite.color = tmp;
		}
	}
	public void Deactivate() {
		activated = false;
		collider.enabled = activated;
		if (debugGraphic) {
			Color tmp = sprite.color;
			tmp.a = 0.2f;
			sprite.color = tmp;
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

	private void Update() {
		if (debugGraphic) {
			if (Input.GetKeyDown(KeyCode.K)) {
				ToggleActivate();
				Debug.Log("Alpha of sprite color: " + sprite.color.a);
			} else if (Input.GetKeyUp(KeyCode.K)) {
				ToggleActivate();
				Debug.Log("Alpha of sprite color: " + sprite.color.a);
			}
		}
	}

}