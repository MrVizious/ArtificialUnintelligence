using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;


[RequireComponent(typeof(StickmanMovement))]

public class StickmanControllerAutomatic : MonoBehaviour {



	private StickmanMovement movement;
	private List<Action> actions;
	private bool started;
	private float executionTime;


	/**********************************************************
	 *                    AUTOMATION                          *
	 **********************************************************/

	public string fileName;

	void Start() {
		movement = GetComponent<StickmanMovement>();
		actions = new List<Action>();
		started = false;
		ReadActionsLog();
	}

	public void StartMoving() {
		if (debugMovementStarted) Debug.Log("Automatic movement started!");
		if (actions.Count <= 0) ReadActionsLog();
		started = true;
		executionTime = 0f;
	}

	private void ReadActionsLog() {
		string path = Application.dataPath + "/Routines/" + fileName + ".txt";
		if (debugPath) Debug.Log("Path chosen is: " + path);
		using (StreamReader sr = File.OpenText(path)) {
			if (debugFileOpened) Debug.Log("First line read is: " + sr.ReadLine());
			string s;
			while ((s = sr.ReadLine()) != null) {
				string[] splitted = s.Split(' ');
				if (splitted.Length == 3) {
					actions.Add(new Action(splitted[0], float.Parse(splitted[1]), float.Parse(splitted[2])));
				} else {
					actions.Add(new Action(splitted[0], float.Parse(splitted[1])));
				}
			}
		}
	}

	void FixedUpdate() {
		if (started) {
			if (debugExecutionTime) Debug.Log("Current execution time since the movement started: " + executionTime);
			if (actions.Count > 0) {
				if (executionTime >= actions[0].executionTime) {
					switch (actions[0].name) {
						case "Jump":
							movement.Jump();
							break;
						case "Run":
							movement.setDirection((int) actions[0].numericData);
							break;
						default: break;
					}
					if (debugActionsPerformed) Debug.Log(actions[0].name + " action performed at time " + executionTime);
					actions.RemoveAt(0);
				}
				executionTime += Time.fixedDeltaTime;
			} else started = false;
		} else if (Input.GetButtonDown("Fire1")) StartMoving();
	}


	/**********************************************************
	 *                    DEBUGGING                           *
	 **********************************************************/
	public bool debugPath;
	public bool debugFileOpened;
	public bool debugMovementStarted;
	public bool debugExecutionTime;
	public bool debugActionsPerformed;
}
