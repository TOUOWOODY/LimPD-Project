using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private float count = 1;
    private Vector3 target;

    void Start()
    {
        StartCoroutine(Target_Move());
    }

    void Update()
    {
        
    }

    IEnumerator Target_Move()
    {
        count -= Time.deltaTime;

        transform.localPosition = Vector3.MoveTowards(transform.localPosition, target, 0.01f);
        if (count < 0 || transform.localPosition == target)
        {
            Setting_Target();
            count = 2;
        }

        yield return new WaitForSeconds(0.01f);
        StartCoroutine(Target_Move());
    }

    private void Setting_Target()
    {
        target = new Vector3(UnityEngine.Random.Range(0.6f, 1.5f), UnityEngine.Random.Range(0.4f, 2.9f), 0);
    }
}
