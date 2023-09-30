using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
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
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI LivesText;
    public TextMeshProUGUI GameOverText;
    public Button RestartButton;
    public Slider VolumeSlider;
    public GameObject StartPanel;
    public GameObject PausePanel;

    [HideInInspector]
    public GameDifficulty Difficulty;

    [HideInInspector]
    public bool isGameActive;

    [HideInInspector]
    public int Score;

    [HideInInspector]
    public int Lives;

    [HideInInspector]
    public bool Paused;

    private static readonly float _spawnRate = 1.0f;

    private static float _audioVolume = 1.0f;


    private void Start()
    {
        isGameActive = false;

        Score = 0;
        Lives = 3;

        Paused = false;

        UpdateScore(0);
        UpdateLives(0);

        VolumeSlider.value = _audioVolume;
        SetVolume();
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
        Score += scoreToAdd;
        ScoreText.text = "Score: " + Score;
    }


    public void UpdateLives(int livesToAdd)
    {
        int newLives = Lives + livesToAdd;

        if (newLives < 0)
        {
            newLives = 0;
        }

        Lives = newLives;

        LivesText.text = "Lives: " + Lives;

        if (Lives == 0)
        {
            GameOver();
        }
    }


    public void DecLives()
    {
        UpdateLives(-1);
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
        isGameActive = true;
        Difficulty = difficulty;

        StartPanel.gameObject.SetActive(false);

        StartCoroutine(SpawnTargets());
    }


    public void OnVolumeChange()
    {
        SetVolume();
    }


    private void SetVolume()
    {
        var audioSource = GetComponent<AudioSource>();
       
        _audioVolume =
        audioSource.volume = VolumeSlider.value;       
    }


    private void Update()
    {
        if(isGameActive)
        {
            if(Input.GetKeyDown(KeyCode.P))
            {
                TogglePause();
            }
        }
    }


    private void TogglePause()
    {
        Paused = !Paused;

        Time.timeScale = Paused ? 0.0f : 1.0f;
        PausePanel.gameObject.SetActive(Paused);

    }
}
