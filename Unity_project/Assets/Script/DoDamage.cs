using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DoDamage : MonoBehaviour {

    public float DamageAmount = 1;
    public string TagToHit = "Enemy";

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	

	}

    void OnTriggerEnter(Collider col)
    {
        //Debug.Log("TriggerCollision");
        string colTag = col.gameObject.tag;
        if (colTag == TagToHit || colTag == "PickUp") //TODO: Why PickUp?
        {
            //play sfx

            IDamageable<float> otherHealth = col.GetComponent<IDamageable<float>>();
            otherHealth.Damage(DamageAmount);
            gameObject.Recycle();
        }   
    }
}
