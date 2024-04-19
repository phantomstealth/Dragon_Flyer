using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBallista : MonoBehaviour
{
    public float TimeLiveBullet=3f;
    sfxManager sfxman;
    // Start is called before the first frame update
    void Start()
    {
        sfxman = FindObjectOfType<sfxManager>();
        sfxman.ballistashot.Play();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        TimeLiveBullet = TimeLiveBullet - Time.deltaTime;
        if (TimeLiveBullet < 0) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            sfxman.dyeDragon.Play();
            collision.gameObject.GetComponent<Player>().Damage();
            //collision.gameObject.SetActive(false);
            Destroy(gameObject);

        }
    }
}
