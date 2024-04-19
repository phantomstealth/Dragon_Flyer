using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallistaMove : MonoBehaviour
{
    public float speed = 2f;
    Rigidbody2D body;
    private sfxManager sfxMan;
    public Transform startArrow;
    public GameObject prefabArrow;
    public bool ArrowHave = true;
    private Surprice surprice;



    // Start is called before the first frame update
    void Start()
    {
        surprice = FindObjectOfType<Surprice>();
        body = GetComponent<Rigidbody2D>();
        sfxMan = FindObjectOfType<sfxManager>();
    }

    // Update is called once per frame
    void Update()
    {
        body.velocity = new Vector2(-speed, body.velocity.y);
        if (transform.position.x < -12) Destroy(gameObject);
        if (body.velocity.y >= -0.1 & ArrowHave) FireArrow();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            sfxMan.dyeDragon.Play();
            collision.gameObject.GetComponent<Player>().Damage();
            //collision.gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "BorderBack")
        {
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        }

    }

    public void FireArrow()
    {
        GameObject arrow=Instantiate(prefabArrow, startArrow.position, transform.rotation);
        arrow.GetComponent<Rigidbody2D>().AddForce(new Vector2(-12,8),ForceMode2D.Impulse);
        ArrowHave = false;
    }
}
