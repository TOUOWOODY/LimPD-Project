using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Right_Wall")
        {
            Game_Manager.Instance.ingame.Right_End = true;
        }

        if (collision.name == "Left_Wall")
        {
            Game_Manager.Instance.ingame.Left_End = true;
        }
    }
}
