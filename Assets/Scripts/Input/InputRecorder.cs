using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;

public class InputRecorder : MonoBehaviour {

	private string path;
	private float startTime;
	private List<Action> actions;

	//This is used to check if a new direction input should be recorded or not
	private int lastRunDirection;
	//This allows to create a named file instead of a non-specific name
	public string fileName;

	//Custom class to keep action data
	private class Action {
		public string name;
		public float executionTime;
		public float numericData;

		public Action(string newName, float newExecutionTime, float newNumericData) {
			name = newName;
			executionTime = newExecutionTime;
			numericData = newNumericData;
		}

		public Action(string newName, float newExecutionTime) {
			name = newName;
			executionTime = newExecutionTime;
		}

		public string toString() {
			string returnString = "";
			returnString += name;
			returnString += " ";
			returnString += executionTime;
			if (name.Equals("Run")) {
				returnString += " ";
				returnString += numericData;
			}
			return returnString;
		}
	}

	void Start() {
		//Empty List of actions and times
		actions = new List<Action>();
		//startTime begins with a negative value until the start button is pressed
		startTime = -1f;
		//lastAxis begins as 0f so any change on the horizontal axis is recorded
		lastRunDirection = 0;
		//Debugger of the created path
		if (debugPath) {
			//Path  of the file created using the current time as name
			path = Application.dataPath + "/InputLogs/" + DateTime.Now.ToString("dd_MM_yyyy__HH_mm_ss") + ".txt";
			Debug.Log(path);
			if (debugCreateFile) CreateDummyFile();
		}
	}

	// Update is called once per frame
	void Update() {
		//If the button (Left-Ctrl // Letf-Click) has been pressed, the startTime changes to  start or stop the recording of the actions
		if (Input.GetButtonDown("Fire1")) {
			if (startTime < 0) {
				startTime = Time.time;
				if (debugRecording) Debug.Log("Input recording BEGAN at time: " + Time.time);
			} else {
				startTime = -1f;
				if (debugRecording) Debug.Log("Input recording STOPPED at time: " + Time.time);
				SaveActionsLog();
			}
		}
		//If the recording has begun, record the inputs
		if (startTime >= 0) RecordInputs();
	}


	private void RecordInputs() {
		//Records the Jump action
		if (Input.GetButtonDown("Jump")) actions.Add(new Action("Jump", Time.time - startTime));

		//Records the Horizontal axis only if it is different from the last one
		int runDirection = (int) Input.GetAxisRaw("Horizontal");
		if (runDirection != lastRunDirection) {
			actions.Add(new Action("Run", Time.time - startTime, runDirection));
			lastRunDirection = runDirection;
		}
	}


	private void SaveActionsLog() {
		if (debugSaving) Debug.Log("Saving ACTUAL data!");
		if (fileName.Length < 1) {
			path = Application.dataPath + "/InputLogs/" + DateTime.Now.ToString("dd_MM_yyyy__HH_mm_ss") + ".txt";
		} else {
			path = Application.dataPath + "/InputLogs/" + fileName + ".txt";
		}
		if (debugPath) Debug.Log("Saving ACTUAL data at: " + path);

		if (!File.Exists(path)) {
			// Create a file to write to
			using (StreamWriter sw = File.CreateText(path)) {
				//Write in a separate line every single action as "Action\ntime", and empty the list after
				foreach (Action action in actions) {
					sw.WriteLine(action.toString());
					if (debugArgumentSplit) {
						string[] words = action.toString().Split(' ');
						for (int i = 0; i < words.Length; i++) Debug.Log(i + ": " + words[i]);
					}
				}
				actions.Clear();
			}
		}
	}





	/***************************************************************************
     *                               DEBUGGING                                 *
     **************************************************************************/

	//Variables for the editor. If checked, they will allow some debugging
	public bool debugPath;
	public bool debugCreateFile;
	public bool debugRecording;
	public bool debugSaving;
	public bool debugArgumentSplit;



	/* This function simply creates a dummy file with no real information.
     * Can be useful to check if the file itself is being generated with the given path.
     */
	private void CreateDummyFile() {
		//Checks if the name of the file already exists. If that is te case, the new file is not created
		if (!File.Exists(path)) {
			// Create a file to write to
			using (StreamWriter sw = File.CreateText(path)) {
				//Dummy text
				sw.WriteLine("Test line... Hello!");
			}
		}
	}


}