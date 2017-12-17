using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    static public GameController Instance;

    public List<GameObject> EnemyPrefabs;

    public float SpawnDelay = 10.0f;
    public float SpawnAcceleration = 0.5f;
    public float MaxSpawnRate = 0.5f;
    public float EnemyStartHealth = 15.0f;

    public List<Transform> SpawnPoints;

    public List<GameObject> PickUpPrefabs;

    private int PlayerStrength;

    void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        StartCoroutine(SpawnWaves());
    }


    void Update()
    {
        //DEBUG
        if (Input.GetKeyDown(KeyCode.E))
        {
            //Instantiate(EnemyPrefabs[0], PlayerReference.Instance.transform.position, Quaternion.identity);
        }
    }

    public GameObject GetRandomPickUp()
    {
        int rand = Random.Range(0, PickUpPrefabs.Count);

        return PickUpPrefabs[rand];
    }

    public IEnumerator SpawnWaves()
    {
        while (PlayerReference.Instance.GetComponent<HealthManager>().IsAlive())
        {
            foreach (Transform point in SpawnPoints)
            {
                //TODO: List of enemies, later in game spawn stronger ones
                var enemy = Instantiate(EnemyPrefabs[0], point.position, Quaternion.identity);
                
                //Increase shot rate, will be replaced
                enemy.GetComponent<PrimaryWeaponManager>()._shotDelay = SpawnDelay / 4.0f;

                //Increase health
                enemy.GetComponent<HealthManager>()._maxHealth = EnemyStartHealth * (20.0f/SpawnDelay);
            }

            yield return new WaitForSeconds(SpawnDelay);

            if (SpawnDelay * SpawnAcceleration > MaxSpawnRate)
            {
                SpawnDelay *= SpawnAcceleration;
            }
        }
    }
}
