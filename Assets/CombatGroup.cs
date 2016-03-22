using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Random = UnityEngine.Random;


/*
Attack intensity is regulated by:

    attack frequency
    maximum simultaneous attacks
        Attack opportunity slots (multiple attack frequencies)


 */


public class CombatGroup
{
    int attackingCount = 0;
    
    List<Enemy> enemies = new List<Enemy>();
    
    float timeOfLastAttack = 0;

    float baseAttackFrequency = 3;
    float attackFrequency = 3;

    public CombatGroup()
    {

    }

    public void Add(Enemy enemy)
    {
        enemies.Add(enemy);

    }

    internal void Update()
    {
        RemoveDead();
        
        if (Time.realtimeSinceStartup - timeOfLastAttack > attackFrequency)
        {
            Attack();
        }       
    }
    
    private void Attack()
    {
        // just pick a random one. If it is not available this time, it will keep
        // trying every frame until an attack is launched...
        var enemy = FindClosest();// enemies[Random.Range(0, enemies.Count)];

        if (enemy != null && enemy.attackPhase == Enemy.AttackPhase.Circle)
        {
            // switch to closing phase
            enemy.attackPhase = Enemy.AttackPhase.Close;
            // reset attackFrequency (only when we find an available attacker)
            float frequencyVariance = baseAttackFrequency * 0.5f;
            attackFrequency = Random.Range(
                baseAttackFrequency - frequencyVariance,
                baseAttackFrequency + frequencyVariance);
            timeOfLastAttack = Time.realtimeSinceStartup;
        }
    }
    
    private Enemy FindClosest()
    {
        if (enemies.Count == 0 || Player.player == null)
        {
            return null;
        }
        
        Enemy closest = enemies[0];
        float closestDistance = (closest.transform.position - Player.player.transform.position).magnitude;

        for (int i = 1; i < enemies.Count; i++)
        {
            Enemy enemy = enemies[i];
            float distance = (enemy.transform.position - Player.player.transform.position).magnitude;
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closest = enemy;
            }
        }
        return closest;
    }
    
    private void RemoveDead()
    {
        enemies.RemoveAll(enemy => enemy == null);
    }    
}
