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
    private GameObject Ball_Penal;
    [SerializeField]
    private GameObject Start_Penal;

    public GameObject End_Penal;

    public GameObject Throw_Btn;
    [SerializeField]
    private GameObject Start_Panel;

    private int count = 0;

    public List<GameObject> Ball_Count = new List<GameObject>();

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
        UI_Initialized();
    }


    public void Click_Throw()
    {
        if(count == 3)
        {
            count = 0;
            for(int i = 0; i < 3; i ++)
            {
                Ball_Count[i].gameObject.SetActive(true);
            }
        }
        Throw_Btn.SetActive(false);
        Ball_Count[count].SetActive(false);
        count += 1;

        Target.SetActive(false);

        GameObject ball = object_Pooling.Ball_OP.Dequeue();
        ball.SetActive(true);
        ball.transform.SetParent(Ball_Parents.transform, false);
        ball.transform.localPosition = new Vector2(0, -4);
        ball.GetComponent<Ball>().Throw_ball();
    }

    private void UI_Initialized()
    {
        Start_Penal.SetActive(true);
        Throw_Btn.SetActive(false);
        Ball_Penal.SetActive(false);
        End_Penal.SetActive(false);
    }
    

    public void Click_Start_Btn()
    {
        Start_Penal.SetActive(false);
        Throw_Btn.SetActive(true);
        Ball_Penal.SetActive(true);
        End_Penal.SetActive(false);

        for(int i = 0; i < 3; i ++)
        {
            Ball_Count[i].gameObject.SetActive(true);
        }
        count = 0;
    }

    public void End_Game()
    {
        Start_Penal.SetActive(false);
        Throw_Btn.SetActive(false);
        Ball_Penal.SetActive(false);
        End_Penal.SetActive(true);
    }
}
