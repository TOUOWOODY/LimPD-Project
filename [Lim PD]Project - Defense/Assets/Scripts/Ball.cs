using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private float speed = 0f;
    private float scale = 0f;

    public Vector3 target;

    
    public void Throw_ball()
    {
        speed = 0;
        scale = 0f;
        target = Game_Manager.Instance.Target.transform.localPosition;
        StartCoroutine(Moving());
    }

    IEnumerator Moving()
    {

        if(transform.localPosition == target)
        {
            Delete_Ball();
            yield break;
        }

        speed += (Time.deltaTime * 0.1f);
        scale += (Time.deltaTime * 0.0005f);

        transform.localPosition = Vector3.MoveTowards(transform.localPosition, target, 0.2f);
        this.transform.localScale -= new Vector3(scale, scale);

        yield return new WaitForSeconds(0.01f);
        StartCoroutine(Moving());
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Hitter" || collision.name == "Wall")
        {
            Delete_Ball();
        }
    }


    private void Delete_Ball()
    {
        Game_Manager.Instance.object_Pooling.Ball_OP.Enqueue(this.gameObject);
        transform.SetParent(Game_Manager.Instance.object_Pooling.OP_Parents.transform, false);
        this.gameObject.SetActive(false);

        Game_Manager.Instance.Target.GetComponent<Target>().Moving_Target();
        Game_Manager.Instance.Throw_Btn.SetActive(true);
    }
}
