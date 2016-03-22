using UnityEngine;
using System.Collections;

public class ClickToAttack : MonoBehaviour {

    private Entity entity;
    private BoxCollider2D collider;

    void Awake()
    {
        entity = transform.parent.GetComponent<Entity>();
        collider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            var mousePos = Input.mousePosition;
            mousePos.z = 10.0f;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            //Ray ray = new Ray(pos, Vector3.down);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.down);

            if (hit.collider == collider)
            {
                Player.player.attackTarget = entity;
            }

        }
    }
    
}
