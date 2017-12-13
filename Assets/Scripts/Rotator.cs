using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rotator : MonoBehaviour {

    public GameObject xTest;
    public GameObject yTest;
    public GameObject zTest;

    public float x = 0;
    public float y = 0;
    public float z = 0;
    public float angle=0;

    private Quaternion initialOrientation;

    void Start()
    {
        initialOrientation = Quaternion.Euler(-90,90,90);
        Debug.Log(transform.rotation.ToString());
        Debug.Log(transform.localRotation.ToString());
        //initialOrientation = Quaternion.Euler(0, 90, 90); //pointing north
    }
    // Update is called once per frame
    void Update () {

        float currentOrientation = Singleton.GetInstance().compassOrientation;
        //float currentOrientation = 90*Time.deltaTime;
        //Quaternion compassRotationTest = Quaternion.Euler(0, currentOrientation, 0);
        //Quaternion compassRotation = Quaternion.Euler(x, y, z);
        x = int.Parse(xTest.GetComponent<InputField>().text);
        y = int.Parse(yTest.GetComponent<InputField>().text);
        z = int.Parse(zTest.GetComponent<InputField>().text);
        //Quaternion compassRotation = Quaternion.Euler(x, y, z);
        float xDeg = currentOrientation + x;
        Quaternion compassRotation = Quaternion.Euler(xDeg, y, z);
        Debug.Log(compassRotation.ToString());
        //Quaternion compassRotation = Quaternion.Euler(0.5f, 0.5f, 0);
        this.transform.rotation = Quaternion.RotateTowards(initialOrientation, compassRotation, maxDegreesDelta: 360f);
        //transform.rotation = compassRotation;

    }
}
