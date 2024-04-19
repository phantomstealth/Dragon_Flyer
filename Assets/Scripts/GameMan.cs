using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GameMan : MonoBehaviour
{
    // Start is called before the first frame update
    public Text countEnemy;
    public int intCountEnemy=0;
    public Text countDragonEgg;
    private int intDragonEgg = 0;
    private Animator animFlame;
    private int intLiveDragon=3;
    private LiveBar livebar;
    public Slider slider;
    public Slider sliderPathFinish;
    public GameObject frmMenu;
    public int LiveDragon
    {
        get {return intLiveDragon;}
        set
        {
            if (value < 0) value = 0;
            if (value > 5) value = 5;
            intLiveDragon = value;
            livebar.Refresh();
        }
    }
    public int DragonEgg
    {
        get { return intDragonEgg; }
        set 
        {
            if (value > 0)
            {
                intDragonEgg = value;
                animFlame.SetBool("enabled", true);
            }
            else
            {
                intDragonEgg = 0;
                animFlame.SetBool("enabled", false);
            }
            countDragonEgg.text = intDragonEgg.ToString();
        }
    }

    private void Update()
    {
        if ((Input.backButtonLeavesApp) || (Input.GetKeyDown(KeyCode.Escape)))
        {
            frmMenu.SetActive(true);
            Time.timeScale = 0;
            Debug.Log("back");
        }

    }

    void Start()
    {
        animFlame = GameObject.FindGameObjectWithTag("FlameButton").GetComponent<Animator>();
        livebar = FindObjectOfType<LiveBar>();
    }


    public void RestartGame(float delay)
    {
        StartCoroutine(RestartGameCoroutine(delay));
    }

    private IEnumerator RestartGameCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
