using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Princess : MonoBehaviour
{
    private float Count;
    private int Direction;
    private bool Jump = false;
    public void Initialize()
    {
        Count = UnityEngine.Random.Range(0.5f, 1.9f);
        Direction = UnityEngine.Random.Range(0, 3);

        StartCoroutine(Princess_Move());
    }



    private IEnumerator Princess_Move()
    {
        Count -= Time.deltaTime;
        if(Direction == 0 && transform.localPosition.x < 2.2)
        {
            transform.Translate(0.007f, 0, 0);
        }
        else if(Direction == 1 && transform.localPosition.x > 1.8)
        {
            transform.Translate(-0.007f, 0, 0);
        }
        else
        {

        }

        if(Count < 0)
        {
            Count = UnityEngine.Random.Range(0.5f, 1.9f);
            Direction = UnityEngine.Random.Range(0, 2);
            Jump = true;
            StartCoroutine(Princess_Jump());
        }
        yield return new WaitForSeconds(0.05f);
        StartCoroutine(Princess_Move());
    }

    private IEnumerator Princess_Jump()
    {
        if(transform.localPosition.y <= -4.17f && Jump)
        {
            transform.Translate(0, 0.03f, 0);
        }

        if(transform.localPosition.y > -4.17f)
        {
            Jump = false;
        }

        if(!Jump)
        {
            transform.Translate(0, -0.04f, 0);
            if(transform.localPosition.y <= -4.3f)
            {
                yield break;
            }
        }

        yield return new WaitForSeconds(0.05f);
        StartCoroutine(Princess_Jump());
    }
}
