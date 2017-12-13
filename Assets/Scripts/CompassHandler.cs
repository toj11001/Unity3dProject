using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompassHandler : MonoBehaviour {

    public GameObject textBox;
    //public Camera;

	// Use this for initialization
	void Start () {

        // Start the compass.

        Input.compass.enabled = true;
        textBox.GetComponent<Text>().text = "-";

    }
	
	// Update is called once per frame
	void Update () {
        float orientation = Input.compass.trueHeading;
        Singleton.GetInstance().compassOrientation = orientation;
        //Quaternion compassRotation  = Quaternion.Euler(0, -orientation, 0);
        textBox.GetComponent<Text>().text = "Orientation: " + ((int)orientation).ToString() + "º";

    }
}
