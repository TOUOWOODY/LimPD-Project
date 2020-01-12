using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pet : MonoBehaviour
{
    private bool right = false;
    private bool left = true;

    void Start()
    {
    }

    void FixedUpdate()
    {
        if(left)
        {
            transform.Translate(0.1f, 0, 0);
        }

        if(right)
        {
            transform.Translate(-0.1f, 0, 0);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Right_Wall")
        {
            right = true;
            left = false;
        }

        if (collision.name == "Left_Wall")
        {
            right = false;
            left = true;
        }
    }



    public IEnumerator Pet_Attack()
    {
        GameObject arrow = Game_Manager.Instance.object_Pooling.Arrow_OP.Dequeue();
        arrow.SetActive(true);
        arrow.name = "Arrow";
        arrow.transform.SetParent(Game_Manager.Instance.ingame.arrow_Parents.transform, false);
        arrow.transform.localRotation = Quaternion.Euler(0, 0, -90);
        arrow.transform.localPosition = this.transform.localPosition;

        yield return new WaitForSeconds(UnityEngine.Random.Range(0.5f, 1));
        StartCoroutine(Pet_Attack());
    }
}
