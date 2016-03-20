using UnityEngine;
using System.Collections;

public class Slasher : MonoBehaviour {

    public Animator weaponAnimator;
    public GameObject weapon;
    public PlayerTriggerZone triggerZone;

    private Enemy enemy;
    private Entity entity;
    private Knockback knockback;

    void Start()
    {
        enemy = GetComponent<Enemy>();
        entity = GetComponent<Entity>();
        knockback = entity.knockback;
        Player.player.fightCircle.attackers.Add(entity);
    }

    void Update()
    {
        Vector3 newWeaponDirection = (Player.player.transform.position - transform.position).normalized;
        weapon.transform.up = Vector3.Lerp(weapon.transform.up, newWeaponDirection, 3f * Time.deltaTime);

        weaponAnimator.SetBool("InRange", triggerZone.PlayerInZone);
        weaponAnimator.SetBool("CanAttack", Player.player.fightCircle.CanAttack(entity));
        if ( knockback.stunned )
        {
            weaponAnimator.SetTrigger("Interrupted");
        }

        enemy.moveTarget = Player.player.fightCircle.GetFightPosition(entity);
    }

}
