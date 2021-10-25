using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    Text highScore;
    Text bestTime;

    Text score;
    Text timer;
    Text countDown;
    GameObject scaredTimer;
    Text scaredTime;
    GameObject lives;
    public bool started;
    float time;
    SaveGameManager saveGameManager;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        time = 0;
        started = false;
        saveGameManager = gameObject.GetComponent<SaveGameManager>();
        GetPlayerPrefs();
    }

    private void GetPlayerPrefs()
    {
        highScore = GameObject.FindGameObjectWithTag("HighScore").GetComponent<Text>();
        bestTime = GameObject.FindGameObjectWithTag("BestTime").GetComponent<Text>();

        if (highScore != null && bestTime != null)
        {
            if (PlayerPrefs.GetString(saveGameManager.saveScoreKey) != null)
            {
                highScore.text = PlayerPrefs.GetInt(saveGameManager.saveScoreKey).ToString();
                bestTime.text = FormatTime(PlayerPrefs.GetFloat(saveGameManager.saveTimeKey));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (started)
        {
            if (timer != null)
            {
                time += Time.deltaTime;

                timer.text = FormatTime(time);
            }
        }
    }

    private string FormatTime(float time)
    {
        int timeInInt = (int)time;
        int min = timeInInt / 60;
        int sec = timeInInt % 60;
        float mil = (time * 1000) % 1000;

        string timeFormat = String.Format("{0:00}:{1:00}:{2:00}",
        min, sec, mil);

        return timeFormat;
    }

    public void LoadLevelOne()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        Destroy(gameObject);
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }

    public void UpdateScore(int num)
    {
        score.text = num.ToString();
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 0)
        {
            GetPlayerPrefs();
        }

        if (scene.buildIndex == 1)
        {
            Button exitButton = GameObject.FindWithTag("ExitButton").GetComponent<Button>();
            exitButton.onClick.AddListener(Exit);

            score = GameObject.FindGameObjectWithTag("Score").GetComponent<Text>();
            countDown = GameObject.FindGameObjectWithTag("CountDown").GetComponent<Text>();
            timer = GameObject.FindGameObjectWithTag("Timer").GetComponent<Text>();
            scaredTimer = GameObject.FindGameObjectWithTag("ScaredTime");
            scaredTimer.SetActive(false);
            lives = GameObject.FindGameObjectWithTag("Life");

            StartCoroutine(DisplayStartSequence());
        }
    }


    IEnumerator DisplayStartSequence()
    {
        if (countDown != null)
        {
            countDown.text = "3";
            yield return new WaitForSeconds(1f);
            countDown.text = "2";
            yield return new WaitForSeconds(1f);
            countDown.text = "1";
            yield return new WaitForSeconds(1f);
            countDown.text = "GO!";
            yield return new WaitForSeconds(1f);
            countDown.enabled = false;
            started = true;
        }
    }

    public void DisplayGameOver()
    {
        StartCoroutine(GameOverHandler());
    }

    IEnumerator GameOverHandler()
    {
        countDown.enabled = true;
        countDown.text = "GAME OVER";
        started = false;
        int scoreVal = int.Parse(score.text);
        if (scoreVal > PlayerPrefs.GetInt(saveGameManager.saveScoreKey))
        {
            saveGameManager.SaveScore(scoreVal);
            saveGameManager.SaveTime(time);
        }

        if (scoreVal == PlayerPrefs.GetInt(saveGameManager.saveScoreKey) && time < PlayerPrefs.GetFloat(saveGameManager.saveTimeKey))
        {
            saveGameManager.SaveScore(scoreVal);
            saveGameManager.SaveTime(time);
        }
        yield return new WaitForSeconds(3f);
        Exit();
    }

    public void UpdateLife(int count)
    {
        lives.GetComponent<RectTransform>().sizeDelta = new Vector2(lives.GetComponent<RectTransform>().sizeDelta.y * count, lives.GetComponent<RectTransform>().sizeDelta.y);
    }

    public void UpdateScaredTime()
    {
        StartCoroutine(ScaredTimeHandler());
    }

    IEnumerator ScaredTimeHandler()
    {
        scaredTimer.SetActive(true);
        scaredTime = scaredTimer.GetComponent<Text>();
        scaredTime.text = "10";
        yield return new WaitForSeconds(1f);
        scaredTime.text = "9";
        yield return new WaitForSeconds(1f);
        scaredTime.text = "8";
        yield return new WaitForSeconds(1f);
        scaredTime.text = "7";
        yield return new WaitForSeconds(1f);
        scaredTime.text = "6";
        yield return new WaitForSeconds(1f);
        scaredTime.text = "5";
        yield return new WaitForSeconds(1f);
        scaredTime.text = "4";
        yield return new WaitForSeconds(1f);
        scaredTime.text = "3";
        yield return new WaitForSeconds(1f);
        scaredTime.text = "2";
        yield return new WaitForSeconds(1f);
        scaredTime.text = "1";
        yield return new WaitForSeconds(1f);
        scaredTimer.SetActive(false);
    }
}
