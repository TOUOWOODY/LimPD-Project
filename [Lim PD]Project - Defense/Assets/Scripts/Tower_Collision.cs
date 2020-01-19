using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower_Collision : MonoBehaviour
{
    private float power = 12;
    private float hp = 100;

    [SerializeField]
    private Transform HP_Bar;

    [SerializeField]
    private GameObject Tower;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Monster_Shot" || collision.name == "Boss_Shot")
        {
            if (HP_Bar.localScale.x > 0)
            {

                if (collision.name == "Monster_Shot")
                {
                    HP_Bar.localScale -= new Vector3((Game_Manager.Instance.ingame.Units_info["Archer"].Power / hp), 0, 0);
                }
                else if(collision.name == "Boss_Shot")
                {
                    HP_Bar.localScale -= new Vector3((Game_Manager.Instance.ingame.Units_info["Boss"].Power / hp), 0, 0);
                }


                if (HP_Bar.localScale.x <= 0)
                {
                    HP_Bar.localScale = new Vector3(0, 0, 0);
                    Tower.SetActive(false);
                }
            }
        }
    }
}
