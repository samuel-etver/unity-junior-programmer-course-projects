﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoveForwardX : MonoBehaviour
{
    public float Speed;


    void Update()
    {
        transform.Translate(Vector3.forward * (Speed * Time.deltaTime));
    }
}
