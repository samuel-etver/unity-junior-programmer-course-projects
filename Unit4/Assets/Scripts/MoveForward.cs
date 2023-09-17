using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoveForward : MonoBehaviour
{ 
    public float Speed = 5.0f;


    void Update()
    {
        transform.Translate(new Vector3(0, Time.deltaTime * Speed, 0));
    }
}
