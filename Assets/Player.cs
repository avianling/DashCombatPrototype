using UnityEngine;
using System.Collections;
using System;

public class Player : MonoBehaviour {

    public Entity entity;

    public static Player player;
    public FightCircle fightCircle;

    public Vector3 moveTarget;
    public Entity attackTarget;
    public bool dashing;

    private float invulnerable;

    public float attackRange;
    internal int chainCount;
    private float timeOfLastHit;

    float chainExpireTime = 2f;

    public void MoveTo(Vector3 temp)
    {
        attackTarget = null;
        dashing = false;
        StopAllCoroutines();
        moveTarget = temp;
    }

    void Awake()
    {
        player = this;
        fightCircle = gameObject.AddComponent<FightCircle>();
        fightCircle.focus = entity;
        moveTarget = transform.position;
        entity.onDamage += OnDamage;
    }

    void Update()
    {
        if (invulnerable > 0 )
        {
            invulnerable -= Time.deltaTime;
        }

        if (dashing)
        {
            return;
        }
        
        // check to see if chain has expired...
        // (allow slight extension if already dashing)
        if ((Time.realtimeSinceStartup - timeOfLastHit) > chainExpireTime)
        {
            chainCount = 0;
        }
        
        if (!entity.knockback.stunned)
        {
            // move towards our target!
            if ( attackTarget != null )
            {
                if (InRange())
                {
                    StartCoroutine(TryAttackTarget());
                } else
                {
                    StepTowards(attackTarget.transform.position);
                }
            }
            else
            {
                if ( transform.position != moveTarget )
                {
                    StepTowards(moveTarget);
                }
            }
        }
    }

    private void StepTowards(Vector3 targetPos)
    {
        if (targetPos != transform.position )
        {
            Vector3 direction = targetPos - transform.position;
            float length = direction.magnitude;
            direction /= length;
            if ( length < entity.speed * Time.deltaTime )
            {
                transform.position = targetPos;
            } else
            {
                transform.position += direction * entity.speed * Time.deltaTime;
            }
        }
    }

    private bool InRange()
    {
        Vector3 direction = attackTarget.transform.position - transform.position;
        float length = direction.magnitude;
        return length < attackRange;
    }

    IEnumerator WalkTo( Vector3 target, float speed )
    {
        while ( transform.position != target )
        {
            Vector3 direction = target - transform.position;
            float length = direction.magnitude;
            direction /= length;
            if (length < speed * Time.deltaTime)
            {
                transform.position = target;
            }
            else
            {
                transform.position += direction * speed * Time.deltaTime;
            }
            yield return 0;
        }

        transform.position = target;
        yield break;
    }

    IEnumerator TryAttackTarget()
    {
        // if we are in range, dash towards them at speed?
        Vector3 direction = attackTarget.transform.position - transform.position;
        float length = direction.magnitude;
        direction /= length;
        
        if ( length > attackRange )
        {
            // out of attack range.
            yield break;
        } else
        {
            dashing = true;

            // super quick dash to just in front of the target
            Vector3 desiredPosition = attackTarget.transform.position - direction * (entity.radius + attackTarget.radius);

            yield return StartCoroutine(WalkTo(desiredPosition, GetDashSpeed()));

            attackTarget.Damage(entity.damage, entity);
            chainCount++;
            timeOfLastHit = Time.realtimeSinceStartup;

            dashing = false;
            invulnerable = 0.2f;
            // stop moving
            attackTarget = null;
            moveTarget = transform.position;
        }
    }
    
    float GetDashSpeed()
    {
        if (chainCount >= 50) return 35;
        else if (chainCount >= 20) return 25;
        else if (chainCount >= 10) return 20;
        return 15f;
    }

    float GetDamage()
    {
        if (chainCount >= 50) return 4;
        else if (chainCount >= 20) return 3;
        else if (chainCount >= 10) return 2;
        return 1;
    }

    void OnDamage()
    {
        chainCount = 0;
    }

    void OnDestroy()
    {
        Application.LoadLevel(0);
    }
    
}
