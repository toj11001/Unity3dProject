using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiatePoint : MonoBehaviour {
	public GameObject pointPrefab;

	public void CreatePoint(){
		pointPrefab = Instantiate(pointPrefab);
		//pointPrefab.GetComponent<ObjectLocation>().lat = Singleton.GetInstance().latitudeGps;
		//pointPrefab.GetComponent<ObjectLocation>().lon = Singleton.GetInstance().longitudeGps;

	}
}
