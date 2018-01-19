using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LocButtonClick : MonoBehaviour
{
    
    void Start()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        Debug.Log("Clicked " + transform.name);
        string location = transform.GetComponentInChildren<Text>().text;
        transform.parent.GetComponent<Locations>().GetLocationCoordinates(location);
    }
}
