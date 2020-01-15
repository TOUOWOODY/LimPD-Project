using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public RectTransform monster_y;

    void Start()
    {

    }


    void FixedUpdate()
    {
        //transform.Translate(new Vector3(0, -0.01f, 0));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Wall")
        {
            Game_Manager.Instance.object_Pooling.Archer_OP.Enqueue(this.gameObject);
            this.transform.SetParent(Game_Manager.Instance.object_Pooling.OP_Parents.transform, false);
            this.transform.localPosition = new Vector2(0, 0);
            this.gameObject.SetActive(false);
        }

        if (collision.name == "Bomb")
        {
            Drop_Item();

            Game_Manager.Instance.object_Pooling.Archer_OP.Enqueue(this.gameObject);
            this.transform.SetParent(Game_Manager.Instance.object_Pooling.OP_Parents.transform, false);
            this.transform.localPosition = new Vector2(0, 0);
            this.gameObject.SetActive(false);
        }

        if (collision.name == "Arrow")
        {
            Game_Manager.Instance.object_Pooling.Archer_OP.Enqueue(this.gameObject);
            this.transform.SetParent(Game_Manager.Instance.object_Pooling.OP_Parents.transform, false);
            this.transform.localPosition = new Vector2(0, 0);
            this.gameObject.SetActive(false);
        }
    }

    private void Drop_Item()
    {
        int random = UnityEngine.Random.Range(0, 10);

        if(random == 0)
        {
            GameObject item = Game_Manager.Instance.object_Pooling.Item_OP.Dequeue();
            item.SetActive(true);
            item.name = "Item";
            item.transform.SetParent(Game_Manager.Instance.ingame.item_Parents.transform, false);
            item.transform.localPosition = this.transform.localPosition;
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
        monster_shot.GetComponent<Monster_Shot>().Enemy = Game_Manager.Instance.ingame.Me.transform.localPosition - new Vector3((transform.localPosition.x + Game_Manager.Instance.ingame.Me.transform.localPosition.x),3,0);
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

        yield return new WaitForSeconds(0.01f);
        StartCoroutine(Archer_Move());
    }

    public IEnumerator Warrior_Move()
    {
        transform.localPosition -= new Vector3(0, 0.01f, 0);

        if (transform.localPosition.y < (2f + UnityEngine.Random.Range(-0.5f, 0.5f)))
        {
            yield break;
        }

        if (!this.gameObject.activeSelf)
        {
            yield break;
        }

        yield return new WaitForSeconds(0.01f);
        StartCoroutine(Warrior_Move());
    }
}
