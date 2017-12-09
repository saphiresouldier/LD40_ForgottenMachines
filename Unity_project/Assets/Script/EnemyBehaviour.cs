using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    SearchState,
    AttackState
}

public class EnemyBehaviour : MonoBehaviour {

    public float MinStateDuration = 1.0f;
    public float MaxStateDuration = 10.0f;

    public float SearchStateDurationModifier = 0.5f;
    public float AttackStateDurationModifier = 2.0f;

    public float AttackDistance = 2.0f;
    public float SearchDistance = 20.0f;

    private Transform PlayerTransform;
    private PrimaryWeaponManager WeaponManager;

    private EnemyState currentState;
    private float currentStateDuration;
    private float currentStateTimer;

	// Use this for initialization
	void Start () {
        PlayerTransform = PlayerReference.Instance.transform;
        WeaponManager = gameObject.GetComponent<PrimaryWeaponManager>();

        currentStateTimer = 0.0f;
        ChangeState(EnemyState.SearchState);
	}
	
	// Update is called once per frame
	void Update () {
        currentStateTimer += Time.deltaTime;

        if (currentStateTimer > currentStateDuration)
        {
            currentStateTimer = 0.0f;

            float NextState = Random.Range(0.0f, 1.0f);

            if (NextState > 0.5f)
            {
                ChangeState(EnemyState.AttackState);
            }
            else
            {
                ChangeState(EnemyState.SearchState);
            }
        }

    }

    private void ChangeState(EnemyState newState)
    {
        StopAllCoroutines();

        currentStateDuration = Random.Range(MinStateDuration, MaxStateDuration);
        Vector3 distVec = transform.position - PlayerTransform.position;
        float distanceToPlayer = distVec.magnitude;

        if (newState == EnemyState.SearchState)
        {
            if (distanceToPlayer < AttackDistance)
            {
                ChangeState(EnemyState.AttackState);
            }

            StartCoroutine(SearchStateRoutine(currentStateDuration * SearchStateDurationModifier));
        }
        else
        {
            if (distanceToPlayer > SearchDistance)
            {
                ChangeState(EnemyState.SearchState);
            }

            StartCoroutine(AttackStateRoutine(currentStateDuration * AttackStateDurationModifier));
        }
    }

    private IEnumerator SearchStateRoutine(float duration)
    {
        Debug.Log("SearchRoutine reached!");

        while (true)
        {
            transform.LookAt(PlayerTransform);
            transform.Translate(transform.forward * 0.1f);
            yield return null;
        }        
    }

    private IEnumerator AttackStateRoutine(float duration)
    {
        Debug.Log("AttackRoutine reached!");
        

        while (true)
        {
            transform.LookAt(PlayerTransform);
            WeaponManager.activate();

            yield return null;
        }
    }
}
