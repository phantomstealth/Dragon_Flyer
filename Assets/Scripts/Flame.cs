using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame : MonoBehaviour
{
    private GameMan gameman;
    private sfxManager sfxMan;
    public GameObject ParticleBoom;
    public Player player;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GameObject.FindGameObjectWithTag("FlameButton").GetComponent<Animator>();
        player = FindObjectOfType<Player>();
        gameman = FindObjectOfType<GameMan>();
        sfxMan = FindObjectOfType<sfxManager>();
        DestroyFlame(player.timeFlame);
    }

    // Update is called once per frame
    public void DestroyFlame(float delay)
    {
        StartCoroutine(RestartGameCoroutine(delay));
    }

    private IEnumerator RestartGameCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        anim.SetBool("flame", false);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy"||collision.gameObject.tag == "Ballista")
        {
            gameman.intCountEnemy = gameman.intCountEnemy + 1;
            gameman.countEnemy.text = (gameman.intCountEnemy).ToString();
            sfxMan.hit.Play();
            anim.SetBool("flame", false);
            Destroy(gameObject);
            Instantiate(ParticleBoom, transform.position, transform.rotation);
            gameman.GetComponent<Surprice>().CreateSurprice(collision.gameObject.transform);
            Destroy(collision.gameObject, 0);
        }
        if (collision.gameObject.tag == "DragonEgg")
        {
            collision.gameObject.GetComponent<DragonEgg>().DestroyEgg();
        }
    }


}
