using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserScript : MonoBehaviour {
	private float userlon;
	private float userlat;
	// Use this for initialization
	void Start () {
		this.gameObject.GetComponent<Renderer>().material.color = Color.green;
	}
	
	// Update is called once per frame
	void Update () {
		userlon = Singleton.GetInstance().longitudeGps;
		userlat = Singleton.GetInstance().latitudeGps;

		int x = Mathf.FloorToInt((float)Singleton.GetInstance().tileX);
		int y = Mathf.FloorToInt((float)Singleton.GetInstance().tileY); 

		double a = Singleton.GetInstance().DrawCubeX(userlon, Singleton.GetInstance().TileToWorldPos(x, y, Singleton.GetInstance().zoom).X, Singleton.GetInstance().TileToWorldPos(x+1, y, Singleton.GetInstance().zoom).X);

		double b = Singleton.GetInstance().DrawCubeY(userlat, Singleton.GetInstance().TileToWorldPos(x, y+1, Singleton.GetInstance().zoom).Y, Singleton.GetInstance().TileToWorldPos(x, y, Singleton.GetInstance().zoom).Y);
		Debug.Log("Position in tile "+ a+ " - "+b);
		this.gameObject.transform.position = new Vector3((float)a-0.5f, (float)b-0.5f, this.gameObject.transform.position.z); 
	}
}
