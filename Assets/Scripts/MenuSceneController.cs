using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuSceneController : MonoBehaviour
{
    public GameObject aboutPanel;
    public GameObject instructionPanel;
    public GameObject[] instructions;

    private Vector2 pos;
    private float initialTime;
    private int temp = 0;

    private void Start()
    {
        Default();

    }

    public void Default()
    {
        aboutPanel.SetActive(false);
        instructionPanel.SetActive(false);  
    }
    public void About()
    {
        aboutPanel.SetActive(true);
    }
    public void LoadMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void LoadWorldScene()
    {
        SceneManager.LoadScene("MainWorldScene");
    }
    public void FightCovid()
    {
        SceneManager.LoadScene("FightCovid");
    }
    public void Instruction()
    {
        instructionPanel.SetActive(true);
    }

    private void Update()
    {
        if (Input.touches.Length == 1 && instructionPanel.activeSelf)
        {
            Touch t1 = Input.touches[0];

            if (t1.phase == TouchPhase.Began)
            {
                pos = t1.position;
                initialTime = Time.time;
            }
            else if (t1.phase == TouchPhase.Moved)
            {

                var dis = (pos.x - t1.position.x) * 0.1f;
                var velocity = dis / (Time.time - initialTime);
                if(velocity > 300 && dis > 30)
                {
                    temp++;
                    if(temp >= 5)
                    {
                        temp = 0;
                    }
                }
                else if(velocity < -300 && dis < 30)
                {
                    temp--;
                    if(temp <= -1)
                    {
                        temp = 4;
                    }
                }
                
            }


        }

        for(int i = 0; i< instructions.Length; i++)
        {
            if(i == temp)
            {
                instructions[i].SetActive(true);
            }
            else
            {
                instructions[i].SetActive(false);

            }
        }
    }


}
