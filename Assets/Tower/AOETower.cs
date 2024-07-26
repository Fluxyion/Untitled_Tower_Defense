using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOETower : MonoBehaviour
{
    public Vector3 boxSize = new Vector3(20f, 5f, 20f);  // The size of the box within which the tower can damage enemies
    public float damageRate = 1f;  // Time between each damage application
    public int damage = 20;  // Amount of damage dealt to each enemy
    private Enemy enemy;
    private float damageCountdown = 0f;

    private void Start()
    {
    }

    void Update()
    {
        if (damageCountdown <= 0f)
        {
            DealDamage();
            damageCountdown = 1f / damageRate;
        }

        damageCountdown -= Time.deltaTime;
    }

    void DealDamage()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position, boxSize / 2, Quaternion.identity);
        foreach (Collider collider in colliders)
        {
            EnemyHealth enemyHealth = collider.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.ProcessAOEHit(damage);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, boxSize);
    }

    public int getDamage()
    {
        return damage;
    }
}