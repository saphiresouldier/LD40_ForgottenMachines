using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMove : MonoBehaviour {

    public float MoveXAmount;
    public float MoveYAmount;
    public float MoveZAmount;
	
	// Update is called once per frame
	void Update () {

        transform.Translate(MoveXAmount, MoveYAmount, MoveZAmount);

	}
}
