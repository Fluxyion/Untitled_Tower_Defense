using System.Collections.Generic;
using UnityEngine;

public class TowerObjectPool : MonoBehaviour
{
    [SerializeField] private Tower towerPrefab;
    [SerializeField] public int poolSize = 10;
    private List<Tower> pool = new List<Tower>();
    public int currentTowerCount;

    private void Awake()
    {
        for (int i = 0; i < poolSize; i++)
        {
           Tower towerInstance = Instantiate(towerPrefab);
           towerInstance.gameObject.SetActive(false);
            pool.Add(towerInstance);
        }
        
        currentTowerCount = 0;
    }

    public Tower GetFromPool()
    {
        if (pool.Count > 0)
        {
            Tower towerInstance = pool[pool.Count -1];
            pool.RemoveAt(pool.Count -1);
            towerInstance.gameObject.SetActive(true);
            currentTowerCount++;
            return towerInstance;
            
        }
        else
        {
            Tower towerInstance = Instantiate(towerPrefab);
            towerInstance.transform.SetParent(transform);
            currentTowerCount++;
            return towerInstance;
            
        }
    }

    public void ReturnToPool(Tower towerInstance)
    {
        towerInstance.gameObject.SetActive(false);
        pool.Add(towerInstance);
        currentTowerCount--;
    }
    
}