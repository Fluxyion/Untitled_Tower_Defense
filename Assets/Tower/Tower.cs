using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quaternion = System.Numerics.Quaternion;

public class Tower : MonoBehaviour
{
    [SerializeField] private int towerCost = 40;
    [SerializeField] private int towerUpgradeCost = 100;
    [SerializeField] private int towerResellPrice = 30;
    public void WithdrawMoneyToCreateTower(Tower tower)
    {
        Bank bank = FindObjectOfType<Bank>();
        if (bank == null)
        {
            return;
        }
        if(bank.CurrentBalance>=towerCost)
        {
            bank.Withdraw(towerCost);
        }
    }

    public void WithdrawMoneyToUpgradeTower(Tower tower)
    {
        Bank bank = FindObjectOfType<Bank>();
        if (bank == null)
        {
            return;
        }
        if(bank.CurrentBalance>=towerCost)
        {
            bank.Withdraw(towerUpgradeCost);
        }
        
    }

    public void ResellTower(Tower tower, int upgradeLevel)
    {
        Bank bank = FindObjectOfType<Bank>();
        if (bank == null)
        {
            return;
        }
        else
        {
            bank.Deposit(towerResellPrice+(upgradeLevel*towerUpgradeCost));
        }
    }
    
}
