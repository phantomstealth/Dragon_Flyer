using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonEgg : MonoBehaviour
{
    public float punchUp = 2f;
    private GameMan gameman;
    private sfxManager sfxMan;
    public GameObject ParticleBoom;
    private GameObject progressfire;
    // Start is called before the first frame update
    void Start()
    {
        progressfire = GameObject.FindGameObjectWithTag("ProgressFire");
        sfxMan = FindObjectOfType<sfxManager>();
        gameman = FindObjectOfType<GameMan>();
        GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, punchUp);
    }

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            sfxMan.takeEgg.Play();
            if (gameObject.tag == "DragonEgg")
                gameman.DragonEgg += 5;
            else if (gameObject.tag == "GreenEgg")
                gameman.LiveDragon += 1;
            else if (gameObject.tag == "DiamondYellow")
                gameman.slider.value= gameman.slider.maxValue;

            //gameman.countDragonEgg.text = gameman.DragonEgg.ToString();
            Destroy(gameObject);
        }
        else
        {
            DestroyEgg();
        }
    }

    public void DestroyEgg()
    {            
        sfxMan.damageEgg.Play();
        Instantiate(ParticleBoom, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
