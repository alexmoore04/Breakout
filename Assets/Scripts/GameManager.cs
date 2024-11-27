using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public HighScore saveData = new HighScore();
    public int score;

    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text livesText;
    [SerializeField] private TMP_Text gameOverScoreText;
    [SerializeField] private TMP_Text highScoreText;
    [SerializeField] private GameObject gameUI;
    [SerializeField] private GameObject gameOverUI;

    [SerializeField] private GameObject bricks;

    private BallMovement ballMovement;

    [SerializeField] private UnityEvent _onWin;

    private void Start()
    {
        ballMovement = (BallMovement)GameObject.Find("Ball").GetComponent("BallMovement");
    }

    public void ScoreUpdate()
    {
        score++;
        scoreText.text = "Score: " + score;
        if (score == 35)
        {
            _onWin.Invoke();
        }
    }

    public void GameOver()
    {
        loadFromJson();
        gameUI.SetActive(false);
        gameOverUI.SetActive(true);
        if (score > saveData.highScore)
        {
            UpdateSaveData();
            highScoreText.text = "New High Score: " + score;
            gameOverScoreText.text = "Score: " + score;
        }
        else
        {
            highScoreText.text = "High Score: " + saveData.highScore;
            gameOverScoreText.text = "Score: " + score;
        }
        score = 0;
    }

    public void Restart()
    {
        gameUI.SetActive(true);
        gameOverUI.SetActive(false);
        ballMovement.lives = 3;
        livesText.text = "Lives: 3";
        ballMovement.RestartBall();
        score = 0;
        scoreText.text = "Score: 0";
        for (int i = 0; i < bricks.transform.childCount; i++)
        {
            bricks.transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    public void SaveToJson()
    {
        string data = JsonUtility.ToJson(saveData);
        string filePath = Application.persistentDataPath + "/SaveData.json";
        Debug.Log("FilePath: " + filePath);
        System.IO.File.WriteAllText(filePath, data);
        Debug.Log("Data Saved");
    }

    public void loadFromJson()
    {
        string filePath = Application.persistentDataPath + "/SaveData.json";
        string save = System.IO.File.ReadAllText(filePath);
        if (save.Length > 0)
        {
            saveData = JsonUtility.FromJson<HighScore>(save);
        }
    }
    public void UpdateSaveData()
    {
        saveData.highScore = score;
        SaveToJson();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

    public class HighScore
    {
        public int highScore;
    }
