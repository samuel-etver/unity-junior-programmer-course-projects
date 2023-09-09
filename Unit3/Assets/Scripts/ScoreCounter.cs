using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    public GameManager GameManager;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<ScoreItem>())
        {
            GameManager.AddScore(1);
            Debug.Log("Score=" + GameManager.Score);
        }
    }
}
