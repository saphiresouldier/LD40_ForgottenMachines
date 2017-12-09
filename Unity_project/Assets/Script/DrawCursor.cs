using UnityEngine;
using System.Collections;

public class DrawCursor : MonoBehaviour {

    public Texture2D _cursorTex;
    public Sprite _cursorSprite;
    public Vector2 _targetPoint = Vector2.zero;
    public CursorMode _cMode = CursorMode.Auto;
    public int _cursorTexWidth = 32;
    public int _cursorTexHeight = 32;

    private Vector2 mousepos;

    void Start()
    {
        Cursor.visible = false;
        //Cursor.SetCursor(_cursorTex, _targetPoint, _cMode);
    }

    void OnGUI()
    {
        GUI.DrawTexture(new Rect(mousepos.x - (_cursorTexWidth / 2), mousepos.y - (_cursorTexHeight / 2), _cursorTexWidth, _cursorTexHeight), _cursorTex);
    }

    void Update()
    {
        mousepos = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);
    }

    //void OnMouseEnter()
    //{
    //    //Cursor.visible();
    //    Cursor.SetCursor(_cursorTex, _targetPoint, _cMode);
    //}

    //void OnMouseExit()
    //{
    //    Cursor.SetCursor(null, Vector2.zero, _cMode);
    //}

}
