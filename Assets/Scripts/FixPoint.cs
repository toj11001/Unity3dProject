using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FixPoint : MonoBehaviour {
	public float fixPointlon;
	public float fixPointlat;
	public string description;

	public GameObject textBox;
	// Use this for initialization
	IEnumerator Start () {
		this.gameObject.GetComponent<Renderer>().material.color = Color.green;

		while (Singleton.GetInstance ().tileX == 0.0f) 
		{
			yield return new WaitForSeconds(5);
		}

		int x = Mathf.FloorToInt((float)Singleton.GetInstance().tileX);
		int y = Mathf.FloorToInt((float)Singleton.GetInstance().tileY); 

		double a = Singleton.GetInstance().DrawCubeX(fixPointlon, Singleton.GetInstance().TileToWorldPos(x, y, Singleton.GetInstance().zoom).X, 
			Singleton.GetInstance().TileToWorldPos(x+1, y, Singleton.GetInstance().zoom).X);

		double b = Singleton.GetInstance().DrawCubeY(fixPointlat, Singleton.GetInstance().TileToWorldPos(x, y+1, Singleton.GetInstance().zoom).Y, 
			Singleton.GetInstance().TileToWorldPos(x, y, Singleton.GetInstance().zoom).Y);
		Debug.Log("Position in tile "+ a+ " - "+b);
		this.gameObject.transform.position = new Vector3((float)a-0.5f, (float)b-0.5f, this.gameObject.transform.position.z); 
	}

	public void OnMouseDown(){
//		textBox.gameObject.transform.position = new 
		Debug.Log("Click");	
		textBox.GetComponent<InputField>().text = description.ToString();
	}
}
