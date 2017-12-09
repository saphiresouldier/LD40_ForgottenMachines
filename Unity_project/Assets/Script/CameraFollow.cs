using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public Transform _target;

    public float _smoothingFactor = 1.0f;

    public Player_Input _pInput;

    public float _aimDeviation = 3.0f;

    private Vector3 offset;

    private Transform camPos;

    void Start()
    {
        camPos = gameObject.transform; //cache

        offset = gameObject.transform.position - _target.position;
    }

	void FixedUpdate()
    {
        Vector3 aim_dir = _pInput.GetMousePos() - _target.position;

        if(aim_dir.magnitude > _aimDeviation)
        {
            aim_dir = aim_dir.normalized * _aimDeviation;
        }

        Vector3 delta = offset + _target.position + aim_dir;
        camPos.position = delta;
        //camPos.position = Vector3.Lerp(camPos.position, delta, _smoothingFactor * Time.deltaTime);
    }
}
