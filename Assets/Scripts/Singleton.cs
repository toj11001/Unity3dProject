using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton{

	private static Singleton instance;

	public float latitudeGps {get; set;}
	public float longitudeGps { get; set;}
    public float compassOrientation { get; set; }
	public float tileX;
	public float tileY;
	public float localScaleX;
	public float localScaleY;
	public int zoom;


	private Singleton(){
		latitudeGps  	= 0.0f;
		longitudeGps 	= 0.0f;
		tileX 		 	= 0.0f;
		tileY		 	= 0.0f;
        compassOrientation = 0.0f;
	}

	public static Singleton GetInstance() {

		if (instance == null)
		{
			instance = new Singleton();
		}
		return instance;

	}


	public void WorldToTilePos(float lon, float lat, int z){
		tileX = (float)((lon + 180.0f) / 360.0f * (1 << z));
		tileY = (float)((1.0f - Mathf.Log(Mathf.Tan(lat * Mathf.PI / 180.0f) + 1.0f / Mathf.Cos(lat * Mathf.PI / 180.0f)) / Mathf.PI) / 2.0f * (1 << z));
	}


	public struct Point{
		public double X;
		public double Y;
	} 

	public Point TileToWorldPos(double tile_x, double tile_y, int z){
		Point p = new Point();
		double n = System.Math.PI - ((2.0 * System.Math.PI * tile_y) / System.Math.Pow(2.0, z));

		p.X =((tile_x / System.Math.Pow(2.0, z) * 360.0) - 180.0);
		p.Y = (180.0 / System.Math.PI * System.Math.Atan(System.Math.Sinh(n)));
		return p;
	}

	public double DrawCubeY(double targetLat, double minLat, double maxLat) {
		double pixelY = ((targetLat - minLat) / (maxLat - minLat)) * localScaleX;
		return pixelY;
	}

	public double DrawCubeX(double targetLong, double minLong, double maxLong) {
		double pixelX = ((targetLong - minLong) / (maxLong - minLong)) * localScaleY;
		return pixelX;
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
