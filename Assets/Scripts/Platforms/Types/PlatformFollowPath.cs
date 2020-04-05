using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class PlatformFollowPath : Platform {

	[SerializeField] private PathCreator pathCreator;
	[SerializeField] private float speed;
	[Range(0.0f, 0.99f)] [SerializeField] private float initialPosition;
	private float distanceTravelled;

	private void Update() {
		if (activated) Move();
	}
	new void Start() {
		base.Start();
		setActivated(activated);
		transform.position = pathCreator.path.GetPoint((int) Mathf.Round(pathCreator.path.NumPoints * initialPosition));
		distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
	}

	private void Move() {
		distanceTravelled += speed * Time.deltaTime;
		transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, EndOfPathInstruction.Reverse);
	}
}