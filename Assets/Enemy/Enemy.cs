using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int goldReward = 25;
    //[SerializeField] int goldPenalty = 25;
    [SerializeField] float damage = 5f;
     AllyBaseHealth AllyBase;

    Bank bank;
    // Start is called before the first frame update
    void Start()
    {
        bank = FindObjectOfType<Bank>();
        AllyBase = FindObjectOfType<AllyBaseHealth>();
    }

    public void RewardGold()
    {
        if (bank == null) {return;}
        bank.Deposit(goldReward);
    }
    //public void StealGold()
    //{
    //if (bank == null) {return;}
      //  bank.Withdraw(goldPenalty);
   // }
   public void DealDamage()
   {
       if(AllyBase ==null) { return; }
       AllyBase.BaseTakenDamage(damage);
   }
}
