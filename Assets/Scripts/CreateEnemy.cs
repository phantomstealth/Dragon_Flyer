using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEnemy : MonoBehaviour
{
    public float positionX=8.22f;
    public Object Enemy;
    public GameObject Ballista;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 newPos;
        float verifyAddEnemy = Random.Range(0f, 100.0f);
        if (verifyAddEnemy < 0.2)
        {
            newPos = new Vector3(8, Random.Range(-3f, 4.79f), 0);
            Instantiate(Ballista, newPos, Quaternion.identity);
        }
        else if (verifyAddEnemy < 1)
        {
            newPos = new Vector3(positionX, Random.Range(-3f, 4.79f), 0);
            Instantiate(Enemy, newPos, Quaternion.identity); 
        }
    }
}
