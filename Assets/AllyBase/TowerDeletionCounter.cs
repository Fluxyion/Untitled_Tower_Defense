using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDeletionCounter : MonoBehaviour
{
    public int currentTowerDeletionCount;
    private void Awake()
    {
        currentTowerDeletionCount = 0;
    }
    
    public void TowerDeletionCounterMethod()
    {
        currentTowerDeletionCount++;
    }

   
}
