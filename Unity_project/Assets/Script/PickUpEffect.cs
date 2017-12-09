using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpEffect : MonoBehaviour {

    public float Amount = 0.1f;

    public bool HealthPickUp = true;
    public bool WeaponPickUpBulletAmount = false;
    public bool WeaponPickUpShotDelay = false;
    public bool WeaponPickUpSpreadAmount = false;

    private GameObject Player;
    private PrimaryWeaponManager PlayerWeaponManager;
    private HealthManager PlayerHealthManager;

    // Use this for initialization
    void Start () {
        Player = PlayerReference.Instance.gameObject;

        PlayerWeaponManager = Player.GetComponent<PrimaryWeaponManager>();
        PlayerHealthManager = Player.GetComponent<HealthManager>();

        if (PlayerHealthManager == null)
        {
            Debug.Log("Whoops");
        }
    }

    public void Apply()
    {
        if (HealthPickUp)
        {
            PlayerHealthManager.Heal(Amount);
        }
        else if (WeaponPickUpBulletAmount)
        {
            PlayerWeaponManager._bulletsAmount += (int)Amount;
        }
        else if (WeaponPickUpShotDelay)
        {
            if (PlayerWeaponManager._shotDelay - Amount > 0.0f)
            {
                PlayerWeaponManager._shotDelay -= Amount;
            }
        }
        else if (WeaponPickUpSpreadAmount)
        {
            if (PlayerWeaponManager._spreadAmount + Amount < 1.0f)
            {
                PlayerWeaponManager._spreadAmount += Amount;
            }
        }
    }
}
