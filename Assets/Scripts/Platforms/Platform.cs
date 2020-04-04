using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Platform : MonoBehaviour {

	protected Collider2D col;
	protected SpriteRenderer sprite;
	[SerializeField] protected bool inContactWithPlayer;
	protected void Start() {
		col = GetComponent<Collider2D>();
		sprite = GetComponent<SpriteRenderer>();
		inContactWithPlayer = false;
	}

	protected void ChangeSpriteOpacity(float newValue) {
		Color tmp = sprite.color;
		tmp.a = newValue;
		sprite.color = tmp;
	}

	protected bool DisableCollider() {
		col.isTrigger = true;
		return true;
	}
	protected bool EnableCollider() {
		if (!inContactWithPlayer) {
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

	private void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag.Equals("Player")) inContactWithPlayer = true;
		if (debugCollision) Debug.Log("TriggerEnter", this);
	}
	private void OnTriggerStay2D(Collider2D other) {
		if (other.gameObject.tag.Equals("Player")) inContactWithPlayer = true;
		if (debugCollision) Debug.Log("TriggerStay", this);
	}
	private void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.tag.Equals("Player")) inContactWithPlayer = false;
		if (debugCollision) Debug.Log("TriggerExit", this);
	}

	/**********************************************************
     *                    DEBUGGING                           *
	 **********************************************************/

	public bool debugCollision;

}
