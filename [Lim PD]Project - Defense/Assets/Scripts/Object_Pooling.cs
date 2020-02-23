using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Pooling : MonoBehaviour
{
    public GameObject OP_Parents;



    [SerializeField]
    private GameObject ball;



    public Queue<GameObject> Ball_OP = new Queue<GameObject>();

    void Start()
    {
        
    }



    public void Initialized()
    {
        for(int i = 0; i < 10; i++)
        {
            OP(ball, "ball");
        }
    }


    private void OP(GameObject Game_Object, string name)
    {
        GameObject asd = Instantiate(ball, new Vector3(0, 0, 0), Quaternion.identity);
        Ball_OP.Enqueue(asd);
        asd.name = name;
        asd.transform.SetParent(OP_Parents.transform, false);
        asd.SetActive(false);
    }
    void Update()
    {
        
    }
}
