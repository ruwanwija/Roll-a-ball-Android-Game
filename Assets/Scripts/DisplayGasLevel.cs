using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayGasLevel : MonoBehaviour
{
    Image fill;
    float maxGasLevel;
    float currentGasLevel;
    
    void Start()
    {
        fill = GetComponent<Image>();
        maxGasLevel = FindObjectOfType<RocketController>().maxGasLevel;
    }
    
    void Update()
    {
        if (FindObjectOfType<RocketController>())
        currentGasLevel = FindObjectOfType<RocketController>().currentGasLevel;
        
        fill.fillAmount = currentGasLevel / maxGasLevel;
    }
}