using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    void Start()
    {
        
    }


    void FixedUpdate()
    {
        transform.Translate(-0.2f, 0 ,0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Zombie")
        {
            Game_Manager.Instance.object_Pooling.Arrow_OP.Enqueue(this.gameObject);
            this.transform.SetParent(Game_Manager.Instance.object_Pooling.OP_Parents.transform, false);
            this.gameObject.SetActive(false);
        }

        if (collision.name == "End_Wall")
        {
            Game_Manager.Instance.object_Pooling.Arrow_OP.Enqueue(this.gameObject);
            this.transform.SetParent(Game_Manager.Instance.object_Pooling.OP_Parents.transform, false);
            this.gameObject.SetActive(false);
        }
    }
}
