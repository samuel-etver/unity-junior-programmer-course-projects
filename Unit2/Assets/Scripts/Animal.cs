using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Animal : MonoBehaviour
{
    public bool Agressive = false;
    public Slider HungerSlider;
    public int AmountToBeFed = 0;
    public GameObject GameManager;

    private int _currentFedAmount = 0;


    private void Start()
    {
        HungerSlider.value = _currentFedAmount;
        HungerSlider.maxValue = AmountToBeFed;
    }


    public bool Feed()
    {
        if (_currentFedAmount < AmountToBeFed)
        {
            HungerSlider.value =  ++_currentFedAmount;
            return true;
        }
        return false;
    }
}


