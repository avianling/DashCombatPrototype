using UnityEngine;
using System.Collections;

public class DamageVolume : MonoBehaviour {

    public Entity enemy;
    private Collider2D ourCollider;

    void Start()
    {
        ourCollider = GetComponent<Collider2D>();
    }

    void OnTriggerEnter2D(Collider2D other )
    {
        Player player = other.GetComponent<Player>();
        if ( player != null )
        {
            player.entity.Damage(enemy.damage, enemy);
        }
    }

}
