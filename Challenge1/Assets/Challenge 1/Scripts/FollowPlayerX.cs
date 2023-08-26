using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerX : MonoBehaviour
{
    public GameObject Plane;
    public Vector3 Offset;


    void Update()
    {
        transform.position = Plane.transform.position + Offset;
    }
}
