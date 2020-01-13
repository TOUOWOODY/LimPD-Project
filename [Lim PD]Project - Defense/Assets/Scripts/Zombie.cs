using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    void Start()
    {

    }


    void FixedUpdate()
    {
        transform.Translate(0, -0.02f, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Wall")
        {
            Game_Manager.Instance.object_Pooling.Zombie_OP.Enqueue(this.gameObject);
            this.transform.SetParent(Game_Manager.Instance.object_Pooling.OP_Parents.transform, false);
            this.transform.localPosition = new Vector2(0, 0);
            this.gameObject.SetActive(false);
        }

        if (collision.name == "Bomb")
        {
            Drop_Item();

            Game_Manager.Instance.object_Pooling.Zombie_OP.Enqueue(this.gameObject);
            this.transform.SetParent(Game_Manager.Instance.object_Pooling.OP_Parents.transform, false);
            this.transform.localPosition = new Vector2(0, 0);
            this.gameObject.SetActive(false);
        }

        if (collision.name == "Arrow")
        {
            Drop_Item();

            Game_Manager.Instance.object_Pooling.Zombie_OP.Enqueue(this.gameObject);
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
}
