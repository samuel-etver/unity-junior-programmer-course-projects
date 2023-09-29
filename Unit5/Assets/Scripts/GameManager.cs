using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public enum GameDifficulty
{
    Easy,
    Medium,
    Hard
}


public class GameManager : MonoBehaviour
{
    public List<GameObject> Targets;
    public TextMeshProUGUI GameTitle;
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI GameOverText;
    public Button RestartButton;
    public Button EasyButton;
    public Button MediumButton;
    public Button HardButton;

    [HideInInspector]
    public GameDifficulty Difficulty;

    [HideInInspector]
    public bool isGameActive = true;

    private int _score;

    private static readonly float _spawnRate = 1.0f;


    private void Start()
    {
        _score = 0;
        UpdateScore(0);
    }


    private IEnumerator  SpawnTargets()
    {
        while(isGameActive) {
            yield return new WaitForSeconds(_spawnRate);
            int index = Random.Range(0, Targets.Count);
            var targetGameObject = Instantiate(Targets[index]);
            var target = targetGameObject.GetComponent<Target>();
            target.GameManager = this;
        }
    }


    public void UpdateScore(int scoreToAdd)
    {
        _score += scoreToAdd;
        ScoreText.text = "Score: " + _score;
    }


    public void GameOver ()
    {
        isGameActive = false;
        RestartButton.gameObject.SetActive(true);
        GameOverText.gameObject.SetActive(true);
    } 


    public void OnRestartButtonClick ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    public void OnHardButtonClick()
    {
        StartGame(GameDifficulty.Hard);
    }


    public void OnMediumButtonClick()
    {
        StartGame(GameDifficulty.Medium);
    }


    public void OnEasyButtonClick()
    {
        StartGame(GameDifficulty.Easy);
    }


    private void StartGame(GameDifficulty difficulty)
    {
        Difficulty = difficulty;

        GameTitle.gameObject.SetActive(false);

        EasyButton.gameObject.SetActive(false);
        MediumButton.gameObject.SetActive(false);
        HardButton.gameObject.SetActive(false);

        StartCoroutine(SpawnTargets());
    }
}
