using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabScript : MonoBehaviour {
	public float lat;
	public float lon;
	private float latitudeGps;
	private float longitudeGps;

	// Use this for initialization
	void Start () {
		longitudeGps = Singleton.GetInstance().longitudeGps;
		latitudeGps = Singleton.GetInstance().latitudeGps;
		lon= longitudeGps;
		lat= latitudeGps;

		int x = Mathf.FloorToInt((float)Singleton.GetInstance().tileX);
		int y = Mathf.FloorToInt((float)Singleton.GetInstance().tileY); 


		double a = Singleton.GetInstance().DrawCubeX(lon, Singleton.GetInstance().TileToWorldPos(x, y, Singleton.GetInstance().zoom).X, Singleton.GetInstance().TileToWorldPos(x+1, y, Singleton.GetInstance().zoom).X);

		double b = Singleton.GetInstance().DrawCubeY(lat, Singleton.GetInstance().TileToWorldPos(x, y+1, Singleton.GetInstance().zoom).Y, Singleton.GetInstance().TileToWorldPos(x, y, Singleton.GetInstance().zoom).Y);
		Debug.Log("Position in tile "+ a+ " - "+b);
		this.gameObject.transform.position = new Vector3((float)a-0.5f, (float)b-0.5f, this.gameObject.transform.position.z); 
	}

	// Update is called once per frame
	void Update () {

		longitudeGps = Singleton.GetInstance().longitudeGps;
		latitudeGps = Singleton.GetInstance().latitudeGps;

		float d =  Singleton.GetInstance().CalcDistance(longitudeGps,latitudeGps,lon,lat);

		if (d < 1.0f){
			gameObject.GetComponent<Renderer>().enabled = true;
		}
	}



	public void OnMouseDown(){
		this.gameObject.GetComponent<Renderer>().material.color = Color.blue;
	}

}
