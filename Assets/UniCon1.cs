using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniCon1 : MonoBehaviour {

    // スクロール速度
    private float scrollSpeed = -0.15f;
    // 背景終了位置
    private float deadLine = -16;
    //背景上限
    private float maxline =5 ;
    //背景下限
    private float minline=-5;

    bool turn = true;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if(transform.position.y >= this.maxline)
        {
            turn = true;
        }

        if (transform.position.y <= this.minline)
        {
            turn = false;
        }

        if (turn)
        {
            // 斜め下に移動する
            transform.Translate(this.scrollSpeed - 0.05f, this.scrollSpeed -0.1f , 0);
        }
        else
        {
            // 斜め上に移動する
            transform.Translate(this.scrollSpeed -0.05f, -this.scrollSpeed +0.1f, 0);
        }
        

        

        // 画面外に出たら、破棄
        if (transform.position.x < this.deadLine)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PlayerTag")
        {
            
            Destroy(gameObject);
        }

    }

}
