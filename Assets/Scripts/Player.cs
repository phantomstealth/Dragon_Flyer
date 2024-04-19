using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
	public float speed=6f;
    public float jumpspeed = 0.4f;
    public Transform pointSpawnBullet;
    public GameObject bullet;
    private sfxManager sfxMan;
    public GameObject bloodSprayPrefab;
    public GameObject Flame;
    public float timeFlame=1f;
    private Animator animButton;
    public GameMan gameman;
    private SpriteRenderer sprite;
    public int countFlashingDamage=10;
    private int curFlashingDamage;
    public bool Immortal = false;
    public GameObject gameover;
    public GameObject JoystickCanvas;
    private float flameDeltaTime=0f;
    private bool restartgame = false;


    Rigidbody2D body;

	// Use this for initialization
	void Start () {
        //Time.timeScale = 0;
        flameDeltaTime = Time.deltaTime;
        curFlashingDamage = countFlashingDamage;
        sprite = GetComponent<SpriteRenderer>();
        gameman = FindObjectOfType<GameMan>();
		body = GetComponent <Rigidbody2D>();
        sfxMan = FindObjectOfType<sfxManager>();
        animButton = GameObject.FindGameObjectWithTag("FlameButton").GetComponent<Animator>();
        sfxMan.start.Play();
        StartCoroutine(StartMusicPause(5f));
    }

    void FixedUpdate()
    {
        if (restartgame) return;
        flameDeltaTime += Time.deltaTime;
        if (flameDeltaTime > 1f)
        {
            gameman.sliderPathFinish.value += 0.1f;
            gameman.slider.value += 0.3f;
            flameDeltaTime = 0;
            if (gameman.sliderPathFinish.value == gameman.sliderPathFinish.maxValue)
            {
                if (gameman.DragonEgg < 100)
                {
                    sfxMan.returnforegg.Play();
                    gameman.RestartGame(10f);
                    restartgame = true;
                }
                else
                    SceneManager.LoadScene("Win");
            }
        }
    }
    // Update is called once per frame
    void Update () {
        if (Input.GetButtonDown("Fire3") || Input.GetKeyDown(KeyCode.RightControl)|| Input.GetKeyDown(KeyCode.LeftControl))
            fire();
        if (Input.GetButtonDown("Jump"))
            flame();
        float hspeed = Input.GetAxis("Horizontal");
        float vspeed = Input.GetAxis("Vertical");
        //if (hspeed > 0) hspeed = 1; else if (hspeed < 0) hspeed = -1;
        body.velocity = new Vector2(hspeed*speed, body.velocity.y);
        body.velocity = new Vector2(body.velocity.x, vspeed * speed);
        //if (transform.position.y<10) body.AddForce (vspeed*jumpspeed*Vector2.up, ForceMode2D.Impulse);
	}

    private void flame() 
    {
        if (animButton.GetBool("flame")) return;
        if ((gameman.slider.value < 1)||(gameman.DragonEgg == 0))
        {
            sfxMan.emptyfire.Play();
            return;
        }
        gameman.slider.value -= 2;

        if (gameman.DragonEgg > 0)
            gameman.DragonEgg -= 1;
        else
            return;
        animButton.SetBool("flame",true);
        sfxMan.flame.Play();
        var flameobj = Instantiate(Flame, pointSpawnBullet.position, Flame.transform.rotation);
        flameobj.transform.parent = gameObject.transform;
        //animButton.SetBool("flame", false);
    }

    private void fire()
    {
        if (gameman.slider.value < 1)
        {
            sfxMan.emptyfire.Play();
            return;
        }
        gameman.slider.value -= 1;
        sfxMan.shot.Play();
        Instantiate(bullet, pointSpawnBullet.position,pointSpawnBullet.rotation);
    }

    public void Damage()
    {
        if (!Immortal)
        {
            gameman.LiveDragon -= 1;
            if (gameman.LiveDragon == 0)
                Death();
            else
                Flashing(0.2f);
        }
        var bloodSpray = (GameObject)Instantiate(bloodSprayPrefab, transform.position, Quaternion.identity);
        Destroy(bloodSpray, 3f);
    }

    public void Win()
    { 
    
    }
    public void Death()
    {
        JoystickCanvas.SetActive(false);
        sfxMan.fly.Stop();
        sfxMan.music.Stop();
        sfxMan.gameover.Play();
        gameObject.SetActive(false);
        var bloodSpray = (GameObject)Instantiate(bloodSprayPrefab, transform.position, Quaternion.identity);
        Destroy(bloodSpray, 3f);
        GameMan gameman=FindObjectOfType<GameMan>();
        gameman.RestartGame(10f);
        //gameover.SetActive(true);
        SceneManager.LoadScene("GameOver");
    }

    public void Flashing(float delay)
    {
        Immortal = true;
        StartCoroutine(RestartGameCoroutine(delay));
    }

    private IEnumerator StartMusicPause(float delay)
    {
        yield return new WaitForSeconds(delay);
        sfxMan.music.Play();
    }
    private IEnumerator RestartGameCoroutine(float delay)
    {
        while (true)
        {
            sprite.enabled = !sprite.enabled;
            curFlashingDamage -= 1;
            if (curFlashingDamage <= 0)
            {
                curFlashingDamage = countFlashingDamage;
                sprite.enabled = true;
                Immortal = false;
                StopAllCoroutines();
            }
            yield return new WaitForSeconds(delay);
        }
    }


}
