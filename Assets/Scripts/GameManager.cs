using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    Vector2 lastVelocity;
    Rigidbody2D rb;//Rigidbody2Dコンポーネント
    public float speed = 10.0f;



    public GameObject ballPrefab;     //ボールプレハブ
    public GameObject ball;           //ボールオブジェクト



    public GameObject buttonRemember; //覚える！ボタン
    public GameObject clearText;

    public GameObject[] Board = new GameObject[18];//反射板左から右
    public GameObject[] Light = new GameObject[12];//ライト
    public GameObject[] CheckButton = new GameObject[12];//ボタン
    public GameObject[] Circle = new GameObject[12];//正解の丸
    public GameObject[] Cross = new GameObject[12];//不正解のバツ

    public int[] BoardKey = new int[4];//反射板の場所を記憶する
    public int CheckKey;              //押した場所の記憶
    public int LightKey;              //ライトの記憶
    public static int ClearCount=0;            //正解数
    public static int StageCount=0;            //何回目か
    public int BoardCount = 2;            //Boardの数を記憶

    public float countdawn = 20.0f;      //カウントダウン
    public bool Pose = false;           //ポーズのため

    // Start is called before the first frame update
    void Start()
    {
        Destroy(ball);
        Setup();
        ClearCount = 0;
        StageCount = 0;
        clearText.SetActive(false);
        Progress1();
    }

    // Update is called once per frame
    void Update()
    {
        if(Pose)                 //スタートしたら
        {
            return;
        }
        countdawn -= Time.deltaTime;
        if (countdawn <= 0)            //カウントダウンが0になったら
        {
            ClearStage();
        }
    }

    public void ClearStage()
    {
        SceneManager.LoadScene("TimeUp");
    }
    public void Progress1()          //進行度１ 覚える！ボタンタップで2に
    {
        buttonRemember.SetActive(true);
        if (StageCount < 3)
        {
            BoardRandom();
        }
        else if(StageCount<6)
        {
            BoardCount = 3;
            BoardRandom2();
        }
        else
        {
            BoardCount = 4;
            BoardRandom3();
        }
    }

    public void Progress2()          //進行度2 checkボタンタップで3に   
    {
        Boardlost();
        LightRandom();
    }

    public void Progress3()          //進行度3 答え合わせ
    {
        Pose = true;
        BoardAgain();
        LightUp();
    }

    public async void Progress4()          //進行度4 次の準備
    {
        await Task.Delay(500);
        Pose = false;
        Setup();
        StageCount++;
        Progress1();
    }

    public void Setup()              //初期化
    {
        Destroy(ball);
        for (int r = 0; r < 18; r++)
        {
            Board[r].SetActive(false);  //反射板を非表示に
        }
        for (int r = 0; r < 12; r++)
        {
            CheckButton[r].SetActive(true);//ボタン表示
            Light[r].SetActive(false);  //ライト非表示
            Cross[r].SetActive(false);  //バツ非表示
            Circle[r].SetActive(false); //丸非表示
        }
    }
    public void LightUp()
    {
        ball = (GameObject)Instantiate(ballPrefab);
        ball.transform.position = Light[LightKey].transform.position;
        rb = ball.GetComponent<Rigidbody2D>();
        switch (LightKey)
        {
            case 0:
            case 1:
            case 2:
                rb.AddForce(transform.up * -1 * speed, ForceMode2D.Impulse);
                break;
            case 3:
            case 5:
            case 7:
                rb.AddForce(transform.right * speed, ForceMode2D.Impulse);
                break;
            case 4:
            case 6:
            case 8:
                rb.AddForce(transform.right * -1 * speed, ForceMode2D.Impulse);
                break;
            case 9:
            case 10:
            case 11:
                rb.AddForce(transform.up * speed, ForceMode2D.Impulse);
                break;
        }

    }

    public void PushButtonRemember() //覚える！ボタンをタップ
    {
        buttonRemember.SetActive(false); //メッセージを消す
        Progress2();
    }

    public void PushButtonCheck1()   //check１ボタンをタップ
    {
        CheckKey = 0;
        Progress3();
    }

    public void PushButtonCheck2()   //check2ボタンをタップ
    {
        CheckKey = 1;
        Progress3();
    }

    public void PushButtonCheck3()   //check3ボタンをタップ
    {
        CheckKey = 2;
        Progress3();
    }

    public void PushButtonCheck4()   //check4ボタンをタップ
    {
        CheckKey = 3;
        Progress3();
    }

    public void PushButtonCheck5()   //check5ボタンをタップ
    {
        CheckKey = 4;
        Progress3();
    }

    public void PushButtonCheck6()   //check6ボタンをタップ
    {
        CheckKey = 5;
        Progress3();
    }

    public void PushButtonCheck7()   //check7ボタンをタップ
    {
        CheckKey = 6;
        Progress3();
    }

    public void PushButtonCheck8()   //check8ボタンをタップ
    {
        CheckKey = 7;
        Progress3();
    }

    public void PushButtonCheck9()   //check9ボタンをタップ
    {
        CheckKey = 8;
        Progress3();
    }

    public void PushButtonCheck10()   //check１0ボタンをタップ
    {
        CheckKey = 9;
        Progress3();
    }

    public void PushButtonCheck11()   //check１1ボタンをタップ
    {
        CheckKey = 10;
        Progress3();
    }

    public void PushButtonCheck12()   //check１2ボタンをタップ
    {
        CheckKey = 11;
        Progress3();
    }

    public void CheckClear(int x)               //答え合わせ
    {
        CheckButton[x].SetActive(false);
        if (CheckKey == x)
        { 
            Circle[x].SetActive(true);
            ClearCount++;                       //正解数カウント
        }
        else
        {
            Cross[x].SetActive(true);
        }
        Progress4();
    }

   

    public void BoardRandom()         //反射板をランダムに表示
    {
        for (int r = 0; r < 2; r++)
        {

            BoardKey[r] = Random.Range(0, 18);
            if (r > 0)
            {
                if (BoardKey[0] < 9)  //反射板がleftなら
                {
                    for(int l = 0; l < 1000; l++)//被らなくなるまでループ
                    {
                        BoardKey[r] = Random.Range(0, 18);
                        if (BoardKey[r] != BoardKey[0] && BoardKey[r] != BoardKey[0] + 9)//９＋することで右側に
                        {
                            break;
                        }
                    }
                }
                else                  //反射板がright
                {
                    for (int l = 0; l < 1000; l++)//被らなくなるまでループ
                    {
                        BoardKey[r] = Random.Range(0, 18);
                        if (BoardKey[r] != BoardKey[0] && BoardKey[r] != BoardKey[0] - 9)//9-することで左側に
                        {
                            break;
                        }
                    }
                }
            }
            int x = BoardKey[r];          //一時的にx
            Board[x].SetActive(true);//反射板を表示
        }
    }

    public void BoardRandom2()         //反射板をランダムに表示
    {
        for (int r = 0; r < 3; r++)
        {

            BoardKey[r] = Random.Range(0, 18);
            if (r == 1)
            {
                if (BoardKey[0] < 9)  //反射板がleftなら
                {
                    for (int l = 0; l < 1000; l++)//被らなくなるまでループ
                    {
                        BoardKey[r] = Random.Range(0, 18);
                        if (BoardKey[r] != BoardKey[0] && BoardKey[r] != BoardKey[0] + 9)//９＋することで右側に
                        {
                            break;
                        }
                    }
                }
                else                  //反射板がright
                {
                    for (int l = 0; l < 1000; l++)//被らなくなるまでループ
                    {
                        BoardKey[r] = Random.Range(0, 18);
                        if (BoardKey[r] != BoardKey[0] && BoardKey[r] != BoardKey[0] - 9)//9-することで左側に
                        {
                            break;
                        }
                    }
                }
            }
            else if (r == 2)
            {
                if (BoardKey[1] < 9)  //反射板がleftなら
                {
                    for (int l = 0; l < 1000; l++)//被らなくなるまでループ
                    {
                        BoardKey[r] = Random.Range(0, 18);
                        if (BoardKey[r] != BoardKey[0] && BoardKey[r] != BoardKey[0] + 9)//９＋することで右側に
                        {
                            if (BoardKey[r] != BoardKey[1] && BoardKey[r] != BoardKey[1] + 9)//９＋することで右側に
                            {
                                break;
                            }
                        }
                    }
                }
                else                  //反射板がright
                {
                    for (int l = 0; l < 1000; l++)//被らなくなるまでループ
                    {
                        BoardKey[r] = Random.Range(0, 18);
                        if (BoardKey[r] != BoardKey[0] && BoardKey[r] != BoardKey[0] - 9)//9-することで左側に
                        {
                            if (BoardKey[r] != BoardKey[1] && BoardKey[r] != BoardKey[1] - 9)//9-することで左側に
                            {
                                break;
                            }
                        }
                    }
                }
            }
            int x = BoardKey[r];          //一時的にx
            Board[x].SetActive(true);//反射板を表示
        }
    }

    public void BoardRandom3()         //反射板をランダムに表示
    {
        for (int r = 0; r < 4; r++)
        {

            BoardKey[r] = Random.Range(0, 18);
            if (r == 1)
            {
                if (BoardKey[0] < 9)  //反射板がleftなら
                {
                    for (int l = 0; l < 1000; l++)//被らなくなるまでループ
                    {
                        BoardKey[r] = Random.Range(0, 18);
                        if (BoardKey[r] != BoardKey[0] && BoardKey[r] != BoardKey[0] + 9)//９＋することで右側に
                        {
                            break;
                        }
                    }
                }
                else                  //反射板がright
                {
                    for (int l = 0; l < 1000; l++)//被らなくなるまでループ
                    {
                        BoardKey[r] = Random.Range(0, 18);
                        if (BoardKey[r] != BoardKey[0] && BoardKey[r] != BoardKey[0] - 9)//9-することで左側に
                        {
                            break;
                        }
                    }
                }
            }
            else if (r == 2)
            {
                if (BoardKey[1] < 9)  //反射板がleftなら
                {
                    for (int l = 0; l < 1000; l++)//被らなくなるまでループ
                    {
                        BoardKey[r] = Random.Range(0, 18);
                        if (BoardKey[r] != BoardKey[0] && BoardKey[r] != BoardKey[0] + 9)//９＋することで右側に
                        {
                            if (BoardKey[r] != BoardKey[1] && BoardKey[r] != BoardKey[1] + 9)//９＋することで右側に
                            {
                                break;
                            }
                        }
                    }
                }
                else                  //反射板がright
                {
                    for (int l = 0; l < 1000; l++)//被らなくなるまでループ
                    {
                        BoardKey[r] = Random.Range(0, 18);
                        if (BoardKey[r] != BoardKey[0] && BoardKey[r] != BoardKey[0] - 9)//9-することで左側に
                        {
                            if (BoardKey[r] != BoardKey[1] && BoardKey[r] != BoardKey[1] - 9)//9-することで左側に
                            {
                                break;
                            }
                        }
                    }
                }
            }
            else if (r == 3)
            {
                if (BoardKey[2] < 9)  //反射板がleftなら
                {
                    for (int l = 0; l < 1000; l++)//被らなくなるまでループ
                    {
                        BoardKey[r] = Random.Range(0, 18);
                        if (BoardKey[r] != BoardKey[0] && BoardKey[r] != BoardKey[0] + 9)//９＋することで右側に
                        {
                            if (BoardKey[r] != BoardKey[1] && BoardKey[r] != BoardKey[1] + 9)//９＋することで右側に
                            {
                                if (BoardKey[r] != BoardKey[2] && BoardKey[r] != BoardKey[2] + 9)//９＋することで右側に
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
                else                  //反射板がright
                {
                    for (int l = 0; l < 1000; l++)//被らなくなるまでループ
                    {
                        BoardKey[r] = Random.Range(0, 18);
                        if (BoardKey[r] != BoardKey[0] && BoardKey[r] != BoardKey[0] - 9)//9-することで左側に
                        {
                            if (BoardKey[r] != BoardKey[1] && BoardKey[r] != BoardKey[1] - 9)//9-することで左側に
                            {
                                if (BoardKey[r] != BoardKey[2] && BoardKey[r] != BoardKey[2] - 9)//9-することで左側に
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            int x = BoardKey[r];          //一時的にx
            Board[x].SetActive(true);//反射板を表示
        }
    }

    public void Boardlost()           //ランダムで選択された反射板を非表示に
    {
        for (int r = 0; r < BoardCount; r++)
        {
            int x = BoardKey[r];

            Board[x].SetActive(false);
        }
   
    }
    public void BoardAgain()
    {
        for (int r = 0; r < BoardCount; r++)
        {
            int x = BoardKey[r];

            Board[x].SetActive(true);
        }
    }

    public void LightRandom()         //ランダムにライトを表示 その場所のボタンを非表示に
    {
        LightKey = Random.Range(0, 12);
        Light[LightKey].SetActive(true);
        CheckButton[LightKey].SetActive(false); //ライトの場所のボタンを非表示に
    }

}
