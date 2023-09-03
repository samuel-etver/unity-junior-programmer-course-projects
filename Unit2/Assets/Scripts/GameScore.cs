using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScore : MonoBehaviour
{
    public int Score = 0;


    public void IncScore()
    {
        Score++;
        Debug.Log("Score=" + Score);
    }
}
