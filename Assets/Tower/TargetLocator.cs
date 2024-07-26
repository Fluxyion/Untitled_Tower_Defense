using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] Transform weapon;
                     Transform target;
    [SerializeField] float maxrange;
    [SerializeField] private int damage=1;

    [SerializeField] ParticleSystem projectileParticles;
    // Start is called before the first frame update
    void AimWeapon()
    {
        if (target == null) 
        {
            Attack(false);
            return;
        }
        float targetDistance =Vector3.Distance(transform.position,target.position) ;
        weapon.LookAt(target);
        if (targetDistance < maxrange)
        {
            Attack(true);
        }
        else
        {
            Attack(false);
        }
    }

    void FindClosestTarget()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Transform closestTarget = null;
        float maxDistance = Mathf.Infinity;
        foreach (Enemy enemy in enemies)
        {
            if (!enemy.gameObject.activeInHierarchy) 
            {
                continue;
            }
            float targetDistance = Vector3.Distance(transform.position, enemy.transform.position);
            if (targetDistance < maxDistance)
            {
                closestTarget = enemy.transform;
                maxDistance = targetDistance;
            }
        }

        target = closestTarget;
    }
    // Update is called once per frame
    void Update()
    {
        FindClosestTarget();
        AimWeapon();
    }

    void Attack(bool isActive)
    {
        var emissionModule = projectileParticles.emission;
        emissionModule.enabled = isActive;


    }

    public int getDamage()
    {
        return damage;
    }
}
