using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiveBar : MonoBehaviour
{
    private GameMan gameman;
    private Transform[] hearths = new Transform[5];

    // Start is called before the first frame update
    private void Awake()
    {
        gameman = FindObjectOfType<GameMan>();
        for (int i = 0; i < hearths.Length; i++)
        {
            hearths[i] = transform.GetChild(i);
        }
        Refresh();
    }


    public void Refresh()
    {
        for (int i = 0; i < hearths.Length; i++)
        {
            if (i < gameman.LiveDragon)
            {
                hearths[i].gameObject.SetActive(true);
            }
            else
                hearths[i].gameObject.SetActive(false);
        }
    }
}
