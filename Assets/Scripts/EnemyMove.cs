using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public float startSpeed = 5f;
    public float speed = 0f;
    Rigidbody2D body;
    private sfxManager sfxMan;
    public GameObject ParticleBoom;
    public GameObject Surpice;
    private SpriteRenderer sprite;
    public int countFlashing=12;
    private GameObject target;
    private Surprice surprice;


    // Start is called before the first frame update
    void Start()
    {
        surprice = FindObjectOfType<Surprice>();
        body = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        if (FindObjectOfType<Player>()!=null) target = FindObjectOfType<Player>().gameObject;
        Flashing(0.2f);
        sfxMan = FindObjectOfType<sfxManager>();
    }

    public void Flashing(float delay)
    {
        StartCoroutine(RestartGameCoroutine(delay));
    }

    private IEnumerator RestartGameCoroutine(float delay)
    {
        while (true)
        {
            sprite.enabled = !sprite.enabled;
            countFlashing -= 1;
            if (countFlashing <= 0)
            {
                speed = startSpeed;
                Vector2 directMove;
                if (target!=null)
                    directMove= new Vector2(target.transform.position.x - transform.position.x, target.transform.position.y - transform.position.y).normalized;
                else
                    directMove= new Vector2(1, 0).normalized;
                body.velocity = directMove*speed;
                sprite.enabled = true;
                StopAllCoroutines();
            }
            yield return new WaitForSeconds(delay);
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (speed == 0) return;
        body.velocity = new Vector2(-speed, body.velocity.y);
        if (transform.position.x < -12) Destroy(gameObject);
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

}
