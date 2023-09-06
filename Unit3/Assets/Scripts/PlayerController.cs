using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float JumpForce = 0.0f;
    public bool IsOnGround { get { return _isOnGround; } }

    private Rigidbody _rigidbody;
    private bool _isOnGround = true;
    private Animator _animator;



    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isOnGround)
        {
            _rigidbody.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            _isOnGround = false;
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        _isOnGround = true;
    }
}
