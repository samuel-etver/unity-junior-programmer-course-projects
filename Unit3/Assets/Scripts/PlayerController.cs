using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameManager GameManager;

    public float JumpForce = 0.0f;

    private Rigidbody _rigidbody;

    private bool IsOnGround {
        get { return _jumpCount == 0; }
    }
    private int _jumpCount = 0;

    private Animator _animator;

    private bool _startWalk = true;
    private static readonly float _walkSpeed = 3.0f;


    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _animator.SetFloat("Speed_f", 0.4f);
        Invoke(nameof(StartRun), Config.StartupDelay);
    }


    void Update()
    {
        if (_startWalk)
        {
            float deltaX = Time.deltaTime * _walkSpeed;
            transform.position = new Vector3(transform.position.x + deltaX,
                                             transform.position.y,
                                             transform.position.z);
            return;
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_jumpCount < 2)
            {
                _jumpCount++;
                _rigidbody.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            }
        }
    }


    private void StartRun()
    {
        _startWalk = false;
        _animator.SetFloat("Speed_f", 1.0f);
    }


    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collision");
        if (collision.gameObject.GetComponent<Ground>())
        {
            _jumpCount = 0;
        }
        else if (collision.gameObject.GetComponent<Obstacle>())
        {
            _animator.SetBool("Death_b", true);
            _animator.SetInteger("DeathType_int", 1);
            GameManager.GameOver = true;
            Debug.Log("Game Over!");
        }

    }
}
