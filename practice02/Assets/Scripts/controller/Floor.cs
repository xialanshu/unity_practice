using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{

    private float moveSpeed = 5f;
    private bool addStage = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (addStage)
        {
            transform.Translate(0, Time.deltaTime * moveSpeed, 0);
            if (transform.position.y > 6f)
            {
                recyle();
                FloorContoller com = transform.parent.GetComponent<FloorContoller>();
                com.SpawnFloor();
                com.recyleFloor(gameObject);
            }
        }
    }

    public void setAdd()
    {
        addStage = true;
        gameObject.SetActive(true);
    }

    /// <summary>
    /// 回收方法，重置自己的位置
    /// </summary>
    public void recyle()
    {
        gameObject.SetActive(false);
        addStage = false;
        transform.position = new Vector3(0, -5, 0);
        BoxCollider2D bc = gameObject.GetComponent<BoxCollider2D>();
        if (bc != null)
        {
            bc.enabled = true;
        }
    }
}
