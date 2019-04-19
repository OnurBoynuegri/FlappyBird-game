using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class KusKontrol : MonoBehaviour
{
    public Sprite []KusSprite;
    SpriteRenderer spriteRenderer;
    bool KanatKontrol=true;
    int kusSayac = 0,score=0,rekor=0;
    float KusAnimasyonZaman=0;
    Rigidbody2D fizik;
    public Text ScoreText;
    bool gameover=false;
    OyunKontrol oyunKontrol;
    AudioSource []sesler;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        fizik = GetComponent<Rigidbody2D>();
        oyunKontrol = GameObject.FindGameObjectWithTag("oyunkontrol").GetComponent<OyunKontrol>();
        sesler = GetComponents<AudioSource>();
        rekor = PlayerPrefs.GetInt("RekorKayit");
        Debug.Log(rekor);
    }
  
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !gameover)
        {
            fizik.velocity = new Vector2(0, 0);//yerçekiminden kazanılan hız sıfırlandı;
            fizik.AddForce(new Vector2(0,200));//kuşa yukarı doğru bir hareket kazandıırıldı;
            sesler[0].Play();
        }
        if (fizik.velocity.y > 0)
        {
            transform.eulerAngles=new Vector3(0, 0, 45);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, -45);
        }
        Animasyon();        
    }
    void Animasyon()
    {
        KusAnimasyonZaman += Time.deltaTime;
        if (KusAnimasyonZaman > 0.1f)
        {
            KusAnimasyonZaman = 0;

            if (KanatKontrol)
            {
                spriteRenderer.sprite = KusSprite[kusSayac];
                kusSayac++;
                if (kusSayac == KusSprite.Length)
                {
                    kusSayac--;
                    KanatKontrol = false;
                }
            }
            else
            {
                kusSayac--;
                spriteRenderer.sprite = KusSprite[kusSayac];
                if (kusSayac == 0)
                {
                    kusSayac++;
                    KanatKontrol = true;
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag=="puan")
        {
            score++;
            ScoreText.text = "Score = "+score;
            sesler[1].Play();
        }
        if (collision.gameObject.tag == "engel")
        {
            gameover = true;
            oyunKontrol.GameOver();
            sesler[2].Play();
            GetComponent<CircleCollider2D>().enabled = false;
            if(score > rekor)
            {
                rekor = score;
                PlayerPrefs.SetInt("RekorKayit", rekor);
            }
            Invoke("AnaMenuyeDon", 2);
        }
    }
    void AnaMenuyeDon()
    {
        PlayerPrefs.SetInt("ScoreKayit", score);
        SceneManager.LoadScene("AnaMenu");
    }
}
