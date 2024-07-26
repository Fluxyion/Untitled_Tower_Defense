using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class AllyBaseHealth : MonoBehaviour
{
    [SerializeField] float BaseStartingHitPoints = 100;
    [SerializeField]float BaseCurrentHitPoints;
    [SerializeField] TextMeshProUGUI healthDısplay;

    private void Start()
    {
        BaseCurrentHitPoints = BaseStartingHitPoints;
        healthDısplay.text = "Health:" + BaseCurrentHitPoints;
        
    }

    public void BaseTakenDamage(float damage)
    {
        if (BaseCurrentHitPoints <= 0)
        {
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.buildIndex);
        }
        else
        {
            BaseCurrentHitPoints -= Mathf.Abs(damage);
            healthDısplay.text = "Health:" + BaseCurrentHitPoints;


        }
        
    }
}
