using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetX : MonoBehaviour
{
    public int PointValue;
    public GameObject ExplosionFx;

    private static readonly float _timeOnScreen = 1.0f;
    private static readonly float _minValueX = -3.75f; // the x value of the center of the left-most square
    private static readonly float _minValueY = -3.75f; // the y value of the center of the bottom-most square
    private static readonly float _spaceBetweenSquares = 2.5f; // the distance between the centers of squares on the game board

    private GameManagerX _gameManagerX;
    

    void Start()
    {
        _gameManagerX = GameObject.Find("Game Manager").GetComponent<GameManagerX>();

        transform.position = RandomSpawnPosition(); 
        StartCoroutine(RemoveObjectRoutine()); // begin timer before target leaves screen
    }


    // When target is clicked, destroy it, update score, and generate explosion
    private void OnMouseDown()
    {
        if (_gameManagerX.IsGameActive)
        {
            Destroy(gameObject);
            _gameManagerX.UpdateScore(PointValue);
            Explode();
        }              
    }


    // Generate a random spawn position based on a random index from 0 to 3
    Vector3 RandomSpawnPosition()
    {
        float spawnPosX = _minValueX + (RandomSquareIndex() * _spaceBetweenSquares);
        float spawnPosY = _minValueY + (RandomSquareIndex() * _spaceBetweenSquares);

        Vector3 spawnPosition = new (spawnPosX, spawnPosY, 0);
        return spawnPosition;
    }


    // Generates random square index from 0 to 3, which determines which square the target will appear in
    int RandomSquareIndex ()
    {
        return Random.Range(0, 4);
    }


    // If target that is NOT the bad object collides with sensor, trigger game over
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);

        if (other.gameObject.CompareTag("Sensor") && !gameObject.CompareTag("Bad"))
        {
            _gameManagerX.GameOver();
        } 
    }


    // Display explosion particle at object's position
    void Explode ()
    {
        Instantiate(ExplosionFx, transform.position, ExplosionFx.transform.rotation);
    }


    // After a delay, Moves the object behind background so it collides with the Sensor object
    IEnumerator RemoveObjectRoutine ()
    {
        yield return new WaitForSeconds(_timeOnScreen);
        if (_gameManagerX.IsGameActive)
        {
            transform.Translate(Vector3.forward * 5, Space.World);
        }
    }
}
