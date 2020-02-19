using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitter : MonoBehaviour
{

    private float direction = 0.1f;
    void Start()
    {
        StartCoroutine(Moving());
    }


    void Update()
    {
        
    }

    IEnumerator Moving()
    {
        transform.Translate(new Vector3(direction, 0, 0));

        if(transform.localPosition.x > 1)
        {
            direction = -0.1f;
        }

        if (transform.localPosition.x < -1)
        {
            direction = 0.1f;
        }

        yield return new WaitForSeconds(0.05f);
        StartCoroutine(Moving());
    }
}
