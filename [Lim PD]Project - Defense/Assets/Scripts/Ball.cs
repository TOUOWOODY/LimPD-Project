using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private float speed = 0f;


    public void Throw_ball()
    {
        StartCoroutine(Moving());
    }

    IEnumerator Moving()
    {

        if(!this.gameObject.activeSelf)
        {
            yield break;
        }

        speed += (Time.deltaTime * 0.1f);
        transform.Translate(new Vector3(0, speed, 0));


        yield return new WaitForSeconds(0.01f);
        StartCoroutine(Moving());
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Hitter" || collision.name == "Wall")
        {
            this.gameObject.SetActive(false);
        }
    }
}
