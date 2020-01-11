using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ingame : MonoBehaviour
{
    [SerializeField]
    private GameObject character;

    [SerializeField]
    private GameObject arrow_Parents;

    [SerializeField]
    private GameObject zombie_Parents;

    private float arrow_Speed = 0.01f;

    private bool m_Right = false;
    private bool m_Left = false;

    public bool Right_End = false;
    public bool Left_End = false;
    public float Arrow_Speed
    {
        get
        {
            return arrow_Speed;
        }
    }
    void Start()
    {
    }

    public void Initialized()
    {
        StartCoroutine(Shot_Arrow());
        StartCoroutine(Zombie());
    }
    void Update()
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


    IEnumerator Shot_Arrow()
    {
        GameObject arrow = Game_Manager.Instance.object_Pooling.Arrow_OP.Dequeue();
        arrow.SetActive(true);
        arrow.name = "Arrow";
        arrow.transform.SetParent(arrow_Parents.transform, false);
        arrow.transform.localRotation = Quaternion.Euler(0, 0, -90);
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
        zombie.transform.localPosition = new Vector2(UnityEngine.Random.Range(-500, 500), UnityEngine.Random.Range(1000, 1200));

        yield return new WaitForSeconds(0.2f);
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
