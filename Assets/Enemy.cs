using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public enum AttackPhase
    {
        // get to circling distance
        Approach,
        // circle around target "waiting for an opening"
        Circle,
        // close on the target to attack
        Close,
        // Begin pre-attack animation or "tell", showing intent
        // during which the animation can be interrupted
        Tell,
        // make an actual attack
        Attack
    }
    
    private Entity entity;

    public GameObject attackTarget;

    private float closeDistance = 2.5f;
    private float attackDistance = 2f;
    private float preferredCirclingDistance = 3.5f;
    private float attackFrequency = 4;

    //[NonSerialize]
    //public timeOfLastAttack;    
    public Vector3 moveTarget;
    private Knockback knockback;

    private float timeOfLastClose = 0;
    
    public AttackPhase attackPhase = AttackPhase.Approach;

    void Awake()
    {
        entity = GetComponent<Entity>();
        moveTarget = transform.position;
        attackPhase = AttackPhase.Approach;
        preferredCirclingDistance = Random.Range(
            preferredCirclingDistance, 
            preferredCirclingDistance + 1f);

        attackFrequency = Random.Range(4, 7);
    }

    void Start()
    {
        knockback = entity.knockback;
    }

    void Update()
    {
        if (Player.player == null) return;

        attackTarget = Player.player.gameObject;


        var slasher = GetComponent<Slasher>();
        
        switch (attackPhase)
        {
            case AttackPhase.Approach:
                UpdateApproach();
                break;
            case AttackPhase.Circle:
                UpdateCircle();
                break;
            case AttackPhase.Close:
                UpdateClose();
                break;
            case AttackPhase.Tell:
                // do I really need to do anything here?
                break;
            case AttackPhase.Attack:
                UpdateAttack();               
                break;
        }
        
        if ( !knockback.stunned )
        {
            if ( moveTarget != transform.position )
            {
                // move towards that point.
                StepTowards(moveTarget);
            }
        }
    }

    public void UpdateApproach()
    {
        // move toward the attack target
        moveTarget = attackTarget.transform.position;
        StepTowards(moveTarget);

        // are we close enough to enter circle state?
        float distance = GetDistanceToAttackTarget();
        if (distance < preferredCirclingDistance)
        {
            attackPhase = AttackPhase.Circle;
        }
    }

    public float GetDistanceToMoveTarget()
    {
        Vector3 direction = moveTarget - transform.position;
        return direction.magnitude;
    }

    public float GetDistanceToAttackTarget()
    {
        Vector3 direction = attackTarget.transform.position - transform.position;
        return direction.magnitude;
    }

    public void UpdateCircle()
    {
        // calculate vector to target
        Vector3 attackDirection = transform.position - attackTarget.transform.position;

        attackDirection = VectorUtil.RotateByDegrees(attackDirection, 1);
        attackDirection = VectorUtil.Set2DLength(attackDirection, preferredCirclingDistance);        
        moveTarget = attackTarget.transform.position + attackDirection;

        StepTowards(moveTarget);
        

        // NOTE: the command to stop circling and begin closing to attack
        // is now controlled by CombatGroup... 

        //if (Time.realtimeSinceStartup - timeOfLastClose > 5)
        //{
        //    StartClose();
        //}

        // Exit from this state....
        // controlled by CombatGroup...
        //      later, the  circling itself may be better handled by the combat group class
        //      because that will 
    }


    public void StartClose()
    {
        attackPhase = AttackPhase.Close;
        timeOfLastClose = Time.realtimeSinceStartup;
    }

    public void UpdateClose()
    {
        // calculate vector to target
        Vector3 attackDirection = transform.position - attackTarget.transform.position;
        // if within attack distance, switch mode
        if (attackDirection.magnitude <= closeDistance)
        {
            attackPhase = AttackPhase.Attack;

            //Slasher slasher = GetComponent<Slasher>();
            //slasher.DashForward();
        }
        // otherwise keep closing
        else
        {   
            attackDirection = VectorUtil.Set2DLength(attackDirection, attackDirection.magnitude * 0.1f);            
            // maybe keep circling while closing?
            attackDirection = VectorUtil.RotateByDegrees(attackDirection, 1);
            moveTarget = attackTarget.transform.position + attackDirection;
            StepTowards(moveTarget);
        }
    }
    
    public void UpdateTell()
    {

    }
    
    public void UpdateAttack()
    {
        Slasher slasher = GetComponent<Slasher>();
        slasher.AttackNow();

        Vector3 attackDirection = transform.position - attackTarget.transform.position;
        attackDirection = VectorUtil.Set2DLength(attackDirection, attackDistance);
        moveTarget = attackTarget.transform.position + attackDirection;
        StepTowards(moveTarget);
    }
    
    public void OnAttackComplete()
    {
        attackPhase = AttackPhase.Circle;
        attackFrequency = Random.Range(2, 5);
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
