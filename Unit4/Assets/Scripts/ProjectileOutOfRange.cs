using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileOutOfRange : MonoBehaviour
{
    private static readonly float _range = 20.0f;


    void Update()
    {
        if (Mathf.Abs(transform.position.x) > _range ||
            Mathf.Abs(transform.position.z) > _range)
        {
            Destroy(gameObject);
        }
    }
}
