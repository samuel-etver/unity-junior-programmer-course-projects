using System;
using System.Collections.Generic;
using UnityEngine;

public class CongratScript : MonoBehaviour
{
    public TextMesh Text;
    public ParticleSystem SparksParticles;   
    
    private readonly List<String> TextToDisplay = new ();
    
    private float _rotatingSpeed;
    private float _timeToNextText;

    private int _currentText;
    

    void Start()
    {
        _timeToNextText = 0.0f;
        _currentText = 0;
        
        _rotatingSpeed = 100.0f;

        TextToDisplay.Add("Congratulation");
        TextToDisplay.Add("All Errors Fixed");

        Text.text = TextToDisplay[0];
        
        SparksParticles.Play();
    }


    void Update()
    {
        _timeToNextText += Time.deltaTime;

        if (_timeToNextText > 1.5f)
        {
            _timeToNextText = 0.0f;
            
            _currentText++;
            if (_currentText >= TextToDisplay.Count)
            {
                _currentText = 0;
            }


            Text.text = TextToDisplay[_currentText];
        }

        SparksParticles.transform.RotateAround(transform.position, Vector3.up, _rotatingSpeed * Time.deltaTime);
    }
}
