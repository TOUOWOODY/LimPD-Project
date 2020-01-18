using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Ingame : MonoBehaviour
{
    [SerializeField]
    private GameObject Canvas;


    // 히어로가 등장하는 위치
    [SerializeField]
    private GameObject Hero_Flag_Right;
    [SerializeField]
    private GameObject Hero_Flag_Left;

    //캐릭터
    public GameObject Me;

    //타워 1, 2
    [SerializeField]
    private GameObject Tower0;
    [SerializeField]
    private GameObject Tower1;

    // 프리팹 부모들
    public GameObject Shot_Parents;
    public GameObject item_Parents;
    [SerializeField]
    private GameObject Monster_Parents;


    [SerializeField]
    private Sprite Tower_Image;


    // 텍스트
    [SerializeField]
    private Text Kill_Text;
    [SerializeField]
    private Text Score_Text;


    private Game_Manager Manager;

    private int kill_Count = 0;
    public int Kill_Count
    {
        get
        {
            return kill_Count;
        }
        set
        {
            kill_Count = value;
        }
    }
    private float arrow_Speed = 0.5f;
    //private float zombie_Speed = 0.5f;
   

    public Dictionary<string, Monster_Information> Monster_info = null;
    public float Arrow_Speed
    {
        get
        {
            return arrow_Speed;
        }
    }
      

    public void Kill()
    {
        kill_Count += 1;
        Kill_Text.text = "KILL " + Kill_Count;


        if(Kill_Count == 10)
        {
            Boss();
            Hero();
            Debug.Log("10킬 !! 보스 등장");
        }

        if(Kill_Count == 5)
        {
            Tower0.SetActive(true);
            StartCoroutine(Tower0.GetComponent<Tower>().Shot_Bomb());
        }
        if (Kill_Count == 7)
        {
            Tower1.SetActive(true);
            StartCoroutine(Tower1.GetComponent<Tower>().Shot_Bomb());
        }
    }
    public void Initialized()
    {
        Manager = Game_Manager.Instance;

        Monster_info = new Dictionary<string, Monster_Information>();

        for(int i = 0; i < 3; i++)
        {
            Monster_Information monsger_infotmation = new Monster_Information();

            monsger_infotmation.initialize(i);
            Monster_info.Add(monsger_infotmation.Name, monsger_infotmation);
        }

        Debug.Log(Monster_info["Archer"].Name);
        Debug.Log(Monster_info["Warrior"].Name);
        Debug.Log(Monster_info["Boss"].Name);
        Debug.Log(Monster_info["Warrior"].Power);

        StartCoroutine(Shot_Arrow());
        StartCoroutine(Monster());
    }


    private void Object_Dequeue(GameObject prefab, GameObject Parents, string Name, Vector3 position)
    {
        prefab.SetActive(true);
        prefab.name = Name;
        prefab.transform.SetParent(Parents.transform, false);
        prefab.transform.localPosition = position;
    }


    void FixedUpdate()
    {

    }

    private void Boss()
    {
        GameObject boss = Manager.object_Pooling.Boss_OP.Dequeue();

        Object_Dequeue(boss, Monster_Parents, "Boss", new Vector3(UnityEngine.Random.Range(-1.9f, 1.9f), 5, 0));

        boss.GetComponent<Boss>().Initialize();

        StartCoroutine(boss.GetComponent<Boss>().Boss_Move());
    }

    private void Hero()
    {
        GameObject hero = Manager.object_Pooling.Hero_OP.Dequeue();

        Object_Dequeue(hero, Monster_Parents, "Hero", Hero_Flag_Left.transform.localPosition);

        StartCoroutine(hero.GetComponent<Hero>().Hero_Move());
    }


    IEnumerator Shot_Arrow()
    {
        GameObject arrow = Manager.object_Pooling.Arrow_OP.Dequeue();

        Object_Dequeue(arrow, Shot_Parents, "Arrow", Me.transform.localPosition);

        yield return new WaitForSeconds(arrow_Speed);
        StartCoroutine(Shot_Arrow());
    }

    IEnumerator Monster()
    {
        int random = UnityEngine.Random.Range(0, 2);

        if (random == 0) // archer
        {
            GameObject archer = Manager.object_Pooling.Archer_OP.Dequeue();

            Object_Dequeue(archer, Monster_Parents, "Archer", new Vector2(UnityEngine.Random.Range(-1.9f, 1.9f), 5.5f));

            archer.GetComponent<Monster>().Initialize();
            StartCoroutine(archer.GetComponent<Monster>().Archer_Move());
        }
        else // warrior
        {
            GameObject warrior = Manager.object_Pooling.Warrior_OP.Dequeue();

            Object_Dequeue(warrior, Monster_Parents, "Warrior", new Vector2(UnityEngine.Random.Range(-1.9f, 1.9f), 5.5f));
            warrior.GetComponent<Monster>().Initialize();
            StartCoroutine(warrior.GetComponent<Monster>().Warrior_Move());
        }

        yield return new WaitForSeconds(2f);
        StartCoroutine(Monster());
    }





}
