using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Speed = 10.0f;
    public float XRange = 10.0f;
    public float ZMax = 15.0f;
    public float ZMin = -1.0f;

    public GameObject ProjectilePrefab;

    public GameObject GameManager;
    private PlayerLives _playerLives;


    private void Start()
    {
        _playerLives = GameManager.GetComponent<PlayerLives>();    
    }


    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * (horizontalInput * Time.deltaTime * Speed));

        float x = transform.position.x;
        if (x < -XRange)
        {
            transform.position = new Vector3(-XRange, transform.position.y, transform.position.z);
        }
        else if (x > XRange)
        {
            transform.position = new Vector3(XRange, transform.position.y, transform.position.z);
        }


        float verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.forward * (verticalInput * Time.deltaTime * Speed));

        float z = transform.position.z;
        if (z < ZMin) 
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, ZMin);
        }
        else if (z > ZMax)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, ZMax);
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(ProjectilePrefab, transform.position, ProjectilePrefab.transform.rotation);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Animal>(out var animal))
        {
            if (animal.Agressive)
            { 
                _playerLives.DecLive();
                if (_playerLives.IsDead())
                {
                    Destroy(gameObject);
                    Debug.Log("Game Over!");
                }
            }
        }
    }
}
