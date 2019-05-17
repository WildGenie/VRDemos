using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HTC.UnityPlugin.Vive;

public class Flying : MonoBehaviour {
    
    public GameObject head;
    public GameObject rightHand;
    public GameObject leftHand;
    public int speed;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (ViveInput.GetPress(HandRole.RightHand, ControllerButton.FullTrigger) || ViveInput.GetPress(HandRole.LeftHand, ControllerButton.FullTrigger)) flying();
	}

    private void flying()
    {
        Vector3 direction = (rightHand.transform.position - head.transform.position) / speed;        
        transform.position += direction;
    }
}
