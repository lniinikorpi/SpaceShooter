using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame_button : MonoBehaviour
{
    // Start is called before the first frame update
    public void LoadSceneOnClick(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
