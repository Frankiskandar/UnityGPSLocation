using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowLocation : MonoBehaviour {

    private float currentLongitude;
    private float currentLatitude;
    private GameObject longitudeText;
    private GameObject latitudeText;

    IEnumerator showLocation()
    {
        // First, check if user has location service enabled
        if (!Input.location.isEnabledByUser)
            yield break;

        // Start service before querying location
        Input.location.Start();

        // Wait until service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // Service didn't initialize in 20 seconds
        if (maxWait < 1)
        {
            print("Timed out");
            yield break;
        }

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            print("Unable to determine device location");
            yield break;
        }
        else
        {
            // Access granted and location value could be retrieved
            print("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
            currentLongitude = Input.location.lastData.longitude;
            currentLatitude = Input.location.lastData.latitude;
            ShowOnScreen();
        }

        // Stop service if there is no need to query location updates continuously
        Input.location.Stop();
    }

    // Use this for initialization
    void Start()
    {
        longitudeText = GameObject.Find("Longitude");
        latitudeText = GameObject.Find("Latitude");

        StartCoroutine("showLocation");

    }

    void ShowOnScreen()
    {
        

        longitudeText.GetComponent<Text>().text = "Longitude: " + currentLongitude;
        latitudeText.GetComponent<Text>().text = "Latitude: " + currentLatitude;
    }


    // Update is called once per frame
    void Update()
    {

    }
}


