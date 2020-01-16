using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    private bool enemy = false;

    
    public IEnumerator Hero_Move()
    {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(0, -1.3f, 0), 0.03f);
        
        if(transform.localPosition == new Vector3(0, -1.3f, 0))
        {
            StartCoroutine(Hero_Move2());
            yield break;
        }
        yield return new WaitForSeconds(0.01f);
        StartCoroutine(Hero_Move());
    }

    IEnumerator Hero_Move2()
    {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(0, 3f, 0), 0.02f);

        if(enemy)
        {
            yield break;
        }
        yield return new WaitForSeconds(0.01f);
        StartCoroutine(Hero_Move2());
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Archer" || collision.name == "Warrior")
        {
            enemy = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //if (collision.name == "Archer" || collision.name == "Warrior")
        //{
        //    enemy = true;
        //}
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision && enemy)
        {
            if (collision.name == "Archer" || collision.name == "Warrior")
            {
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, collision.gameObject.transform.localPosition, 0.05f);
            }
            else
            {
                //transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(0, 3f, 0), 0.02f);
            }
        }
        else
        {
            //if(enemy)
            //{
            //    StartCoroutine(Hero_Move2());
            //    enemy = false;
            //}
        }
    }
}
