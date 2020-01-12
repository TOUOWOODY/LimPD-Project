using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{

    private GameObject enemy;

    [SerializeField]
    private GameObject bomb_Parents;
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
            if (collision.name == "Zombie")
            {
                enemy = collision.gameObject;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Zombie")
        {
            enemy = null;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.name != "Zombie")
        {
            enemy = null;
        }
        if (enemy == null)
        {
            if (collision && collision.name == "Zombie")
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
            GameObject bomb = Game_Manager.Instance.object_Pooling.Bomb_OP.Dequeue();
            bomb.transform.SetParent(bomb_Parents.transform, false);
            bomb.GetComponent<Bomb>().Enemy = enemy.transform.localPosition;
            bomb.transform.localPosition = this.transform.localPosition;
            bomb.SetActive(true);
            bomb.name = "Bomb";
        }
        yield return new WaitForSeconds(0.5f);

        StartCoroutine(Shot_Bomb());
    }
}
