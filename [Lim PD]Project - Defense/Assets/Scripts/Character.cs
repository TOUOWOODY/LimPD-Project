using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private bool m_Right = false;
    private bool m_Left = false;

    public bool Right_End = false;
    public bool Left_End = false;


    private float hp = 1000;
    private float speed = 0.05f;
    private float power = 12;

    [SerializeField]
    private Transform HP_Bar;

    void Start()
    {
    }

    public void Initialize()
    {
        hp = Game_Manager.Instance.ingame.Units_info[this.name].HP;
        speed = Game_Manager.Instance.ingame.Units_info[this.name].Speed;
        power = Game_Manager.Instance.ingame.Units_info[this.name].Power;
    }

    void FixedUpdate()
    {
        if (m_Right && !Right_End)
        {
            transform.Translate(speed, 0, 0);
        }

        if (m_Left && !Left_End)
        {
            transform.Translate(-speed, 0, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Right_Wall")
        {
            Right_End = true;
        }

        if (collision.name == "Left_Wall")
        {
            Left_End = true;
        }

        if (collision.name == "Monster_Shot" || collision.name == "Boss_Shot")
        {
            if (HP_Bar.localScale.x > 0)
            {
                HP_Bar.localScale -= new Vector3((20f / hp), 0, 0);
            }
            else
            {
                HP_Bar.localScale = new Vector3(0, 0, 0);
                Game_Manager.Instance.ingame.Finish_Game();
            }
        }
    }

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
