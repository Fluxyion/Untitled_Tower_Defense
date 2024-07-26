using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tile: MonoBehaviour
{
    [SerializeField] private int deleteLimitPerLevel;
    [SerializeField] private TowerDeletionCounter towerDeletionCounter;
    [SerializeField] private List<TowerObjectPool> towerPools;
    [SerializeField] private List<TowerObjectPool> aoeTowerPools;
    [SerializeField] private List<TowerObjectPool> roadblockPools;
    [SerializeField] private bool isPlaceable;
    public bool IsPlaceable { get { return isPlaceable; } }
    private GridManager gridManager;
    private Pathfinder pathfinder;
    private TowerManager towerManager;
    private Vector2Int coordinates = new Vector2Int();
    
    private Tower currentTower; 
    private int currentUpgradeLevel = 0; 
    private bool isAOETower = false;
    private bool isRoadblock=false;
    [SerializeField] int maxTowerCount;
    [SerializeField]private int maxAoeTowerCount;
    [SerializeField]private int maxRoadblockCount;
    
    

    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        pathfinder = FindObjectOfType<Pathfinder>();
        towerManager = FindObjectOfType<TowerManager>();
        towerDeletionCounter = FindObjectOfType<TowerDeletionCounter>();
    }

    private void Start()
    {
        if (gridManager != null)
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);

            if (!isPlaceable)
            {
                gridManager.BlockNode(coordinates);
            }
            
            
            
        }
        
    }


    public bool GetIsplaceable()
    {
        return isPlaceable;
    }
    
    private void OnMouseOver()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            PlaceRoadBlock();
        }
        if(Input.GetKeyDown(KeyCode.I))
        {
            PlaceAOETower();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            PlaceTower();
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            UpgradeTower();
        }

        if (Input.GetKeyDown(KeyCode.Z) && currentTower != null && towerDeletionCounter.currentTowerDeletionCount<deleteLimitPerLevel)
        {  
                towerDeletionCounter.TowerDeletionCounterMethod();
                DeleteTower();
        }
        
    }

    void PlaceRoadBlock()
    {
        if (gridManager.GetNode(coordinates).isWalkable && !pathfinder.WillBlockPath(coordinates) && roadblockPools[0].currentTowerCount<maxRoadblockCount)
        {
            if (currentTower == null )
            {
                Tower RoadblockInstance = roadblockPools[0].GetFromPool();
                if (RoadblockInstance != null)
                {
                    RoadblockInstance.transform.position = gridManager.GetPositionFromCoordinates(coordinates);
                    RoadblockInstance.gameObject.SetActive(true);
                    RoadblockInstance.WithdrawMoneyToCreateTower(RoadblockInstance);

                    gridManager.BlockNode(coordinates);
                    pathfinder.NotifyRecievers();

                    currentTower = RoadblockInstance;
                    currentUpgradeLevel = 0;
                    isAOETower = false;
                    isRoadblock = false;
                    

                }
            }
                
        }
    }

    void PlaceAOETower()
    {
        if (gridManager.GetNode(coordinates).isWalkable && !pathfinder.WillBlockPath(coordinates) && aoeTowerPools[0].currentTowerCount<maxAoeTowerCount)
        {
            if (currentTower == null )
            {
                // Place the initial AOE tower
                Tower aoeTowerInstance = aoeTowerPools[0].GetFromPool();
                if (aoeTowerInstance != null)
                {
                    aoeTowerInstance.transform.position = gridManager.GetPositionFromCoordinates(coordinates);
                    aoeTowerInstance.gameObject.SetActive(true);
                    aoeTowerInstance.WithdrawMoneyToCreateTower(aoeTowerInstance);

                    gridManager.BlockNode(coordinates);
                    pathfinder.NotifyRecievers();

                    currentTower = aoeTowerInstance;
                    currentUpgradeLevel = 0;
                    isAOETower = true;
                    isRoadblock = false;
                    
                }
            }
        }
    }
    
    
    
    void PlaceTower()
    {
        if (gridManager.GetNode(coordinates).isWalkable && !pathfinder.WillBlockPath(coordinates) && towerPools[0].currentTowerCount< maxTowerCount )
        {
            if (currentTower == null )
            {
                // Place the initial tower
                Tower towerInstance = towerPools[0].GetFromPool();
                if (towerInstance != null)
                {
                    towerInstance.transform.position = gridManager.GetPositionFromCoordinates(coordinates);
                    towerInstance.gameObject.SetActive(true);
                    towerInstance.WithdrawMoneyToCreateTower(towerInstance);

                    gridManager.BlockNode(coordinates);
                    pathfinder.NotifyRecievers();

                    currentTower = towerInstance;
                    currentUpgradeLevel = 0;
                    isAOETower = false;
                    isRoadblock = false;
                }
            }
                
        }
    }
    private void UpgradeTower()
    {
        if (isAOETower && !isRoadblock)
        {
            if (currentUpgradeLevel < aoeTowerPools.Count - 1)
            {
                aoeTowerPools[currentUpgradeLevel].ReturnToPool(currentTower);

                currentUpgradeLevel++;
                Tower upgradedAOETower = aoeTowerPools[currentUpgradeLevel].GetFromPool();
                if (upgradedAOETower != null)
                {
                    upgradedAOETower.transform.position = transform.position;
                    upgradedAOETower.gameObject.SetActive(true);
                    upgradedAOETower.WithdrawMoneyToUpgradeTower(upgradedAOETower);
                    currentTower = upgradedAOETower;
                }
            }
        }
        else if(!isAOETower && !isRoadblock)
        {
            if (currentUpgradeLevel < towerPools.Count - 1 && !isRoadblock)
            {
                towerPools[currentUpgradeLevel].ReturnToPool(currentTower);

                currentUpgradeLevel++;
                Tower upgradedTower = towerPools[currentUpgradeLevel].GetFromPool();
                if (upgradedTower != null)
                {
                    upgradedTower.transform.position = transform.position;
                    upgradedTower.gameObject.SetActive(true);
                    upgradedTower.WithdrawMoneyToUpgradeTower(upgradedTower);
                    currentTower = upgradedTower;
                }
            }
        }
    }
    
    private void DeleteTower()
    {
        if (isAOETower)
        {
            aoeTowerPools[currentUpgradeLevel].ReturnToPool(currentTower);
            currentTower.ResellTower(currentTower,currentUpgradeLevel);
        }
        else if(isRoadblock)
        {
            roadblockPools[0].ReturnToPool(currentTower);
            currentTower.ResellTower(currentTower,0);
        }
        else
        {
            towerPools[currentUpgradeLevel].ReturnToPool(currentTower);
            currentTower.ResellTower(currentTower,currentUpgradeLevel);
        }

        currentTower = null;
        currentUpgradeLevel = 0;
        
        
        // Unblock the node in the grid manager
        gridManager.UnblockNode(coordinates);
        pathfinder.NotifyRecievers();
    }
}
