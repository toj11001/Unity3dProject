using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapLoader : MonoBehaviour {

	public GameObject plane; 
	public GameObject textBox;


	private float lat1 = 41.286690f;
	private float lon1 = 1.978701f;
	private int zoom = 16;

	// Use this for initialization
    IEnumerator Start () {
		//lon1 = Singleton.GetInstance().longitudeGps;
		//lat1 = Singleton.GetInstance().latitudeGps;
		textBox.GetComponent<InputField>().text = "MP ("+ lon1.ToString() + "; "+ lat1.ToString() + ")";
		int maxWait = 5;
		while (lon1==0.0f && maxWait > 0)
		{
			yield return new WaitForSeconds(5);
			maxWait--;
			lon1 = Singleton.GetInstance().longitudeGps;
			textBox.GetComponent<InputField>().text = "MP (" + lon1.ToString() + "; " + lat1.ToString() + ")";
        }
//		lon1 = Singleton.GetInstance().longitudeGps;
//		lat1 = Singleton.GetInstance().latitudeGps;

		Singleton.GetInstance().WorldToTilePos(lon1,lat1,zoom);

		Singleton.GetInstance().localScaleX = 1.0f;
		Singleton.GetInstance().localScaleY = 1.0f;
		Singleton.GetInstance().zoom = zoom;

		string url = "http://a.tile.openstreetmap.org/"+zoom+"/"+Mathf.FloorToInt(Singleton.GetInstance().tileX)+"/"+Mathf.FloorToInt(Singleton.GetInstance().tileY)+".png";
	    WWW www = new WWW(url);
        yield return www;
		if (!string.IsNullOrEmpty (www.error))
			Debug.Log (www.error);
        Texture2D texture = new Texture2D(2,2,TextureFormat.ARGB32, true);
        www.LoadImageIntoTexture(texture);
        plane.GetComponent<Renderer>().material.mainTexture=texture; 

    }
	
 

}
