using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveMovement : MonoBehaviour {

    public float magnitude;
    public float frequency;

    private float curTime;

    void Start()
    {
        curTime = 0.0f;
    }
	
	// Update is called once per frame
	void Update()
    {
        curTime += Time.deltaTime;
        float floaty = transform.position.y + Mathf.Sin(curTime * frequency) * magnitude;
        transform.position = new Vector3(transform.position.x, floaty, transform.position.z);
	}
}
