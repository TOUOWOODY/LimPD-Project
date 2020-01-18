﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private bool right = false;
    private bool left = false;

    [SerializeField]
    private Transform HP_Bar;

    private float hp;
    private float speed;
    private float power;


    void FixedUpdate()
    {

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Arrow")
        {
            if (HP_Bar.localScale.x > 0)
            {
                HP_Bar.localScale -= new Vector3((30f / hp), 0, 0);

                if (HP_Bar.localScale.x <= 0)
                {
                    HP_Bar.localScale = new Vector3(0, 0, 0);
                    Game_Manager.Instance.ingame.Kill();
                    Death_Boss();
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name == "Heroo")
        {
            if (HP_Bar.localScale.x > 0)
            {
                HP_Bar.localScale -= new Vector3((1f / hp), 0, 0);

                if (HP_Bar.localScale.x <= 0)
                {
                    HP_Bar.localScale = new Vector3(0, 0, 0);
                    Game_Manager.Instance.ingame.Kill();
                    Death_Boss();

                }
            }
        }


    }

    private void Death_Boss()
    {
        Game_Manager.Instance.object_Pooling.Boss_OP.Enqueue(this.gameObject);
        this.transform.SetParent(Game_Manager.Instance.object_Pooling.OP_Parents.transform, false);
        this.transform.localPosition = new Vector2(0, 0);
        this.gameObject.SetActive(false);
    }

    public void Initialize()
    {
        hp = Game_Manager.Instance.ingame.Units_info[this.name].HP;
        speed = Game_Manager.Instance.ingame.Units_info[this.name].Speed;
        power = Game_Manager.Instance.ingame.Units_info[this.name].Power;
    }

    public IEnumerator Boss_Attack()
    {

        GameObject shot = Game_Manager.Instance.object_Pooling.Boss_Shot_OP.Dequeue();
        shot.SetActive(true);
        shot.GetComponent<Monster_Shot>().Enemy = Game_Manager.Instance.ingame.Me.transform.localPosition;
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

        if(!this.gameObject.activeSelf)
        {
            yield break;
        }

        yield return new WaitForSeconds(0.01f);
        StartCoroutine(Boss_Move());
    }

    private IEnumerator Boss_Move2()
    {
        if (left)
        {
            transform.Translate(-speed, 0, 0);
            if (transform.localPosition.x <= -2f)
            {
                left = false;
                right = true;
            }
        }
        if (right)
        {
            transform.Translate(speed, 0, 0);
            if (transform.localPosition.x >= 2f)
            {
                left = true;
                right = false;
            }
        }

        if (!this.gameObject.activeSelf)
        {
            yield break;
        }


        yield return new WaitForSeconds(0.01f);
        StartCoroutine(Boss_Move2());
    }
}
