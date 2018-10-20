using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour
{

    // スクロール速度
    private float scrollSpeed = -0.15f;
    // 背景終了位置
    private float deadLine = -16;
    UIController uicon;
    // Use this for initialization
    void Start()
    {
        uicon = GameObject.Find("Canvas").GetComponent<UIController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (uicon.isGameOver == false)
        {
            // 地面を移動する
            transform.Translate(this.scrollSpeed, 0, 0);

        }

        // 画面外に出たら、画面右端に移動する
        if (transform.position.x < this.deadLine)
        {
            Destroy(gameObject);
        }
    }
}
