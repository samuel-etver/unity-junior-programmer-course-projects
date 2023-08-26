using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject Player;
    public Vector3 Offset = new Vector3(0, 5, -7);


    void Start()
    {
        
    }


    void LateUpdate()
    {
        transform.position = Player.transform.position + Offset;
    }
}
