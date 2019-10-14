using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    private bool _gameOver = false;
    // Start is called before the first frame update
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

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            if (_gameOver)
            {
                SceneManager.LoadScene("Game"); 
            }
        }
    }

    public void SetGameOver(bool gameOver)
    {
        _gameOver = gameOver;
    }
}
