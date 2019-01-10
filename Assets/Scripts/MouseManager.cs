using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    public LayerMask ClickableLayer;
    public Texture2D NormalCursor;
    public Texture2D ClickableObjectCursor;
    public Texture2D DoorwayCursor;
    public Texture2D CombatCursor;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit raycastHit;
        //When mouseover a layer that should be interactable
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out raycastHit, 50,
            ClickableLayer.value))
        {
            bool door = false;
            if (raycastHit.collider.gameObject.tag == "Doorway")
            {
                Cursor.SetCursor(DoorwayCursor, new Vector2(16, 16), CursorMode.Auto);
                door = true;
            }
            else
            {
                Cursor.SetCursor(ClickableObjectCursor, new Vector2(16, 16), CursorMode.Auto);
            }
        }
        //Non-interactable stuff
        else
        {
            Cursor.SetCursor(NormalCursor, Vector2.zero, CursorMode.Auto);
        }
    }
}