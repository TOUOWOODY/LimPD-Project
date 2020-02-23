using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{

    public Object_Pooling object_Pooling;


    public GameObject Target;

    [SerializeField]
    private GameObject Ball_Parents;


    [SerializeField]
    private GameObject Start_Panel;

    private static Game_Manager instance = null;    // 싱글톤


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
        object_Pooling.Initialized();
    }


    public void Throw_Btn()
    {
        GameObject ball = object_Pooling.Ball_OP.Dequeue();
        ball.SetActive(true);
        ball.transform.SetParent(Ball_Parents.transform, false);
        ball.transform.localPosition = new Vector2(0, -4);
        ball.GetComponent<Ball>().Throw_ball();
    }
}
