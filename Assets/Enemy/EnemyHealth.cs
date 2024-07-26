using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField]int maxHitPoints = 5;
     private int _currentHitPoints;
     [SerializeField] private int difficultyRamp=1;
     [SerializeField] private Vector3 startPosition;
     Enemy enemy;
     EnemyMover respawnEnemy;
    
    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Enemy>();
        respawnEnemy = GetComponent<EnemyMover>();

    }

    void OnEnable()
    {
        _currentHitPoints = maxHitPoints;
    }

    void OnParticleCollision(GameObject other)
    {
        TargetLocator tower = other.GetComponentInParent<TargetLocator>();
        if (tower != null)
        {
            ProcessHit(tower.getDamage());
        }
        
    }
    public void ProcessAOEHit(int damage)
    {
        _currentHitPoints -= damage;
        if (_currentHitPoints <= 0)
        {
            HandleDeath();
        }
    }

    void ProcessHit(int damage)
    {
        _currentHitPoints -= damage;
        if (_currentHitPoints <= 0)
        {
            HandleDeath();


        }
    }

    void HandleDeath()
    {
        gameObject.SetActive(false);
            
        enemy.RewardGold();
        respawnEnemy.EnemyRespawn();
        maxHitPoints += difficultyRamp;
    }
    
}
