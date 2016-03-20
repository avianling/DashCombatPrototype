using UnityEngine;
using System.Collections;

public class ClickToAttack : MonoBehaviour {
    private Entity entity;

    void Awake()
    {
        entity = GetComponent<Entity>();
    }

    void OnMouseUpAsButton()
    {
        Player.player.attackTarget = entity;
    }
}
