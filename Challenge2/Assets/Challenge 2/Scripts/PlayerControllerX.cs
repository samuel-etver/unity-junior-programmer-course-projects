using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public GameObject DogPrefab;


    void Update()
    {
        // On spacebar press, send dog
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(DogPrefab, transform.position, DogPrefab.transform.rotation);
        }
    }
}
