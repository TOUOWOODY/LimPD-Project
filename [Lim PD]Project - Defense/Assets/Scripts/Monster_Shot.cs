using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Shot : MonoBehaviour
{
    private Vector3 enemy;
    public Vector3 Enemy
    {
        get
        {
            return enemy;
        }
        set
        {
            enemy = value;
        }
    }
    void FixedUpdate()
    {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, enemy, 0.1f);
        if(transform.localPosition == enemy)
        {
            Delete_Shot();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.name == "Down_Wall" || collision.name == "Right_Wall" || collision.name == "Left_Wall")
        {
            Delete_Shot();
        }

        if (collision.name == "Me")
        {
            Delete_Shot();
        }
    }

    private void Delete_Shot()
    {
        Game_Manager.Instance.object_Pooling.Monster_Shot_OP.Enqueue(this.gameObject);
        this.transform.SetParent(Game_Manager.Instance.object_Pooling.OP_Parents.transform, false);
        this.gameObject.SetActive(false);
    }
}
