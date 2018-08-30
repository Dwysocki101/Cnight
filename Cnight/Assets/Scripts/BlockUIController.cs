using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockUIController : MonoBehaviour {

    // Use this for initialization
    private bool active;
    private float countdown;

    private Renderer renderComponent;

	void Start () {
        renderComponent = GetComponent<Renderer>();
        SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (active)
        {
            countdown -= Time.deltaTime;//About 2 seconds

            if (countdown <= 0.0f)
            {
                SetActive(false);
            }
        }
    }

    public void SetActive(bool setActive)
    {
        if (setActive)
        {
            countdown = 2.0f;
            renderComponent.enabled = true;
            active = setActive;
        }
        else
        {
            countdown = 0.0f;
            renderComponent.enabled = false;
            active = setActive;
        }
    }
}
