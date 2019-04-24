using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public AudioSource MusicSource;
    public AudioClip WinMusic;
    public AudioClip LoseMusic;

    public Text ScoreText;
    public Text RestartText;
    public Text GameoverText;
    public Text WinText;

    private BG_Scroller backgroundScroller;

    private bool gameOver;
    private bool restart;
    private int score;

    void Start()
    {
        gameOver = false;
        restart = false;
        RestartText.text = "";
        GameoverText.text = "";
        WinText.text = "";
        score = 0;
        UpdateScore();
        StartCoroutine(SpawnWaves());

        GameObject backgroundScrollerObject = GameObject.FindWithTag("Background");
        backgroundScroller = backgroundScrollerObject.GetComponent<BG_Scroller>();
    }

    void Update()
    {
        if (Input.GetKey("escape"))
            Application.Quit();
        if (restart)
        {
            if (Input.GetKeyDown (KeyCode.Q))
            {
                SceneManager.LoadScene("Space Shooter");
            }
        }
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range (0,hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                RestartText.text = "Press 'Q' to Restart";
                restart = true;
                break;
            }
        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        ScoreText.text = "Points: " + score;
        if (score >= 100)
        {
            WinText.text = "Game Created by: Franco Parodi";
            gameOver = true;
            restart = true;
            MusicSource.clip = WinMusic;
            MusicSource.loop = false;
            MusicSource.Play();
            backgroundScroller.scrollSpeed = -15.0f;
        }
    }
    public void GameOver ()
    {
        GameoverText.text = "Game Over!";
        gameOver = true;
        MusicSource.clip = LoseMusic;
        MusicSource.loop = false;
        MusicSource.Play();
    }
}
