using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    private bool enemy = false;

    private float hp;
    private float speed;
    private float power;

    [SerializeField]
    private Transform HP_Bar;

    public void Initialize()
    {
        hp = Game_Manager.Instance.ingame.Units_info[this.name].HP;
        speed = Game_Manager.Instance.ingame.Units_info[this.name].Speed;
        power = Game_Manager.Instance.ingame.Units_info[this.name].Power;
    }

    public IEnumerator Hero_Move()
    {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(0, -1.3f, 0), speed);
        
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
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(0, 3f, 0), speed);

        if(enemy)
        {
            yield break;
        }
        yield return new WaitForSeconds(0.01f);
        StartCoroutine(Hero_Move2());
    }

    IEnumerator Move_Enemy(GameObject target_onject)
    {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, target_onject.transform.localPosition, speed * 2);

        HP_Bar.localScale -= new Vector3((0.1f / hp), 0, 0);

        Death_Hero();

        if (!target_onject.activeSelf)
        {
            enemy = false;
            StartCoroutine(Hero_Move2());
            yield break;
        }

        if(!this.gameObject.activeSelf)
        {
            yield break;
        }
        yield return new WaitForSeconds(0.01f);
        StartCoroutine(Move_Enemy(target_onject));
    }


    private void Death_Hero()
    {
        if (HP_Bar.localScale.x <= 0)
        {
            HP_Bar.localScale = new Vector3(0, 0, 0);

            Game_Manager.Instance.object_Pooling.Hero_OP.Enqueue(this.gameObject);

            this.transform.SetParent(Game_Manager.Instance.object_Pooling.OP_Parents.transform, false);
            this.transform.localPosition = new Vector2(0, 0);
            this.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Archer" || collision.name == "Warrior")
        {
            if(!enemy)
            {
                StartCoroutine(Move_Enemy(collision.gameObject));
                enemy = true;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision && !enemy)
        {
            if (collision.name == "Archer" || collision.name == "Warrior" || collision.name == "Boss")
            {
                enemy = true;
                StartCoroutine(Move_Enemy(collision.gameObject));
            }
        }
    }
}
