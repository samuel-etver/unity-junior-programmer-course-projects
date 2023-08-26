using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera Camera1;
    public Camera Camera2;

    
    void Start()
    {
        Camera1.gameObject.SetActive(true);
        Camera2.gameObject.SetActive(false);
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            Camera1.gameObject.SetActive(!Camera1.gameObject.activeSelf);
            Camera2.gameObject.SetActive(!Camera2.gameObject.activeSelf);
        }        
    }
}
