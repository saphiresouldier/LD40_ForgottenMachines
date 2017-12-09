using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlingToCamera : MonoBehaviour {

    public Transform Cam;

	// Use this for initialization
	void Start () {
        if (!Cam)
        {
            Cam = Camera.main.transform;
        }

        transform.LookAt(Cam);
    }

    // Update is called once per frame
    void Update() {
        transform.LookAt(Cam);
    }
}
