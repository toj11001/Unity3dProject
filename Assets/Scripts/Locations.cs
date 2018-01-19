using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Locations : MonoBehaviour {

    public GameObject locationLabel;
    public GameObject distanceLabel;
    public GameObject longitudeLabel;
    public GameObject latitudeLabel;
    public Text debug;

    Dictionary<string, float[]> locationsDictionary = new Dictionary<string, float[]>()
    {
        { "Train Station", new float[2] { 41.27880f, 1.979011f } },
        { "Student Residence", new float[2] { 41.274496f, 1.985206f } },
        { "Library and Cafe", new float[2] { 41.275445f, 1.985333f } },
        { "UPC Telecom & Aerospace", new float[2] { 41.275471f, 1.986940f } },
        { "UPC Agriculture", new float[2] { 41.275828f, 1.986575f } },
        { "CTTC", new float[2] { 41.275060f, 1.987707f } },
        { "ESA Building", new float[2] { 41.275882f, 1.988986f } },
        { "ICFO", new float[2] { 41.275297f, 1.989281f } },
        { "UOC", new float[2] { 41.275059f, 1.988230f } },
        { "Campus Parking", new float[2] { 41.276311f, 1.988276f } },
        { "Bus Stop", new float[2] { 41.275912f, 1.990089f } },
        { "Hospital", new float[2] { 41.284591f, 1.980895f } },
    };
    string[] locations = new string[]
    {
        "Train Station", "Student Residence", "Library and Cafe", "UPC Telecom & Aerospace",
        "UPC Agriculture", "CTTC","ESA Building","ICFO","UOC","Campus Parking","Bus Stop","Hospital"
    };



    // Use this for initialization
    void Start () {
        int p = 0;
		foreach(Transform child in transform)
        {
            Debug.Log(child.name);
            //locationsDictionary[locations[p]]
            child.GetComponentInChildren<Text>().text = locations[p++];
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        float userLongitude, userLatitude;
        float targetLongitude, targetLatitude;
        userLongitude = Singleton.GetInstance().longitudeGps;
        userLatitude = Singleton.GetInstance().latitudeGps;
        targetLongitude = Singleton.GetInstance().targetLongitude;
        targetLatitude = Singleton.GetInstance().targetLatitude;
        float distance = Singleton.GetInstance().CalcDistance(userLongitude, userLatitude, targetLongitude, targetLatitude);
        distanceLabel.GetComponent<InputField>().text = distance.ToString();
    }


    public float[] GetLocationCoordinates(string location)
    {
        float[] locationCoord;
        Debug.Log("Getting location " + location);
        locationLabel.GetComponent<InputField>().text = location;
        locationCoord = locationsDictionary[location];
        Debug.Log(locationCoord[0].ToString());
        Singleton.GetInstance().targetLongitude = locationCoord[0];
        Singleton.GetInstance().targetLatitude = locationCoord[1];
        longitudeLabel.GetComponent<InputField>().text = locationCoord[0].ToString();
        latitudeLabel.GetComponent<InputField>().text = locationCoord[1].ToString();
        return locationsDictionary[location];
    }
}
