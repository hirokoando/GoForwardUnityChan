﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour {

    // キューブの移動速度
    private float speed = -0.15f;

    // 消滅位置
    private float deadLine = -10;

    //オーディオソース
    private AudioSource BlockSound;

    UIController uicon;

    // Use this for initialization
    void Start () {
        BlockSound = GetComponent<AudioSource>();
        uicon = GameObject.Find("Canvas").GetComponent<UIController>();
    }
	
	// Update is called once per frame
		
	void Update ()
    {
        if(uicon.isGameOver == false)
        {
            // キューブを移動させる
            transform.Translate(this.speed, 0, 0);
        }
        

        // 画面外に出たら破棄する
        if (transform.position.x < this.deadLine)
        {
            Destroy(gameObject);
        }

    }

    void OnCollisionEnter2D (Collision2D other)
    {

        //ブロックと地面に衝突した場合（追加）
        if (other.gameObject.tag == "BlockTag" || other.gameObject.tag == "GroundTag")
        {
           
           BlockSound.PlayOneShot(BlockSound.clip);

        }


    }
}
