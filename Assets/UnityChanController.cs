using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnityChanController : MonoBehaviour {

    //アニメーションするためのコンポーネントを入れる
    Animator animator;
 

    //Unityちゃんを移動させるコンポーネントを入れる（追加）
    Rigidbody2D rigid2D;

    // ジャンプの速度の減衰（追加）
    private float dump = 0.8f;

    // ジャンプの速度（追加）
    float jumpVelocity = 15;

    // ゲームオーバになる位置（追加）
    private float deadLine = -8;
    private float lostLine = -6;

    //twojump
    private bool jumping;

    //接地
    bool isGround;
    public LayerMask groundLayer;	// 着地できるレイヤー 

    //Hp
    Slider hpBar;
    int HP = 100;
    private float times;
    private float span = 2.0f;


    [SerializeField]
    private AudioClip damageVoice;

    [SerializeField]
    private AudioClip[] jumpVoices;

    [SerializeField]
    private AudioClip gameOverVoice;

    private AudioSource audioSource1;
    private AudioSource audioSource2;


    // Use this for initialization
    void Start () {
        // アニメータのコンポーネントを取得する
        this.animator = GetComponent<Animator>();
        // Rigidbody2Dのコンポーネントを取得する（追加）
        this.rigid2D = GetComponent<Rigidbody2D>();
        // スライダーを取得する
        hpBar = GameObject.Find("HpBar").GetComponent<Slider>();

       AudioSource[] audioSources = GetComponents<AudioSource>();
        audioSource1 = audioSources[0];
        audioSource2 = audioSources[1];

    }
	
	// Update is called once per frame
	void Update () {
        //HP
        hpBar.value = HP;

        // 走るアニメーションを再生するために、Animatorのパラメータを調節する
        this.animator.SetFloat("Horizontal", 1);

        // 着地しているかどうかを調べる

        // 地面との衝突を検知する。 
        RaycastHit2D hit = Physics2D.Linecast(
            transform.position,           // 始点 
                transform.position - transform.up * 1.2f, // 終点 
            groundLayer);

        if (hit.collider)
        {
            isGround = true;
        }
        else
        {
            isGround = false;
        }

        if (isGround == false)
        {
            audioSource2.Pause();

        }
        else
        {
            audioSource2.UnPause();
        }

        this.animator.SetBool("isGround", isGround);

       
        // 着地状態でクリックされた場合（追加）
        if (Input.GetMouseButtonDown(0) && isGround )
        {
            PlayVoice(jumpVoices[0]);
            // 上方向の力をかける（追加）
            this.rigid2D.velocity = new Vector2(0, this.jumpVelocity);
            this.jumping = true;
        }

        // jump状態でクリックされた場合（追加）
        if (Input.GetMouseButtonDown(0) && isGround == false && jumping ==true)
        {
            PlayVoice(jumpVoices[1]);
            // 上方向の力をかける（追加）
            this.rigid2D.velocity = new Vector2(0, this.jumpVelocity);
            this.jumping = false;
        }

        // クリックをやめたら上方向への速度を減速する（追加）
        if (Input.GetMouseButton(0) == false)
        {
            if (this.rigid2D.velocity.y > 0)
            {
                this.rigid2D.velocity *= this.dump;
            }
        }

        

        // デッドラインを超えた場合ゲームオーバにする（追加）
        if (transform.position.x < this.deadLine || transform.position.y < this.lostLine || HP == 0)
        {
            PlayVoice(gameOverVoice);
            this.animator.SetFloat("Horizontal", 0);
            this.animator.SetBool("isGround", true);

            if (transform.rotation.z < 30.0f)
            {
                transform.Rotate(0, 0, 1.5f);
            }
            else
            {
                transform.position.Set(transform.position.x, -4.6f,0);
            }
           

            // UIControllerのGameOver関数を呼び出して画面上に「GameOver」と表示する（追加）
            GameObject.Find("Canvas").GetComponent<UIController>().GameOver();

            this.times += Time.deltaTime;

            if (times >= span)
            {
                // ユニティちゃんを破棄する（追加）
                Destroy(gameObject);
                times = 0;

            }
           
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "CoinTag")
        {
            GameObject.Find("Canvas").GetComponent<UIController>().CoinSum();

        }

        if (other.tag == "BigCoinTag")
        {
            GameObject.Find("Canvas").GetComponent<UIController>().BigCoinSum();
        }

        if (other.tag == "EnemyTag")
        {
            PlayVoice(damageVoice);
            HP -= 10;
            if (isGround)
            {
                // Animatorコンポーネントを取得し、Trigger"をtrueにする
                GetComponent<Animator>().SetTrigger("DamageTrigger");
            }
            else
            {
                // Animatorコンポーネントを取得し、Trigger"をtrueにする
                GetComponent<Animator>().SetTrigger("AirDamageTrigger");
            }
            

        }
    }

    void PlayVoice(AudioClip voice)
    {
        audioSource1.Stop();
        audioSource1.PlayOneShot(voice);
    }
}
