using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ClearManager : MonoBehaviour
{

    public GameObject Score;
    public GameObject Clear;
    public GameObject Bat;
    public float score;
    public float clear;
    public float stage;

    public Image UIobj;
    public bool roop;
    public float countTime = 2.0f;


    
    // Start is called before the first frame update
    void Start()
    {
        Display();
    }

    // Update is called once per frame
    void Update()
    {
        if (roop)
        {
            UIobj.fillAmount += 1.0f / countTime * Time.deltaTime;
            if (UIobj.fillAmount > score/100)
            {
                roop = false;
            }
        }
    }

    public void Display()
    {
        roop = true;
        clear = GameManager.ClearCount;
        stage = GameManager.StageCount;
        score = (clear / stage) * 100;
        Text Score_text = Score.GetComponent<Text>();
        Score_text.text = "Score:" + (int)score+"%";
        Text Clear_Text = Clear.GetComponent<Text>();
        Clear_Text.text = "正解数" + "\n" + clear;
        Text Bat_text = Bat.GetComponent<Text>();
        Bat_text.text = "不正解数" + "\n" + (stage - clear);

    }

    public void pushRetryButton()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
