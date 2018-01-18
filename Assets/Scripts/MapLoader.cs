using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapLoader : MonoBehaviour {

	public GameObject plane; 
	public GameObject textBoxLat;
    public GameObject textBoxLon;


    private float lat1 = 41.286690f;
	private float lon1 = 1.978701f;
	private int zoom = 16;

	// Use this for initialization
    IEnumerator Start () {
		//lon1 = Singleton.GetInstance().longitudeGps;
		//lat1 = Singleton.GetInstance().latitudeGps;
		textBoxLat.GetComponent<InputField>().text = lat1.ToString() + "N";
        textBoxLon.GetComponent<InputField>().text = lon1.ToString() + "E";
        int maxWait = 5;
		while (lon1==0.0f && maxWait > 0)
		{
			yield return new WaitForSeconds(5);
			maxWait--;
            lat1 = Singleton.GetInstance().latitudeGps;
			lon1 = Singleton.GetInstance().longitudeGps;
            textBoxLat.GetComponent<InputField>().text = lat1.ToString() + "N";
            textBoxLon.GetComponent<InputField>().text = lon1.ToString() + "E";
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
