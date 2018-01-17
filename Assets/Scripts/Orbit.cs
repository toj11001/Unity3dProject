using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    private void Start()
    {
        enabled = true;
    }

    private void Update ()
    {
        transform.RotateAround(Camera.main.transform.position, Vector3.up, 10 * Time.deltaTime);
	}
}
