using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Platform : MonoBehaviour, IsActivated {

	protected Collider2D col;
	protected SpriteRenderer sprite;
	[SerializeField] protected bool playerIsInside;


	[SerializeField] protected bool activated;
	protected void Start() {
		col = GetComponent<Collider2D>();
		sprite = GetComponent<SpriteRenderer>();
		playerIsInside = false;
	}

	protected void ChangeSpriteOpacity(float newValue) {
		Color tmp = sprite.color;
		tmp.a = newValue;
		sprite.color = tmp;
	}

	/**********************************************************
     *                    COLLIDER                            *
	 **********************************************************/

	protected bool DisableCollider() {
		col.isTrigger = true;
		return true;
	}
	protected bool EnableCollider() {
		if (!playerIsInside) {
			col.isTrigger = false;
			return true;
		}
		return false;
	}
	protected bool ToggleCollider() {
		return setEnabledCollider(!col.isTrigger);
	}
	protected bool setEnabledCollider(bool newValue) {
		if (newValue) return EnableCollider();
		else return DisableCollider();
	}


	/**********************************************************
     *                 TRIGGER COLLISIONS                     *
	 **********************************************************/

	private void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag.Equals("Player")) playerIsInside = true;
		if (logCollision) Debug.Log("TriggerEnter", this);
	}
	private void OnTriggerStay2D(Collider2D other) {
		if (other.gameObject.tag.Equals("Player")) playerIsInside = true;
		if (logCollision) Debug.Log("TriggerStay", this);
	}
	private void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.tag.Equals("Player")) playerIsInside = false;
		if (logCollision) Debug.Log("TriggerExit", this);
	}

	/**********************************************************
     *                    ACTIVATION                          *
	 **********************************************************/

	public virtual void Activate() {
		setActivated(true);
	}
	public virtual void Deactivate() {
		setActivated(false);
	}
	public virtual void ToggleActivated() {
		setActivated(!activated);
	}

	public virtual void setActivated(bool newValue) {
		activated = newValue;
		if (debugActivation) Debug.Log("Platform has been activated: " + newValue);
	}

	public bool getActivated() {
		return activated;
	}

	/**********************************************************
     *                    DEBUGGING                           *
	 **********************************************************/

	public bool logCollision, debugActivation;

}
