using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public int Score = 0;

    [HideInInspector]
    public bool SuperSpeed = false;


    public void AddScore(int value)
    {
        Score += value;
    }


    private void Update()
    {
        SuperSpeed = Input.GetKey(KeyCode.RightShift) ||
                     Input.GetKey(KeyCode.LeftShift);

    }
}
