using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour {

    public GameObject _pickUpPrefab;
    public AudioSource sfx;

    private PickUpEffect effect;

    void Awake()
    {
        effect = gameObject.GetComponent<PickUpEffect>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            //Add effect to player
            effect.Apply();

            //spawn pickup vfx
            _pickUpPrefab.Spawn(new Vector3(transform.position.x, transform.position.y + 1.0f, transform.position.z - 1.0f), Quaternion.Euler(45,0,0));

            //Play sfx
            sfx.Play();

            gameObject.Recycle();
        }
    }
}
