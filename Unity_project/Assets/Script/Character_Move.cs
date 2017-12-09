using UnityEngine;
using System.Collections;

public class Character_Move : MonoBehaviour {

    public float movement_speed = 1f;
    public float Radius = 20.0f;

    Vector3 _movement;
    Animator _anim;
    Rigidbody _rigidbody;

	void Awake ()
    {
        _anim = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
	}
	
	void FixedUpdate ()
    {
        //MovePlayer(horizontal, vertical);
        //TurnPlayer(playerMouseVec);
        //AnimatePlayer(horizontal, vertical); //TODO
    }

    public void MovePlayer(float h, float v)
    {
        _movement.Set(h, 0f, v);
        _movement = _movement.normalized * movement_speed * Time.deltaTime;  //normalized because we want the player to move diagonally in the same speed as vert/horizontally 

        Vector3 dist = transform.position + _movement;
        if ( dist.magnitude < Radius)
        {
            _rigidbody.MovePosition(transform.position + _movement);
        }
    }

    public void TurnPlayer(Vector3 m_vec)
    {
            Quaternion currentRot = Quaternion.LookRotation(m_vec);
            _rigidbody.MoveRotation(currentRot);

    }
}
