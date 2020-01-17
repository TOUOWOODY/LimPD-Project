using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{

    private GameObject enemy;

    [SerializeField]
    private GameObject Shot_Parents;
    void Start()
    {

    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(enemy == null)
        {
            if (collision.name == "Archer" || collision.name == "Warrior")
            {
                enemy = collision.gameObject;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Archer" || collision.name == "Warrior")
        {
            enemy = null;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name == "Archer" || collision.name == "Warrior")
        {
            enemy = null;
        }
        if (enemy == null)
        {
            if (collision && collision.name == "Archer" || collision.name == "Warrior")
            {
                float dis = Vector2.Distance(collision.transform.localPosition, transform.localPosition);


                for (int i = 0; i < 5; i++)
                {
                    if (dis > Vector2.Distance(collision.transform.localPosition, transform.localPosition))
                    {
                        dis = Vector2.Distance(collision.transform.localPosition, transform.localPosition);
                    }
                }

                enemy = collision.gameObject;
            }
            else
            {
                enemy = null;
            }
        }
    }


    public IEnumerator Shot_Bomb()
    {
        if (enemy != null && enemy.activeSelf)
        {
            GameObject shot = Game_Manager.Instance.object_Pooling.Tower_Shot_OP.Dequeue();
            shot.transform.SetParent(Shot_Parents.transform, false);
            shot.GetComponent<Tower_Shot>().Enemy = enemy.transform.localPosition;
            shot.transform.localPosition = this.transform.localPosition;
            shot.SetActive(true);
            shot.name = "Tower_Shot";
        }
        yield return new WaitForSeconds(0.5f);

        StartCoroutine(Shot_Bomb());
    }
}
