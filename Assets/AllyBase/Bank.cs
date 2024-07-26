using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Bank : MonoBehaviour
{
    [SerializeField]int startingBalance = 150;
    [SerializeField]int currentBalance;
    [SerializeField] TextMeshProUGUI goldDisplay;
    public int CurrentBalance { get { return currentBalance; } }

    private void Awake()
    {
        currentBalance = startingBalance;
        updateDisplay();
    }

    public void Deposit(int amount)
    {
        currentBalance += Mathf.Abs(amount);
        updateDisplay();
    }
    public void Withdraw(int amount)
    {
        currentBalance -= Mathf.Abs(amount);
        updateDisplay();

    }

    void updateDisplay()
    {
        goldDisplay.text = "Gold:" + currentBalance;
    }

}
