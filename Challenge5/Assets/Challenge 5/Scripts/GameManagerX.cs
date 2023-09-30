using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerX : MonoBehaviour
{
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI GameOverText;
    public TextMeshProUGUI GameTimeText;
    public GameObject TitleScreen;
    public Button RestartButton; 

    public List<GameObject> targetPrefabs;

    private int _score;
    private float _spawnRate = 1.5f;
    private float _gameTime = 0.0f;

    [HideInInspector]
    public bool IsGameActive;

    private static readonly float _spaceBetweenSquares = 2.5f; 
    private static readonly float _minValueX = -3.75f; //  x value of the center of the left-most square
    private static readonly float _minValueY = -3.75f; //  y value of the center of the bottom-most square
    

    // Start the game, remove title screen, reset score, and adjust spawnRate based on difficulty button clicked
    public void StartGame(int difficulty)
    {
        _spawnRate /= 5;

        float spawnRateK = 10.0f;
        switch(difficulty)
        {
            case 1:
                spawnRateK = 6.0f;
                break;
            case 2:
                spawnRateK = 4.0f;
                break;
            case 3:
                spawnRateK = 2.0f;
                break;
        }
        _spawnRate *= spawnRateK;

        IsGameActive = true;
        StartCoroutine(SpawnTarget());
        _score = 0;
        _gameTime = 60.0f;
        UpdateScore(0);
        UpdateGameTime(0);
        TitleScreen.SetActive(false);
    }


    // While game is active spawn a random target
    IEnumerator SpawnTarget()
    {
        while (IsGameActive)
        {
            yield return new WaitForSeconds(_spawnRate);
            int index = Random.Range(0, targetPrefabs.Count);

            if (IsGameActive)
            {
                Instantiate(targetPrefabs[index], RandomSpawnPosition(), targetPrefabs[index].transform.rotation);
            }            
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
    int RandomSquareIndex()
    {
        return Random.Range(0, 4);
    }


    // Update score with value from target clicked
    public void UpdateScore(int scoreToAdd)
    {
        _score += scoreToAdd;
        ScoreText.text = "Score: " + _score;
    }


    private void UpdateGameTime(float timeToAdd)
    {
        float time = _gameTime + timeToAdd;
        if (time < 0)
            time = 0;
        _gameTime = time;

        GameTimeText.text = "Time: " + Mathf.FloorToInt(_gameTime);
    }


    // Stop game, bring up game over text and restart button
    public void GameOver()
    {
        GameOverText.gameObject.SetActive(true);
        RestartButton.gameObject.SetActive(true);
        IsGameActive = false;
    }


    // Restart game by reloading the scene
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    private void Update()
    {
        if(IsGameActive)
        {
            UpdateGameTime(-Time.deltaTime);
            if(_gameTime == 0)
            {
                GameOver();
            }
        }
    }
}
