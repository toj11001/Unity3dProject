using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GpsHandler : MonoBehaviour {

    //public GameObject textBox;

    [SerializeField]
    private Text label;



    // Use this for initialization
    IEnumerator Start () {

        Input.compass.enabled = true;
        // First, check if user has location service enabled
        if (!Input.location.isEnabledByUser)
			yield break;

		// Start service before querying location
		Input.location.Start(20f,20f);

		// Wait until service initializes
		int maxWait = 20;
		while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
		{
			yield return new WaitForSeconds(1);
			maxWait--;
		}

		// Service didn't initialize in 20 seconds
		if (maxWait < 1)
		{
			label.text = "Timed out";
			yield break;
		}

		// Connection has failed
		if (Input.location.status == LocationServiceStatus.Failed)
		{
			label.text = "Unable to determine device location";
			yield break;
		}
		else
		{
			// Access granted and location value could be retrieved
			label.text = (
                "Location: " + Input.location.lastData.latitude + " " 
                + Input.location.lastData.longitude + " " 
                + Input.location.lastData.altitude + " " 
                + Input.location.lastData.horizontalAccuracy + " " 
                + Input.location.lastData.timestamp
                );
            //Singleton.GetInstance().longitudeGps = Input.location.lastData.longitude;
            //Singleton.GetInstance().latitudeGps = Input.location.lastData.latitude;

            Singleton.GetInstance().longitudeGps = Input.location.lastData.latitude;
            Singleton.GetInstance().latitudeGps = Input.location.lastData.longitude;
            Singleton.GetInstance().gpsReady = true;

        }

        // Stop service if there is no need to query location updates continuously
        //Input.location.Stop();



    }

	// Update is called once per frame
	void Update () {

        //Singleton.GetInstance().longitudeGps = Input.location.lastData.longitude;
        //Singleton.GetInstance().latitudeGps = Input.location.lastData.latitude;

        Singleton.GetInstance().longitudeGps = Input.location.lastData.latitude;
        Singleton.GetInstance().latitudeGps = Input.location.lastData.longitude;

        //label.text = Input.location.lastData.timestamp.ToString();
    }


    public float CalcDistance(float lon1, float lat1, float lon2, float lat2){
		int R = 6371; // radius of earth in km
		float dLat = (lat2-lat1)*(Mathf.PI / 180);
		float dLon = (lon2-lon1)*(Mathf.PI / 180);
		lat1 = lat1*(Mathf.PI / 180);
		lat2 = lat2*(Mathf.PI / 180);
		float a = Mathf.Sin(dLat/2) * Mathf.Sin(dLat/2) + Mathf.Sin(dLon/2) * Mathf.Sin(dLon/2) * Mathf.Cos(lat1) * Mathf.Cos(lat2); 
		float c = 2 * Mathf.Atan2(Mathf.Sqrt(a), Mathf.Sqrt(1-a)); 
		float d = R * c;
		return d; //distance in kms
	}



}
