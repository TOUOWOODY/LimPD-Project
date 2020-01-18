using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Me")
        {
            Game_Manager.Instance.ingame.Arrow_Speed = 0.1f;
            Delete_Item(this.name);
        }
    }

    private void Delete_Item(string shot_name)
    {
        if (shot_name == "Item")
        {
            Game_Manager.Instance.object_Pooling.Item_OP.Enqueue(this.gameObject);
        }
        this.transform.SetParent(Game_Manager.Instance.object_Pooling.OP_Parents.transform, false);
        this.gameObject.SetActive(false);
    }

}
