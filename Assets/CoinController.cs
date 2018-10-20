using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CoinController : MonoBehaviour
{
    public AudioClip getCoin;
    private GameObject cointext;
    private int totalpoint;

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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PlayerTag")
        {
            AudioSourceController.instance.PlayOneShot(getCoin);
            Destroy(gameObject);
        }
      
    }

    void Update()
    {
        if (uicon.isGameOver == false)
        {
            // 地面を移動する
            transform.Translate(this.scrollSpeed, 0, 0);
        }
           

        // 画面外に出たら、破棄
        if (transform.position.x < this.deadLine)
        {
            Destroy(gameObject);
        }

    }
}
