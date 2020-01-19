using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public RectTransform monster_y;

    private float hp;
    private float speed;
    private float power;

    [SerializeField]
    private Transform HP_Bar;

    private Game_Manager Manager = null;
    void Start()
    {

    }


    void FixedUpdate()
    {

    }
    public void Initialize()
    {
        Manager = Game_Manager.Instance;

        hp = Manager.ingame.Units_info[this.name].HP;
        speed = Manager.ingame.Units_info[this.name].Speed;
        power = Manager.ingame.Units_info[this.name].Power;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Wall")
        {
            HP_Bar.localScale -= new Vector3(hp, 0, 0);

            Delete_Monster(this.name);
        }

        if (collision.name == "Arrow" || collision.name == "Tower_Shot")
        {
            if(HP_Bar.localScale.x > 0)
            {
                if (collision.name == "Arrow")
                {
                    HP_Bar.localScale -= new Vector3((Manager.ingame.Units_info["Me"].Power / hp), 0, 0);
                }
                else if (collision.name == "Tower_Shot")
                {
                    HP_Bar.localScale -= new Vector3((Manager.ingame.Units_info["Tower"].Power / hp), 0, 0);
                }

                Delete_Monster(this.name);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name == "Heroo")
        {
            if (HP_Bar.localScale.x > 0)
            {
                HP_Bar.localScale -= new Vector3((1f / hp), 0, 0); // 영웅 공격력으로 바꿔줘야댐

                Delete_Monster(this.name);
            }
        }
    }

    private void Delete_Monster(string name)
    {
        if (HP_Bar.localScale.x <= 0)
        {
            HP_Bar.localScale = new Vector3(0, 0, 0);
            Game_Manager.Instance.ingame.Kill();

            if (name == "Archer")
            {
                Game_Manager.Instance.object_Pooling.Archer_OP.Enqueue(this.gameObject);
            }
            else if (name == "Warrior")
            {
                Game_Manager.Instance.object_Pooling.Warrior_OP.Enqueue(this.gameObject);
            }
            this.transform.SetParent(Game_Manager.Instance.object_Pooling.OP_Parents.transform, false);
            this.transform.localPosition = new Vector2(0, 0);
            this.gameObject.SetActive(false);
        }
    }

    IEnumerator Attack()
    {
        if (!this.gameObject.activeSelf)
        {
            yield break;
        }
        GameObject monster_shot = Game_Manager.Instance.object_Pooling.Monster_Shot_OP.Dequeue();
        monster_shot.SetActive(true);
        //monster_shot.GetComponent<Monster_Shot>().Enemy = new Vector3(Game_Manager.Instance.ingame.Me.transform.localPosition.x * 1.25f ,-6f,0);
        monster_shot.GetComponent<Monster_Shot>().Enemy = Game_Manager.Instance.ingame.Me.transform.localPosition;
        monster_shot.name = "Monster_Shot";
        monster_shot.transform.SetParent(Game_Manager.Instance.ingame.Shot_Parents.transform, false);
        monster_shot.transform.localPosition = this.transform.localPosition;

        yield return new WaitForSeconds(1f);
        StartCoroutine(Attack());
    }

    public IEnumerator Archer_Move()
    {
        transform.localPosition -= new Vector3(0, 0.01f, 0);

        if (transform.localPosition.y < (3f + UnityEngine.Random.Range(-0.5f, 0.5f)))
        {
            StartCoroutine(Attack());
            yield break;
        }

        if (!this.gameObject.activeSelf)
        {
            yield break;
        }
        else
        {
            yield return new WaitForSeconds(0.01f);
            StartCoroutine(Archer_Move());
        }
    }

    public IEnumerator Warrior_Move()
    {
        transform.Translate(0, -0.01f, 0);

        if (transform.localPosition.y < (2f + UnityEngine.Random.Range(-0.5f, 0.5f)))
        {
            StartCoroutine(Warrior_Move2(new Vector3(UnityEngine.Random.Range(-0.5f, 0.5f), UnityEngine.Random.Range(1.5f, 1.8f), 0)));
            yield break;
        }

        if (!this.gameObject.activeSelf)
        {
            yield break;
        }
        else
        {
            yield return new WaitForSeconds(0.01f);
            StartCoroutine(Warrior_Move());
        }
    }

    public IEnumerator Warrior_Move2(Vector3 target)
    {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, target, 0.01f);

        if(transform.localPosition == target)
        {
            StartCoroutine(Warrior_Move3());
            yield break;
        }

        if (!this.gameObject.activeSelf)
        {
            yield break;
        }

        yield return new WaitForSeconds(0.01f);
        StartCoroutine(Warrior_Move2(target));
    }

    public IEnumerator Warrior_Move3()
    {
        transform.Translate(0, -0.02f, 0);


        if (!this.gameObject.activeSelf)
        {
            yield break;
        }

        yield return new WaitForSeconds(0.01f);
        StartCoroutine(Warrior_Move3());
    }
}
