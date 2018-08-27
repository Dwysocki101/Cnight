using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecroShieldIcon : MonoBehaviour {

    // Use this for initialization
    private bool active;
    private float countdown;
	void Start () {
        SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (active)
        {
            countdown -= 80.0f * Time.deltaTime;//About 3 seconds
            if (countdown < 0.0f)
            {
                SetActive(false);
            }
        }
    }

    public void SetActive(bool setActive)
    {
        if (setActive)
        {
            countdown = 120.0f;
            GetComponent<Renderer>().enabled = true;
            active = setActive;
        }
        else
        {
            countdown = 0.0f;
            GetComponent<Renderer>().enabled = false;
            active = setActive;
        }
    }
}
