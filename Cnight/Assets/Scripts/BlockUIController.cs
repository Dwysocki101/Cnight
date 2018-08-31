using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockUIController : MonoBehaviour {

    // Use this for initialization
    private bool active;

    private Renderer renderComponent;

	void Start () {
        renderComponent = GetComponent<Renderer>();
        SetActive(false);
	}

    public void SetActive(bool setActive)
    {
        if (setActive)
        {
            renderComponent.enabled = true;
            active = setActive;
        }
        else
        {
            renderComponent.enabled = false;
            active = setActive;
        }
    }
}
