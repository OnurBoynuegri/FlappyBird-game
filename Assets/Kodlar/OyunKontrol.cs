using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OyunKontrol : MonoBehaviour
{
    public GameObject GokyuzuBir, GokyuzuIki;
    Rigidbody2D fizik1, fizik2;
    float uzunluk = 0;
    public float GokyuzuHiz=-1.5f;
    public GameObject engel;
    public int engelSayisi=5;
    GameObject[] Engeller;
    float engelZaman = 0;
    int engelSayac=0;
    bool gameover=false;
    void Start()
    {
        fizik1 = GokyuzuBir.GetComponent<Rigidbody2D>();
        fizik2 = GokyuzuIki.GetComponent<Rigidbody2D>();
        fizik1.velocity = new Vector2(GokyuzuHiz, 0);
        fizik2.velocity = new Vector2(GokyuzuHiz, 0);
        uzunluk = GokyuzuBir.GetComponent<BoxCollider2D>().size.x;
        Engeller = new GameObject[engelSayisi];
        for (int i=0 ; i<Engeller.Length;i++)
        {
            Engeller[i] = Instantiate(engel, new Vector2(-20, -20),Quaternion.identity);// Alt ve üst nesneyi -20,-20 koordinatında oluşturan kod
           Rigidbody2D fizikEngel= Engeller[i].AddComponent<Rigidbody2D>();//ustNesne adlı prefaba rigitbody ekledik
            fizikEngel.gravityScale = 0;
            fizikEngel.velocity = new Vector2(GokyuzuHiz, 0);
        }
    }
    
    void Update()
    {
        if (!gameover)
        {
            if (GokyuzuBir.transform.position.x <= -uzunluk)
            {
                GokyuzuBir.transform.position += new Vector3(uzunluk * 2, 0);
            }
            if (GokyuzuIki.transform.position.x <= -uzunluk)
            {
                GokyuzuIki.transform.position += new Vector3(uzunluk * 2, 0);
            }
            engelZaman += Time.deltaTime;
            if (engelZaman > 2f)
            {
                engelZaman = 0;
                float Yeksenim = Random.Range(-0.29f, 1.33f);
                Engeller[engelSayac].transform.position = new Vector3(19.03f, Yeksenim);
                engelSayac++;
                if (engelSayac >= Engeller.Length)
                {
                    engelSayac = 0;
                }
            }
        }
    }
        
    public void GameOver()
    {
        for(int i = 0; i < Engeller.Length; i++)
        {
            Engeller[i].GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            fizik1.velocity = Vector2.zero;
            fizik2.velocity = Vector2.zero;
            gameover = true;
        }
    }
}
