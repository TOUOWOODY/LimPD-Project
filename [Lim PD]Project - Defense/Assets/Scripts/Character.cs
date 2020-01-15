using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private bool m_Right = false;
    private bool m_Left = false;

    public bool Right_End = false;
    public bool Left_End = false;
    void Start()
    {
        
    }


    void FixedUpdate()
    {
        if (m_Right && !Right_End)
        {
            transform.Translate(0.07f, 0, 0);
        }

        if (m_Left && !Left_End)
        {
            transform.Translate(-0.07f, 0, 0);
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
