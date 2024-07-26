using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;

    [SerializeField] [Range(0.1f,30f)] float spawnDelay = 0.5f;

    [SerializeField][Range(0,50)]int poolSize=5;

    private GameObject[] pool;
    // Start is called before the first frame update
    void Awake()
    {
        PopulatePool();

    }

    void PopulatePool()
    {
        pool = new GameObject[poolSize];
        for (int i = 0; i < pool.Length; i++)
        {
            pool[i]=Instantiate(enemyPrefab,transform);
            pool[i].SetActive(false);
            
        }
    }
    void Start()
    {
        StartCoroutine(EnemySpawner());
    }

    void EnableObjectInPool()
    {
        for (int i = 0; i < pool.Length; i++)
        {
            if (pool[i].activeInHierarchy==false)
            {
                pool[i].SetActive(true);
                return;
            }
            
        }
    }

     IEnumerator EnemySpawner()
    {
        while (Application.isPlaying)
        {
            EnableObjectInPool();
            yield return new WaitForSeconds(spawnDelay);
        }
        
    }

}
