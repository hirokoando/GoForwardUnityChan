using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{

    // ゲームオーバテキスト
    private GameObject gameOverText;

    // 走行距離テキスト
    private GameObject runLengthText;

    // 走った距離
    public float len = 0;

    // 走る速度
    private float speed = 0.03f;

    // ゲームオーバの判定
    public bool isGameOver = false;
    
    //coin
    private GameObject cointext;
    private int totalpoint;


    // Use this for initialization
    void Start()
    {
        // シーンビューからオブジェクトの実体を検索する
        this.gameOverText = GameObject.Find("GameOver");
        this.runLengthText = GameObject.Find("RunLength");


        // coinシーンビューからオブジェクトの実体を検索する
        this.cointext = GameObject.Find("CoinText");

        this.totalpoint = 0;
        this.cointext.GetComponent<Text>().text = "Coin: " + totalpoint.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.isGameOver == false)
        {
            // 走った距離を更新する
            this.len += this.speed;

            // 走った距離を表示する
            this.runLengthText.GetComponent<Text>().text = "Distance:  " + len.ToString("F2") + "m";
        }


        this.cointext.GetComponent<Text>().text = " : " + totalpoint.ToString();

        // ゲームオーバになった場合
        if (this.isGameOver == true)
        {
            // クリックされたらシーンをロードする（追加）
            if (Input.GetMouseButtonDown(0))
            {
                //GameSceneを読み込む（追加）
                SceneManager.LoadScene("GameScene2");
            }

        }
    }

    public void GameOver()
    {
        // ゲームオーバになったときに、画面上にゲームオーバを表示する
        this.gameOverText.GetComponent<Text>().text = "GameOver";
        this.isGameOver = true;
    }

    public void CoinSum()
    {
        this.totalpoint += 10;
    }

    public void BigCoinSum()
    {
        this.totalpoint += 200;
    }



}
