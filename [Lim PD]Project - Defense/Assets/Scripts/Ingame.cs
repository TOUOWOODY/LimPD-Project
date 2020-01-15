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

    [SerializeField]
    private GameObject character;

    public GameObject arrow_Parents;
    public GameObject item_Parents;
    [SerializeField]
    private GameObject zombie_Parents;

    [SerializeField]
    private GameObject Pet;

    [SerializeField]
    private Sprite Tower_Image;

    private float arrow_Speed = 0.5f;
    private float zombie_Speed = 0.5f;
    private bool m_Right = false;
    private bool m_Left = false;

    public bool Right_End = false;
    public bool Left_End = false;

    public Dictionary<string, Monster_Information> Monster_info = null;
    public float Arrow_Speed
    {
        get
        {
            return arrow_Speed;
        }
    }
      


    public void Initialized()
    {
        StartCoroutine(Shot_Arrow());
        StartCoroutine(Zombie());

        Monster_info = new Dictionary<string, Monster_Information>();
        Monster_Information monsger_infotmation = new Monster_Information();
        for(int i = 0; i < 3; i++)
        {
            monsger_infotmation.initialize(i);
            Monster_info.Add(monsger_infotmation.Name, monsger_infotmation);
        }

    }

    private void Pet_Summon()
    {
        GameObject pet = Instantiate(Pet, new Vector2(0, 0), Quaternion.identity);
        StartCoroutine(pet.GetComponent<Boss>().Pet_Attack());
        pet.name = "pet";
        pet.transform.SetParent(Canvas.transform, false);
        pet.transform.localPosition = flag.transform.localPosition;
    }


    void FixedUpdate()
    {
        if(m_Right && !Right_End)
        {
            character.transform.Translate(0.1f,0,0);
        }

        if (m_Left && !Left_End)
        {
            character.transform.Translate(-0.1f, 0, 0);
        }
    }



    public void Click_Tower()
    {
        if (EventSystem.current.currentSelectedGameObject.GetComponent<Image>().sprite != Tower_Image)
        {
            StartCoroutine(EventSystem.current.currentSelectedGameObject.GetComponent<Tower>().Shot_Bomb());
        }

        EventSystem.current.currentSelectedGameObject.GetComponent<Image>().sprite = Tower_Image;
    }












    IEnumerator Shot_Arrow()
    {
        GameObject arrow = Game_Manager.Instance.object_Pooling.Arrow_OP.Dequeue();
        arrow.SetActive(true);
        arrow.name = "Arrow";
        arrow.transform.SetParent(arrow_Parents.transform, false);
        arrow.transform.localPosition = character.transform.localPosition;

        yield return new WaitForSeconds(arrow_Speed);
        StartCoroutine(Shot_Arrow());
    }

    IEnumerator Zombie()
    {
        GameObject zombie = Game_Manager.Instance.object_Pooling.Zombie_OP.Dequeue();
        zombie.SetActive(true);
        zombie.name = "Zombie";
        zombie.transform.SetParent(zombie_Parents.transform, false);
        zombie.transform.localPosition = new Vector2(UnityEngine.Random.Range(-1.9f, 1.9f), 5.5f);

        yield return new WaitForSeconds(zombie_Speed);
        StartCoroutine(Zombie());
    }


    // 캐릭터 움직임

    public void Move_Right()
    {
        m_Left = false;
        m_Right = true;
        Left_End = false;
    }

    public void Move_Left()
    {
        m_Right = false;
        m_Left = true;
        Right_End = false;
    }

    public void Stop_Right()
    {
        m_Right = false;
    }

    public void Stop_Left()
    {
        m_Left = false;
    }
}
