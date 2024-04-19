using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float speed;
    Rigidbody2D rb;
    public float TimeLiveBullet;
    private sfxManager sfxMan;
    public GameObject ParticleBoom;
    private GameMan gameman;



    // Start is called before the first frame update
    void Start()
    {
        gameman = FindObjectOfType<GameMan>();
        rb = GetComponent<Rigidbody2D>();
        Vector2 ForceBullet = new Vector2(speed, 0);
        rb.AddForce(ForceBullet, ForceMode2D.Impulse);
        sfxMan = FindObjectOfType<sfxManager>();
    }

    private void Update()
    {
        TimeLiveBullet = TimeLiveBullet - Time.deltaTime;
        if (TimeLiveBullet < 0) Destroy(gameObject);
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy"||collision.gameObject.tag=="Ballista")
        {
            gameman.intCountEnemy = gameman.intCountEnemy + 1;
            gameman.countEnemy.text = (gameman.intCountEnemy).ToString();
            sfxMan.hit.Play();
            Destroy(gameObject);
            Instantiate(ParticleBoom, transform.position, transform.rotation);
            gameman.GetComponent<Surprice>().CreateSurprice(collision.gameObject.transform);
            Destroy(collision.gameObject,0);
        }
        if (collision.gameObject.tag == "DragonEgg")
        {
            collision.gameObject.GetComponent<DragonEgg>().DestroyEgg();
        }
    }
}
