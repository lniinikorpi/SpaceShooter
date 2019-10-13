using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance = null;

    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Image _livesDisplay;
    [SerializeField]
    private List<Sprite> _livesImages = new List<Sprite>();
    private Player _player;
    [SerializeField]
    private GameObject _gameOverText;
    [SerializeField]
    private GameObject _restartText;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if(_player == null)
        {
            Debug.LogError("Player is NULL!");
        }

        _scoreText.text = "Score: 0";
        _livesDisplay.sprite = _livesImages[3];
        _gameOverText.SetActive(false);
        _restartText.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            if(_player == null)
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    public void UpdateScoreText()
    {
        _scoreText.text = "Score: " + _player.GetScore();
    }

    public void UpdateLivesDisplay(int health)
    {
        _livesDisplay.sprite = _livesImages[health];
    }

    public void GameOver()
    {
        _gameOverText.SetActive(true);
        _restartText.SetActive(true);
    }
}
