using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    public int currentTowerCount = 0;
    

    public void TowerCountİncrease()
    {
        currentTowerCount++;
        Debug.Log(currentTowerCount);

    }
}
