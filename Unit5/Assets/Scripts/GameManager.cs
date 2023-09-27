using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> Targets;
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI GameOverText;
    public Button RestartButton;

    [HideInInspector]
    public bool isGameActive = true;

    private int _score;

    private static readonly float _spawnRate = 1.0f;


    private void Start()
    {
        StartCoroutine(SpawnTargets());
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


    public void RestartGame ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
