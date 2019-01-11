using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MouseManager : MonoBehaviour
{
    public LayerMask ClickableLayer;
    public Texture2D NormalCursor;
    public Texture2D ClickableObjectCursor;
    public Texture2D DoorwayCursor;
    public Texture2D CombatCursor;
    public EventVector3 OnClickEnvironment;

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
            bool lootBox = false;
            bool enemyNpc = false;
            if (raycastHit.collider.gameObject.tag == "Doorway")
            {
                Cursor.SetCursor(DoorwayCursor, new Vector2(16, 16), CursorMode.Auto);
                door = true;
            }
            else if (raycastHit.collider.gameObject.tag == "Lootbox")
            {
                Cursor.SetCursor(ClickableObjectCursor, new Vector2(16, 16), CursorMode.Auto);
                lootBox = true;
            }
            else if (raycastHit.collider.gameObject.tag == "EnemyNpc")
            {
                Cursor.SetCursor(CombatCursor, new Vector2(16, 16), CursorMode.Auto);
                enemyNpc = true;
            }
            else
            {
                Cursor.SetCursor(NormalCursor, new Vector2(16, 16), CursorMode.Auto);
            }

            if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
            {
                if (door)
                {
                    Transform doorway = raycastHit.collider.gameObject.transform;
                    OnClickEnvironment.Invoke(doorway.position);
                    Debug.Log("Clicked on door");
                }
                else if (lootBox)
                {
                    Transform lootBoxTransform = raycastHit.collider.gameObject.transform;
                    OnClickEnvironment.Invoke(lootBoxTransform.position);
                    Debug.Log("Clicked on lootbox");
                }
                else if (enemyNpc)
                {
                    Transform enemyNpcTransform = raycastHit.collider.gameObject.transform;
                    OnClickEnvironment.Invoke(enemyNpcTransform.position);
                    Debug.Log("Clicked on enemy npc");
                }
                else
                {
                    OnClickEnvironment.Invoke(raycastHit.point);
                }
            }
        }
        //Non-interactable stuff
        else
        {
            Cursor.SetCursor(NormalCursor, Vector2.zero, CursorMode.Auto);
        }
    }
}

[System.Serializable]
public class EventVector3 : UnityEvent<Vector3>
{
}