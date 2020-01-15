using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Ingame : MonoBehaviour
{
    [SerializeField]
    private GameObject Canvas;

    [SerializeField]
    private GameObject flag;

    public GameObject Me;

    public GameObject Shot_Parents;
    public GameObject item_Parents;
    [SerializeField]
    private GameObject Monster_Parents;

    [SerializeField]
    private GameObject Pet;

    [SerializeField]
    private Sprite Tower_Image;

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
            Debug.Log("10킬 !! 보스 등장");
        }
    }
    public void Initialized()
    {
        Manager = Game_Manager.Instance;

        StartCoroutine(Shot_Arrow());
        StartCoroutine(Monster());

        Monster_info = new Dictionary<string, Monster_Information>();
        Monster_Information monsger_infotmation = new Monster_Information();

        for(int i = 0; i < 3; i++)
        {
            monsger_infotmation.initialize(i);
            Monster_info.Add(monsger_infotmation.Name, monsger_infotmation);
        }

    }

    void FixedUpdate()
    {

    }



    public void Click_Tower()
    {
        if (EventSystem.current.currentSelectedGameObject.GetComponent<Image>().sprite != Tower_Image)
        {
            StartCoroutine(EventSystem.current.currentSelectedGameObject.GetComponent<Tower>().Shot_Bomb());
        }

        EventSystem.current.currentSelectedGameObject.GetComponent<Image>().sprite = Tower_Image;
    }


    private void Boss()
    {
        GameObject boss = Manager.object_Pooling.Boss_OP.Dequeue();
        boss.SetActive(true);
        boss.name = "Boss";
        boss.transform.SetParent(Monster_Parents.transform, false);
        boss.transform.localPosition = new Vector3(UnityEngine.Random.Range(-1.9f, 1.9f), 5, 0);

        StartCoroutine(boss.GetComponent<Boss>().Boss_Move());
    }

    IEnumerator Shot_Arrow()
    {
        GameObject arrow = Manager.object_Pooling.Arrow_OP.Dequeue();
        arrow.SetActive(true);
        arrow.name = "Arrow";
        arrow.transform.SetParent(Shot_Parents.transform, false);
        arrow.transform.localPosition = Me.transform.localPosition;

        yield return new WaitForSeconds(arrow_Speed);
        StartCoroutine(Shot_Arrow());
    }

    IEnumerator Monster()
    {
        int random = UnityEngine.Random.Range(0, 2);
        GameObject monster;
        if (random == 0) // archer
        {
            monster = Manager.object_Pooling.Archer_OP.Dequeue();
            monster.name = "Archer";
            monster.SetActive(true);
            monster.transform.SetParent(Monster_Parents.transform, false);
            monster.transform.localPosition = new Vector2(UnityEngine.Random.Range(-1.9f, 1.9f), 5.5f);
            StartCoroutine(monster.GetComponent<Monster>().Archer_Move());
        }
        else // warrior
        {
            monster = Manager.object_Pooling.Warrior_OP.Dequeue();
            monster.name = "Warrior";
            monster.SetActive(true);
            monster.transform.SetParent(Monster_Parents.transform, false);
            monster.transform.localPosition = new Vector2(UnityEngine.Random.Range(-1.9f, 1.9f), 5.5f);
            StartCoroutine(monster.GetComponent<Monster>().Warrior_Move());
        }

        yield return new WaitForSeconds(1f);
        StartCoroutine(Monster());
    }

}
