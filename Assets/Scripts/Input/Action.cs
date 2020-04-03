using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action {
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
