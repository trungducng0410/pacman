using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreenManager : MonoBehaviour
{
    public void Quit()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }

    public void Level1()
    {
        SceneManager.LoadScene("Level1");
    }
}
