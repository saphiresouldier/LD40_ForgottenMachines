using UnityEngine;
using System.Collections;

public class PrimaryBulletMovement : MonoBehaviour {

    public float shootDistance;
    public float shootSpeed;

    void OnEnable()
    {
        StartCoroutine(Shoot());
    }

    void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator Shoot()
    {
        float travelledDistance = 0;
        while (travelledDistance < shootDistance)
        {
            travelledDistance += shootSpeed * Time.deltaTime;
            transform.position += transform.forward * (shootSpeed * Time.deltaTime);
            yield return 0;
        }

        //Spawn explosion here ?

        //Recycle this pooled bullet instance
        gameObject.Recycle();
    }
}
