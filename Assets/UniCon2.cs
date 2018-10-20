using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniCon2 : MonoBehaviour {
    // スクロール速度
    private float scrollSpeed = -0.15f;
    // 背景終了位置
    private float deadLine = -16;

    UIController uicon;
    // Use this for initialization
    void Start () {
        uicon = GameObject.Find("Canvas").GetComponent<UIController>();
    }
	
	// Update is called once per frame
	void Update () {
        if (uicon.isGameOver == false)
        {
            transform.Translate(this.scrollSpeed, 0, 0);
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
