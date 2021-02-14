using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMan : MonoBehaviour
{
    public GameObject virus;
    public GameObject virusHolder;
    private bool stop = false;

    public int Score;
    public GameObject player;
    public GameObject gameOverPanel;
    public Text ScoreBoard;
    public Text scoreText;


    public float speed;



    private void Start()
    {
        StartCoroutine(SendVirus());
        gameOverPanel.SetActive(false);
    }

    private void Update()
    {
        if(player == null && !gameOverPanel.activeSelf)
        {
            gameOverPanel.SetActive(true);
            GameOverPanel();
            ScoreBoard.gameObject.SetActive(false);
        }

        ScoreBoard.text = Score.ToString();
    }

    IEnumerator SendVirus()
    {

        while (true)
        {
            yield return new WaitForSeconds(Random.Range(0.3f, 1.5f));
            Vector2 p = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0, Screen.width), Screen.height));
            Quaternion q = Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360)));
            float scale = Random.Range(0.5f, 2.0f);
            GameObject v = Instantiate(virus, virusHolder.transform);
            v.transform.position = p;
            v.transform.rotation = q;
            v.transform.localScale = scale * Vector3.one;
            if (stop) break;

        }


    }

    private void GameOverPanel() 
    {
        gameOverPanel.SetActive(true);
        scoreText.text = "SCORE : " + Score.ToString();
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
