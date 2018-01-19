using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapLoader : MonoBehaviour {

	public GameObject plane; 
	public GameObject textBoxLat;
    public GameObject textBoxLon;

    private float lon1 = 41.284535f;
    private float lat1 = 1.980892f;
	private int zoom = 14;

	// Use this for initialization
    IEnumerator Start () {
        //lon1 = Singleton.GetInstance().longitudeGps;
        //lat1 = Singleton.GetInstance().latitudeGps;
        textBoxLon.GetComponent<InputField>().text = lon1.ToString() + "N";
        textBoxLat.GetComponent<InputField>().text = lat1.ToString() + "E";
        int maxWait = 1;
        while (!Singleton.GetInstance().gpsReady && maxWait > 0)
        {
            yield return new WaitForSeconds(5);
            maxWait--;
            lat1 = Singleton.GetInstance().latitudeGps;
            lon1 = Singleton.GetInstance().longitudeGps;
            textBoxLon.GetComponent<InputField>().text = lon1.ToString() + "N";
            textBoxLat.GetComponent<InputField>().text = lat1.ToString() + "E";
        }
        Singleton.GetInstance().gpsReady = false;
        //		lon1 = Singleton.GetInstance().longitudeGps;
        //		lat1 = Singleton.GetInstance().latitudeGps;

        Singleton.GetInstance().WorldToTilePos(lon1, lat1, zoom);

        Singleton.GetInstance().localScaleX = 1.0f;
        Singleton.GetInstance().localScaleY = 1.0f;
        Singleton.GetInstance().zoom = zoom;

        string url = "http://a.tile.openstreetmap.org/" + zoom + "/" + Mathf.FloorToInt(Singleton.GetInstance().tileX) + "/" + Mathf.FloorToInt(Singleton.GetInstance().tileY) + ".png";
        WWW www = new WWW(url);
        yield return www;
        if (!string.IsNullOrEmpty(www.error))
            Debug.Log(www.error);
        Texture2D texture = new Texture2D(2, 2, TextureFormat.ARGB32, true);
        www.LoadImageIntoTexture(texture);
        plane.GetComponent<Renderer>().material.mainTexture = texture;


        StartCoroutine(CreateTile(lon1, lat1, zoom));
    }

    IEnumerator CreateTile(float lon, float lat, int zoom)
    {
        while (!Singleton.GetInstance().gpsReady) yield return new WaitForSeconds(10);

        string url = "http://a.tile.openstreetmap.org/" + zoom + "/" + Mathf.FloorToInt(Singleton.GetInstance().tileX) + "/" + Mathf.FloorToInt(Singleton.GetInstance().tileY) + ".png";
        WWW www = new WWW(url);
        yield return www;
        if (!string.IsNullOrEmpty(www.error))
            Debug.Log(www.error);
        Texture2D texture = new Texture2D(2, 2, TextureFormat.ARGB32, true);
        www.LoadImageIntoTexture(texture);
        plane.GetComponent<Renderer>().material.mainTexture = texture;
        lat1 = Singleton.GetInstance().latitudeGps;
        lon1 = Singleton.GetInstance().longitudeGps;
        StartCoroutine(CreateTile(lon1, lat1, zoom));
    }



}
