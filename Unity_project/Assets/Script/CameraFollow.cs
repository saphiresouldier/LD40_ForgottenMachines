using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public Transform _target;
    public float _smoothingFactor = 1.0f;
    public Player_Input _pInput;
    public float _aimDeviation = 3.0f;
    public float _aimThreshold = 0.5f;

    private Vector3 offset;

    void Start()
    {
        //TODO: set this explicitely once start menu is refactored/redone
        offset = gameObject.transform.position - _target.position;
    }

	void FixedUpdate()
    {
        Vector3 aim_dir = _pInput.GetMousePos() - _target.position;

        //follow player pos if cursor near player character to fix jittering
        if (aim_dir.magnitude < _aimThreshold)
        {
            transform.position = _target.position;
        }
        else
        {
            //limit boundaries of camera movement relative to player-cursor distance
            if (aim_dir.magnitude > _aimDeviation)
            {
                aim_dir = aim_dir.normalized * _aimDeviation;
            }

            Vector3 delta = offset + _target.position + aim_dir;
            transform.position = delta;
        }
    }
}
