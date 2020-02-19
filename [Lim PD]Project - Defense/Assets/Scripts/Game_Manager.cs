using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{

    //public Object_Pooling object_Pooling;


    public Ball ball;
    private static Game_Manager instance = null;    // 싱글톤


    // UI
    [SerializeField]
    private GameObject Start_Panel;

    public static Game_Manager Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);

            Initialize();
        }
    }
    private void Initialize()
    {

    }


    public void Throw_Btn()
    {
        ball.Throw_ball();
    }
}
