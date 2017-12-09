using UnityEngine;
using System.Collections;

public class Player_Input : MonoBehaviour {

    private Character_Move _cMove;
    private PrimaryWeaponManager _pWM;

    public LayerMask _clickableGoal;
    public float _clickableDistance = 30f;
    public Character_Animate _charAnimate;

    private Vector3 playerMouseVec = Vector3.zero;
    RaycastHit goalHit;
    Ray cameraRay;

    void Awake()
    {
        _cMove = GetComponent<Character_Move>();
        _pWM = GetComponent<PrimaryWeaponManager>();
    }

    public Vector3 GetMousePos()
    {
        return playerMouseVec;
    }

    void Update()
    {
        //Left Mouse Button ------------------------------
        if (Input.GetMouseButtonDown(0))
        {
            _pWM.activate();

            //Play shooting animation
            _charAnimate.AnimatePlayerHead("Shot");
        }

        if (Input.GetMouseButtonUp(0))
        {
            _pWM.deactivate();
        }

        //Right Mouse Button ------------------------------
        if (Input.GetMouseButtonDown(1))
        {
            //Secondary Weapon
        }

        if (Input.GetMouseButtonUp(1))
        {
            //Secondary Weapon
        }
    }

    void FixedUpdate()
    {
        // Moving ------------------------------------------
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        //Debug.Log("horizontal: " + horizontal + ", vertical: " + vertical);

        _cMove.MovePlayer(horizontal, vertical);

        //Rotating -----------------------------------------
        cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition); 
        if (Physics.Raycast(cameraRay, out goalHit, _clickableDistance, _clickableGoal))
        {
            playerMouseVec = goalHit.point - transform.position;
            playerMouseVec.y = 0f;

            _cMove.TurnPlayer(playerMouseVec);

            //animate movement
            _charAnimate.AnimatePlayerBody(horizontal, vertical, playerMouseVec);
        }
    }

}
