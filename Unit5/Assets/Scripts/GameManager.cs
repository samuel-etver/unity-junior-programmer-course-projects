using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> Targets;
    public TextMeshProUGUI ScoreText;

    private int _score;

    private static readonly float _spawnRate = 1.0f;


    private void Start()
    {
        StartCoroutine(SpawnTargets());
        _score = 0;
        ScoreText.text = "Score: " + _score;
    }


    private IEnumerator  SpawnTargets()
    {
        while(true) {
            yield return new WaitForSeconds(_spawnRate);
            int index = Random.Range(0, Targets.Count);
            Instantiate(Targets[index]);
        }
    }


    private void UpdateScore(int scoreToAdd)
    {
        _score += scoreToAdd;
        ScoreText.text = "Score: " + _score;
    }
}
