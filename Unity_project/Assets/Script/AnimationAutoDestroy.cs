using UnityEngine;
using System.Collections;

public class AnimationAutoDestroy : MonoBehaviour
{
    public float delay = 0f;

    // Use this for initialization
    void Start()
    {
        //Destroy(gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + delay);

        StartCoroutine(RecycleRoutine());
    }

    private IEnumerator RecycleRoutine()
    {
        if (gameObject.GetComponent<Animator>())
        {
            yield return new WaitForSeconds(this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + delay);
        }
        else
        {
            yield return new WaitForSeconds(delay);
        }

        gameObject.Recycle();
    }
}