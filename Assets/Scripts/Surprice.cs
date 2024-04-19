using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Surprice : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Surprice1;
    public GameObject Surprice2;
    public GameObject Surprice3;

    public void CreateSurprice(Transform transform)
    {
        float verifySurprice = Random.Range(0f, 100.0f);
        Debug.Log(verifySurprice);
        if (verifySurprice>80)
            Instantiate(Surprice3, transform.position, transform.rotation);
        else if (verifySurprice>20)
            Instantiate(Surprice1, transform.position, transform.rotation);
        else
            Instantiate(Surprice2, transform.position, transform.rotation);
    }

}
