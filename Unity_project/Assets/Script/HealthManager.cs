using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class HealthManager : MonoBehaviour, IDamageable<float> , IKillable {

    public float _maxHealth = 50;  //start at max health by default

    public Canvas canvas;
    public Slider _healthSlider;
    public Image _fillImage;
    public Color _fillImageColor = Color.green;
    public Image _bgImage;
    public Color _healthBackgroundColor = Color.red;
    public float verticalOffset = 1.0f;

    public GameObject _explosionPrefab;
    public GameObject _hitReward;
    public Transform RewardSpawnerTransform;
    public float UpgradeSpawnProbability = 0.0f;

    public Material _hitMaterial;
    public float _hitDuration = 0.1f;

    private Transform playerTransform;

    private Quaternion _lockRot;
    private Renderer rend;
    private Material _origMat;

    [Header("Debug Output:\n")]

    [SerializeField]
    private float _currentHealth;
    [SerializeField]
    private bool _dead;

    private Transform trans;
    private Camera mainCam;

	// Use this for initialization
	private void Start ()
    {
        trans = transform;
        mainCam = Camera.main;
        rend = gameObject.GetComponentInChildren<Renderer>() ;
        _origMat = rend.material;
        canvas = GameObject.Find("CanvasHealth").GetComponent<Canvas>();
        _bgImage = transform.GetChild(0).GetChild(0).GetComponent<Image>();
        _fillImage = transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Image>();


        _healthSlider.transform.SetParent(canvas.transform, false);
        _currentHealth = _maxHealth;
        Debug.Log("current health: " + _currentHealth);
        _dead = false;

        //setup health HUD
        _healthSlider.maxValue = _maxHealth;
        _healthSlider.minValue = 0;

        _bgImage.color = _healthBackgroundColor;
        _fillImage.color = _fillImageColor;

        _healthSlider.value = _currentHealth;

        _lockRot = _healthSlider.transform.rotation;

        //playerTransform = GameObject.Find("Player").transform;
        playerTransform = PlayerReference.Instance.transform;

    }

    public bool IsAlive()
    {
        return _currentHealth >= 0.0f;
    }
	
    public void Damage(float damage)
    {
        _currentHealth -= damage;

        StartCoroutine(Hit());
        UpdateHUD();

        if (_hitReward)
        {
            GameObject reward =_hitReward.Spawn(RewardSpawnerTransform.position);
            reward.GetComponent<Rigidbody>().AddForce(transform.position - playerTransform.position, ForceMode.Impulse);
        }

        if (_currentHealth <= 0)
        {
            _dead = true;

            kill();
        }
    }

    public void Heal(float healAmount)
    {
        _currentHealth += healAmount;

        if (_currentHealth > _maxHealth)
        {
            _currentHealth = _maxHealth;
        }

        UpdateHUD();
    }

    private void UpdateHUD()
    {
        _healthSlider.value = _currentHealth;
    }

    public void kill()
    {
        //spawn explosion
        //Instantiate(_explosionPrefab, transform.position, Quaternion.Euler(45, 0, 0));
        _explosionPrefab.Spawn(transform.position, Quaternion.Euler(45, 0, 0));

        if (Random.Range(0.0f, 1.0f) < UpgradeSpawnProbability)
        {
            //spawn pickup
            Instantiate(GameController.Instance.GetRandomPickUp(), new Vector3(transform.position.x, transform.position.y + 2.0f, transform.position.z), Quaternion.identity);
        }

        Destroy(_healthSlider.gameObject);
        Destroy(gameObject);

        if (gameObject.tag == "Player")
        {
            SceneManager.LoadScene("Start");
        }
    }

    IEnumerator Hit()
    {
        
        rend.material = _hitMaterial;
        yield return new WaitForSeconds(_hitDuration);
        rend.material = _origMat;
    }

    public void Update()
    {
        Vector3 worldpos = new Vector3(trans.position.x, trans.position.y + verticalOffset, trans.position.z);
        Vector3 screenpos = mainCam.WorldToScreenPoint(worldpos);
        _healthSlider.transform.position = screenpos;

        UpdateHUD();
    }
}
