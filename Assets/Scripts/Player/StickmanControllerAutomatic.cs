using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]

public class StickmanControllerAutomatic : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    public float max_x_Speed;
    public float max_y_Speed;
    public float jumpForce;
    private List<Action> actions;

    [SerializeField]
    private bool grounded;






    void FixedUpdate(){
        //Sets the horizontal speed to that given by the axis input previously recorded and clamps both axis of velocity according to maximum speeds allowed
        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x + currentAxis * speed * Time.deltaTime, -max_x_Speed, max_x_Speed), Mathf.Clamp(rb.velocity.y, -max_y_Speed, max_y_Speed));
    }

    void OnCollisionStay2D(Collision2D collisionInfo)
    {
        //Ground check
        if(collisionInfo.gameObject.tag.Equals("Ground") && collisionInfo.otherCollider.gameObject.tag.Equals("StickMan")) grounded = true;
        //Debug.Log("Collider de objeto: " + collisionInfo.otherCollider.gameObject.tag + " contra collider de tag: " + collisionInfo.gameObject.tag);
    }

    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        //Ground check
        if(collisionInfo.gameObject.tag.Equals("Ground") && collisionInfo.otherCollider.gameObject.tag.Equals("StickMan")) grounded = true;
        //Debug.Log("Collider de objeto: " + collisionInfo.otherCollider.gameObject.tag + " contra collider de tag: " + collisionInfo.gameObject.tag);
    }

    void OnCollisionExit2D(Collision2D collisionInfo){
        //Ground check
        if(collisionInfo.gameObject.tag.Equals("Ground") && collisionInfo.otherCollider.gameObject.tag.Equals("StickMan")) grounded = false;
    }




    /**********************************************************
     *                    AUTOMATION                          *
     **********************************************************/

    public string fileName;
    private float currentAxis;

    void Start(){
        rb = GetComponent<Rigidbody2D>();
        grounded = false;
        currentAxis = 0f;
        actions = new List<Action>();
        ReadActionsLog();
    }

    private void ReadActionsLog(){
        string path = Application.dataPath + "/Routines/" + fileName + ".txt";
        if(debugPath) Debug.Log("Path chosen is: " + path);
        using (StreamReader sr = File.OpenText(path))
        {
            if(debugFileOpened) Debug.Log("First line read is: " + sr.ReadLine());
            string s;
            while ((s = sr.ReadLine()) != null)
            {
                string[] splitted = s.Split(' ');
                if(splitted.Length == 3){
                    actions.Add(new Action(splitted[0], float.Parse(splitted[1]), float.Parse(splitted[2])));
                } else {
                    actions.Add(new Action(splitted[0], float.Parse(splitted[1])));
                }
            }
        }
    }

    //Custom class to keep action data
    private class Action{
        public string name;
        public float executionTime;
        public float numericData;

        public Action(string newName, float newExecutionTime, float newNumericData){
            name = newName;
            executionTime = newExecutionTime;
            numericData = newNumericData;
        }

        public Action(string newName, float newExecutionTime){
            name = newName;
            executionTime = newExecutionTime;
        }

        public string toString(){
            string returnString = "";
            returnString += name;
            returnString += " ";
            returnString += executionTime;
            if(name.Equals("Run")){
                returnString += " ";
                returnString += numericData;
            }
            return returnString;
        }
    }

    //Checks if the input for jump is pressed, and makes the character jump
    private void Jump(){
        if(grounded){
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(new Vector2(0, jumpForce));
        }
    }

    void Update(){
        if(actions.Count > 0 && Time.time >= actions[0].executionTime){
            switch(actions[0].name){
                case "Jump": Jump();
                             break;
                case "Run":  currentAxis = actions[0].numericData;
                             break;
                default:     break;
            }
            actions.RemoveAt(0);
        }
    }




    /**********************************************************
     *                    DEBUGGING                           *
     **********************************************************/
     public bool debugPath;
     public bool debugFileOpened;
}
