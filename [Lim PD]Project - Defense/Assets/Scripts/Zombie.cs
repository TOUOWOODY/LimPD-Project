using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    void Start()
    {

    }


    void Update()
    {
        transform.Translate(0, -0.02f, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Wall")
        {
            Game_Manager.Instance.object_Pooling.Zombie_OP.Enqueue(this.gameObject);
            this.transform.SetParent(Game_Manager.Instance.object_Pooling.OP_Parents.transform, false);
            this.gameObject.SetActive(false);
        }

        if (collision.name == "Arrow")
        {
            Game_Manager.Instance.object_Pooling.Zombie_OP.Enqueue(this.gameObject);
            this.transform.SetParent(Game_Manager.Instance.object_Pooling.OP_Parents.transform, false);
            this.gameObject.SetActive(false);
        }
    }
}
