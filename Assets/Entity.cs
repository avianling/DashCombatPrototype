using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour {

    public float health;
    public float maxHealth;
    public float speed;
    public float damage;
    public float radius;

    public Knockback knockback;

    public GameObject deadVersion;

    void Awake()
    {
        knockback = gameObject.AddComponent<Knockback>();
    }
    
    public void Damage( float damage, Entity attacker )
    {
        health -= damage;
        Game.Slow();
        Game.ShowHit(0.5f * (transform.position + attacker.transform.position));
        knockback.KnockBack(damage * 30f, attacker);
        if ( health <= 0 )
        {
            Die();
        }
    }

    public void Die()
    {
        if (deadVersion != null)
        {
            GameObject corpse = Instantiate<GameObject>(deadVersion);
            Knockback knockback = corpse.GetComponent<Knockback>();
            corpse.transform.position = transform.position;
            if (knockback != null)
            {
                knockback.knockbackDirection = this.knockback.knockbackDirection;
                knockback.knockbackStrength = this.knockback.knockbackStrength;
            }
        }
        this.knockback.Die();
        Destroy(gameObject);
        // transfer the settings from our knockback to their knockback.
    }

}
