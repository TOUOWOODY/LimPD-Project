﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Pooling : MonoBehaviour
{
    [SerializeField]
    private GameObject Arrow;

    [SerializeField]
    private GameObject Bomb;

    [SerializeField]
    private GameObject Zombie;

    [SerializeField]
    private GameObject Item;

    public GameObject OP_Parents;

    public Queue<GameObject> Arrow_OP = null;
    public Queue<GameObject> Bomb_OP = null;
    public Queue<GameObject> Zombie_OP = null;
    public Queue<GameObject> Item_OP = null;
    public void Initialized()
    {
        Arrow_OP = new Queue<GameObject>();
        Bomb_OP = new Queue<GameObject>();
        Zombie_OP = new Queue<GameObject>();
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
            GameObject zombie = Instantiate(Zombie, new Vector3(0, 0, 0), Quaternion.identity);
            Zombie_OP.Enqueue(zombie);
            zombie.transform.SetParent(OP_Parents.transform, false);
            zombie.SetActive(false);
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
