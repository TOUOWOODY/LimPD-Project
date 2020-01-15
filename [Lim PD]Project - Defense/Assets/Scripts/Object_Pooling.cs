using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Pooling : MonoBehaviour
{
    [SerializeField]
    private GameObject Arrow;

    [SerializeField]
    private GameObject Bomb;

    [SerializeField]
    private GameObject Archer;

    [SerializeField]
    private GameObject Warrior;

    [SerializeField]
    private GameObject Boss;

    [SerializeField]
    private GameObject Item;

    public GameObject OP_Parents;

    public Queue<GameObject> Arrow_OP = null;
    public Queue<GameObject> Bomb_OP = null;

    public Queue<GameObject> Archer_OP = null;
    public Queue<GameObject> Warrior_OP = null;
    public Queue<GameObject> Boss_OP = null;

    public Queue<GameObject> Item_OP = null;
    public void Initialized()
    {
        Arrow_OP = new Queue<GameObject>();
        Bomb_OP = new Queue<GameObject>();

        Archer_OP = new Queue<GameObject>();
        Warrior_OP = new Queue<GameObject>();
        Boss_OP = new Queue<GameObject>();

        Item_OP = new Queue<GameObject>();

        for (int i = 0; i < 100; i++)
        {
            GameObject arrow = Instantiate(Arrow, new Vector3(0, 0, 0), Quaternion.identity);
            Arrow_OP.Enqueue(arrow);
            arrow.transform.SetParent(OP_Parents.transform, false);
            arrow.SetActive(false);
        }

        for (int i = 0; i < 100; i++)
        {
            GameObject bomb = Instantiate(Bomb, new Vector3(0, 0, 0), Quaternion.identity);
            Bomb_OP.Enqueue(bomb);
            bomb.transform.SetParent(OP_Parents.transform, false);
            bomb.SetActive(false);
        }

        for (int i = 0; i < 100; i++)
        {
            GameObject archer = Instantiate(Archer, new Vector3(0, 0, 0), Quaternion.identity);
            Archer_OP.Enqueue(archer);
            archer.transform.SetParent(OP_Parents.transform, false);
            archer.SetActive(false);
        }

        for (int i = 0; i < 100; i++)
        {
            GameObject warrior = Instantiate(Warrior, new Vector3(0, 0, 0), Quaternion.identity);
            Warrior_OP.Enqueue(warrior);
            warrior.transform.SetParent(OP_Parents.transform, false);
            warrior.SetActive(false);
        }

        for (int i = 0; i < 10; i++)
        {
            GameObject boss = Instantiate(Boss, new Vector3(0, 0, 0), Quaternion.identity);
            Boss_OP.Enqueue(boss);
            boss.transform.SetParent(OP_Parents.transform, false);
            boss.SetActive(false);
        }


        for (int i = 0; i < 20; i++)
        {
            GameObject item = Instantiate(Item, new Vector3(0, 0, 0), Quaternion.identity);
            Item_OP.Enqueue(item);
            item.transform.SetParent(OP_Parents.transform, false);
            item.SetActive(false);
        }
    }
}
