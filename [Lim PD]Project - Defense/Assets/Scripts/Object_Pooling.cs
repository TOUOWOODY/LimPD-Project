using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Pooling : MonoBehaviour
{
    [SerializeField]
    private GameObject Arrow;
    [SerializeField]
    private GameObject Tower_Shot;
    [SerializeField]
    private GameObject Monster_Shot;
    [SerializeField]
    private GameObject Boss_Shot;


    //영웅
    [SerializeField]
    private GameObject Hero;


    //몬스터
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
    public Queue<GameObject> Monster_Shot_OP = null;
    public Queue<GameObject> Boss_Shot_OP = null;
    public Queue<GameObject> Tower_Shot_OP = null;

    public Queue<GameObject> Hero_OP = null;


    public Queue<GameObject> Archer_OP = null;
    public Queue<GameObject> Warrior_OP = null;
    public Queue<GameObject> Boss_OP = null;

    public Queue<GameObject> Item_OP = null;
    public void Initialized()
    {
        Arrow_OP = new Queue<GameObject>();
        Tower_Shot_OP = new Queue<GameObject>();
        Monster_Shot_OP = new Queue<GameObject>();
        Boss_Shot_OP = new Queue<GameObject>();

        Hero_OP = new Queue<GameObject>();

        Archer_OP = new Queue<GameObject>();
        Warrior_OP = new Queue<GameObject>();
        Boss_OP = new Queue<GameObject>();

        Item_OP = new Queue<GameObject>();


        // 공격

        for (int i = 0; i < 100; i++)
        {
            OP(Arrow, Arrow_OP);
        }

        for (int i = 0; i < 50; i++)
        {
            OP(Tower_Shot, Tower_Shot_OP);
        }

        for (int i = 0; i < 100; i++)
        {
            OP(Monster_Shot, Monster_Shot_OP);
        }

        for (int i = 0; i < 50; i++)
        {
            OP(Boss_Shot, Boss_Shot_OP);
        }


        // 아이탬
        for (int i = 0; i < 10; i++)
        {
            OP(Item, Item_OP);
        }


        // 유닛
        for (int i = 0; i < 100; i++)
        {
            OP(Archer, Archer_OP);
        }

        for (int i = 0; i < 100; i++)
        {
            OP(Warrior, Warrior_OP);
        }

        for (int i = 0; i < 10; i++)
        {
            OP(Boss, Boss_OP);
        }

        for (int i = 0; i < 10; i++)
        {
            OP(Hero, Hero_OP);
        }
    }



    private void OP(GameObject Object, Queue<GameObject> queue)
    {
        GameObject op = Instantiate(Object, new Vector3(0, 0, 0), Quaternion.identity);
        queue.Enqueue(op);
        op.transform.SetParent(OP_Parents.transform, false);
        op.SetActive(false);
    }
}
