using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Animate : MonoBehaviour {

    public Animation _shotAnimHead;
    public Animator _animatorBody;

    // Use this for initialization
    void Start () {
        _animatorBody.SetInteger("SSpeed", 0);
        _animatorBody.SetInteger("FSpeed", 0);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AnimatePlayerBody(float horizontal, float vertical, Vector3 dir)
    {
        Vector2 MovementDir = new Vector2(horizontal, vertical).normalized;
        Vector2 LookDir = new Vector2(dir.x, dir.z).normalized;

        float sideway = 0.0f;
        float forward = 0.0f;

        forward = Vector3.Dot(MovementDir, LookDir);
        Vector2 sideVec = MovementDir - forward * LookDir;
        sideway = sideVec.magnitude;

        //Debug.Log("forward: " + forward + ", sideway: " + sideway);

        SetTransitionInteger(forward, "FSpeed");
        SetTransitionInteger(sideway, "SSpeed");
    }

    public void AnimatePlayerHead(string animName)
    {
        _shotAnimHead.Play(animName);
    }

    private void SetTransitionInteger(float val, string IntegerName)
    {
        if (val > 0.5f)
        {
            //Debug.Log("_animatorBody.SetInteger(SSpeed, 1);");
            _animatorBody.SetInteger(IntegerName, 1); ;
        }
        else if (val < -0.5f)
        {
            //Debug.Log("_animatorBody.SetInteger(SSpeed, -1);");
            _animatorBody.SetInteger(IntegerName, -1);
        }
        else
        {
            //Debug.Log("_animatorBody.SetInteger(SSpeed, 0);");
            _animatorBody.SetInteger(IntegerName, 0);
        }
    }
}
