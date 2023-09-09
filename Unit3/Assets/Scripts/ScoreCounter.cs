using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        Debug.Log("OK");
        if (collision.gameObject.GetComponent<ScoreItem>())
        {
            Debug.Log("OO!");
        }
    }
}
