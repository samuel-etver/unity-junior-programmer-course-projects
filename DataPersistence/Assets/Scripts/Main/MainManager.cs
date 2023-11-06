using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text BestScoreText;
    public GameObject GameOverText;
    
    private bool _started = false;
    private int _points;
    
    private bool _gameOver = false;

    private GlobalStorage _globalStorage;

    
    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.OnDestroyed.AddListener(AddPoint);
            }
        }

        _globalStorage = GlobalStorage.Instance;

        UpdateBestScore();
    }


    private void Update()
    {
        if (!_started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (_gameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }


    void AddPoint(int point)
    {
        _points += point;
        ScoreText.text = $"Score : {_points}";
    }


    public void GameOver()
    {
        _gameOver = true;
        GameOverText.SetActive(true);
        if (_points > _globalStorage.BestScore)
        {
            _globalStorage.BestScore = _points;
            UpdateBestScore();
        }
    }


    private void UpdateBestScore()
    {
        BestScoreText.text = $"Best Score: {_globalStorage.PlayerName}: {_globalStorage.BestScore}";
    }
}
