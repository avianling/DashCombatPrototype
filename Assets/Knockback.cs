using UnityEngine;
using System.Collections;

public class Knockback : MonoBehaviour {

    public float knockbackStrength;
    public Vector3 knockbackDirection;
    private Bleeder bleeder; // tool for making things bleed while they're being knocked back.

    public bool stunned
    {
        get
        {
            return knockbackStrength > 0;
        }
    }

    public void KnockBack( float force, Entity source )
    {
        knockbackDirection = (transform.position - source.transform.position);
        knockbackDirection.z = 0;
        knockbackDirection.Normalize();
        knockbackStrength = force;
        if ( bleeder != null )
        {
            bleeder.bleed = true;
        }
    }

    void Awake()
    {
        bleeder = GetComponentInChildren<Bleeder>();
    }

    void Update()
    {
        transform.position += knockbackStrength * knockbackDirection * Time.deltaTime;
        knockbackStrength = knockbackStrength * (1f - 8f * Time.deltaTime);
        if ( knockbackStrength < 0.1f && knockbackStrength != 0 )
        {
            if ( bleeder != null )
            {
                bleeder.bleed = false;
            }
            Player player = GetComponent<Player>();
            if ( player != null )
            {
                player.MoveTo(player.transform.position);
            }
            knockbackStrength = 0f;
        }
    }

    public void Die()
    {
        if (bleeder != null)
        {
            bleeder.transform.parent = null;
        }
    }
}
