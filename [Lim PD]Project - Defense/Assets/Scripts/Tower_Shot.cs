using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower_Shot : MonoBehaviour
{

    public Vector3 Enemy;

    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, Enemy, 0.2f);
        if (this.transform.localPosition == Enemy)
        {
            Delete_Shot();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Archer" || collision.name == "Warrior")
        {
            Delete_Shot();
        }
    }

    private void Delete_Shot()
    {
        Game_Manager.Instance.object_Pooling.Tower_Shot_OP.Enqueue(this.gameObject);
        this.transform.SetParent(Game_Manager.Instance.object_Pooling.OP_Parents.transform, false);
        this.gameObject.SetActive(false);
    }
}
