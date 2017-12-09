using UnityEngine;
using System.Collections;

public class WorldRotator : MonoBehaviour {

    public float speed = 1.0f;

    private Quaternion rot;

    // Use this for initialization
    void Start () {
        rot = transform.rotation;
    }
	
	// Update is called once per frame
	void Update () {

        if(Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Rotation!");

            StartCoroutine(Rotate(90.0f, new Vector3(0,1,0))); //global axis eg (0,1,0) = Y
        }
	
	}

    IEnumerator Rotate(float degrees, Vector3 axis)
    {
        Quaternion finalRot = Quaternion.AngleAxis(degrees, axis) * rot;

        while(transform.rotation != finalRot)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, finalRot, speed * Time.deltaTime);
            yield return 0;
        }
    }
}
