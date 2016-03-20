using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    private Entity entity;

    public Vector3 moveTarget;
    private Knockback knockback;
    
    void Awake()
    {
        entity = GetComponent<Entity>();
        moveTarget = transform.position;
    }

    void Start()
    {
        knockback = entity.knockback;
    }

    void Update()
    {
        if ( !knockback.stunned )
        {
            if ( moveTarget != transform.position )
            {
                // move towards that point.
                StepTowards(moveTarget);
            }
        }
    }

    private void StepTowards(Vector3 targetPos)
    {
        if (targetPos != transform.position)
        {
            Vector3 direction = targetPos - transform.position;
            float length = direction.magnitude;
            direction /= length;
            if (length < entity.speed * Time.deltaTime)
            {
                transform.position = targetPos;
            }
            else
            {
                transform.position += direction * entity.speed * Time.deltaTime;
            }
        }
    }

    /*void OnCollisionEnter2D( Collision2D other )
    {
        Debug.Log("Collision yo");
        Player player = other.collider.GetComponent<Player>();
        if ( player != null )
        {
            if ( !player.dashing || player.attackTarget != entity )
            {
                player.entity.Damage(entity.damage, entity);
            }
        }
    }*/
}
