using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Racket solRacket, sagRacket;
    Rigidbody2D rb2;
    public float moveSpeed = 10;
    // Start is called before the first frame update
    void Start()
    {
        rb2 = GetComponent<Rigidbody2D>();
        rb2.velocity = new Vector2(1, 0) * moveSpeed;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        moveSpeed += 0.1;
        TagManager tagManager = collision.gameObject.GetComponent<TagManager>();
        GetComponent<AudioSource>().Play();
        if (tagManager == null) return;
        Tag tag = tagManager.myTag;

        if(tag == Tag.SOL_DUVAR)
        {
            //sağ raket skor
            sagRacket.skorYap();
        }
        else if(tag == Tag.SAG_DUVAR)
        {
            //sol skor yap
            solRacket.skorYap();
        }

        if (tag.Equals(Tag.SOL_RAKET))
        {
            DonusYonHesapla(collision, 1);
        }
        else if (tag.Equals(Tag.SAG_RAKET))
        {
            DonusYonHesapla(collision, -1);
        }
    }

    private void DonusYonHesapla(Collision2D collision, float x)
    {
        float a = transform.position.y - collision.gameObject.transform.position.y;
        float b = collision.collider.bounds.size.y;
        float y = a / b;
        rb2.velocity = new Vector2(x, y) * moveSpeed;
    }
}
