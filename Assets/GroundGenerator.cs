using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundGenerator : MonoBehaviour
{
    
    // 背景開始位置
    private float startLine = 15.0f;
    //地面の選択
    int choise;
    float choise2;

    //ground
    public GameObject ground1Prefab;
    public GameObject ground2Prefab;

    //Block
    public GameObject cubeprefab;

    //coin
    public GameObject coinPrefab;
    public GameObject BigcoinPrefab;

    //Uni
    public GameObject uniPrefab;

    //Ui
    UIController uIController;

    // 時間計測用の変数
    private float delta = 0;
    private float delta2 = 0;
    // 地面生成間隔
    private float span = 1.9f;

    //空中地面生成間隔
    private float airspan = 1.5f;
    //空中地面y軸間隔
    private float CubeSize = 0.3f;
    //ウニ発生率
    private float Uni = 0.1f;

    UIController uicon;
    // Use this for initialization
    void Start()
    {
        uIController = GameObject.Find("Canvas").GetComponent<UIController>();
        

        GameObject road = Instantiate(ground1Prefab) as GameObject;
        road.transform.position = new Vector2(0, -5);
        GameObject roads = Instantiate(ground1Prefab) as GameObject;
        road.transform.position = new Vector2(this.startLine, -5);

        uicon = GameObject.Find("Canvas").GetComponent<UIController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (uicon.isGameOver == false)
        {
            this.delta += Time.deltaTime;

            if (this.delta > this.span)
            {
                this.delta = 0;

                this.choise = Random.Range(1, 10 + 1);


                if (this.choise <= 7)
                {
                    GameObject road = Instantiate(ground1Prefab) as GameObject;
                    road.transform.position = new Vector2(this.startLine, -5);

                    if (this.choise <= 2)
                    {
                        for (int i = 1; i < 6; i++)
                        {
                            GameObject coins = Instantiate(coinPrefab) as GameObject;
                            coins.transform.position = new Vector2(8 + i, -3);
                        }
                    }

                }
                else
                {
                    GameObject road2 = Instantiate(ground2Prefab) as GameObject;
                    road2.transform.position = new Vector2(this.startLine, -5);

                    if (this.choise == 9)
                    {
                        for (int i = -2; i < 3; i++)
                        {
                            GameObject coins = Instantiate(coinPrefab) as GameObject;
                            coins.transform.position = new Vector2(18.0f + i, -0.5f - (i * 0.2f) * (i + 0.2f));
                        }
                    }

                }
                this.span = 1.65f;
            }

            this.delta2 += Time.deltaTime;

            if (this.delta2 > this.airspan)
            {
                this.choise = Random.Range(15, 20 + 1);
                this.choise2 = Random.Range(-2.5f, 3.0f);

                for (int i = 1; i < this.choise + 1; i++)
                {
                    GameObject Cube = Instantiate(cubeprefab) as GameObject;
                    Cube.transform.position = new Vector2(15.0f + i * CubeSize, this.choise2);
                }

                if (this.choise2 >= 2.5f)
                {

                    GameObject Bigcoin = Instantiate(BigcoinPrefab) as GameObject;
                    Bigcoin.transform.position = new Vector2(15.0f + this.choise / 2 * CubeSize, this.choise2 + 1);
                }

                if (this.choise2 < 2.4f && this.choise2 > -1.0f)
                {
                    for (int i = 1; i < 6; i++)
                    {
                        GameObject coins = Instantiate(coinPrefab) as GameObject;
                        coins.transform.position = new Vector2(15.0f + i, this.choise2 + 1);
                    }
                }

                if (CheckRate(uIController.len * this.Uni))
                {
                    float uniy = Random.Range(-4.5f, 4.0f);
                    float unix = Random.Range(-5.0f, 5.0f);

                    GameObject unis = Instantiate(uniPrefab) as GameObject;
                    unis.transform.position = new Vector2(15.0f + unix, uniy);

                }

                this.delta2 = 0;
            }
        }
       

    }


    public static bool CheckRate(float rate)
    {
        if (UnityEngine.Random.Range(0, 100) < rate)
        {
            return true;
        }
        else
        {
            return false;
        }
           
    }

}
