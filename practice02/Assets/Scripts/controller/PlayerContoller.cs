using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContoller : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    /// <summary>
    /// 记录当前脚下踩的方块
    /// </summary>
    private GameObject currentFloor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow)|| Input.GetKey(KeyCode.D))
        {
            // 方向键 右键
            transform.Translate(moveSpeed * Time.deltaTime, 0, 0);

        }else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            // 方向键 左键
            transform.Translate(-moveSpeed * Time.deltaTime, 0, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 可以通过碰撞物体的标签来判断进一步的处理
        switch (collision.gameObject.tag)
        {
            case "floor_top":
                if(currentFloor != null)
                {
                    BoxCollider2D bc = currentFloor.gameObject.GetComponent<BoxCollider2D>();
                    if (bc != null)
                    {
                        bc.enabled = false;
                    }
                }
                break;
            case "floor_end":
                break;
            default:
                // 取碰撞点的法向量，以获取是垂直的碰撞还是左右的，垂直的，再更新当前脚下的方块
                Vector2 nor = collision.contacts[0].normal;
                if (nor.y > 0)
                {
                currentFloor = collision.gameObject;
                }
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 可以通过碰撞物体的标签来判断进一步的处理
        
    }
}
