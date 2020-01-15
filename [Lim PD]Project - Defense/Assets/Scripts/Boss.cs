using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private bool right = false;
    private bool left = false;


    void FixedUpdate()
    {

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

    }



    public IEnumerator Boss_Attack()
    {

        GameObject shot = Game_Manager.Instance.object_Pooling.Boss_Shot_OP.Dequeue();
        shot.SetActive(true);
        shot.GetComponent<Monster_Shot>().Enemy = new Vector3(Game_Manager.Instance.ingame.Me.transform.localPosition.x * 1.3f, -6f, 0);
        shot.name = "Boss_Shot";
        shot.transform.SetParent(Game_Manager.Instance.ingame.Shot_Parents.transform, false);
        shot.transform.localPosition = this.transform.localPosition;

        yield return new WaitForSeconds(UnityEngine.Random.Range(0.1f, 0.5f));
        StartCoroutine(Boss_Attack());
    }

    public IEnumerator Boss_Move()
    {
        transform.Translate(0, -0.01f, 0);

        if (transform.localPosition.y < 3.5f)
        {
            if(UnityEngine.Random.Range(0,2) == 0)
            {
                right = true;
                left = false;
            }
            else
            {
                right = false;
                left = true;
            }
            StartCoroutine(Boss_Move2());
            StartCoroutine(Boss_Attack());
            yield break;
        }

        yield return new WaitForSeconds(0.01f);
        StartCoroutine(Boss_Move());
    }

    private IEnumerator Boss_Move2()
    {
        if (left)
        {
            transform.Translate(-0.01f, 0, 0);
            if (transform.localPosition.x <= -2f)
            {
                left = false;
                right = true;
            }
        }
        if (right)
        {
            transform.Translate(0.01f, 0, 0);
            if (transform.localPosition.x >= 2f)
            {
                left = true;
                right = false;
            }
        }


        yield return new WaitForSeconds(0.01f);
        StartCoroutine(Boss_Move2());
    }
}
