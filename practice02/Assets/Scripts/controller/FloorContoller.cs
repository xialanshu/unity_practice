using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FloorContoller : MonoBehaviour
{
    [SerializeField] private GameObject[] floorFabs;

    private Queue<GameObject> floorQueue = new Queue<GameObject>();

    /// <summary>
    /// 固定时间生成阶梯
    /// </summary>
    public void SpawnFloor()
    {

        GameObject floor;
        if (floorQueue.Count > 0)
        {
            floor = floorQueue.Dequeue();
        }
        else
        {
            int index = Random.Range(0, floorFabs.Length);
            // 这里的transform是脚本挂载对象的transform，意味着，生成的物体会放入当前脚本挂载的对象中
            floor = Instantiate(floorFabs[index], transform);
        }
        // 设置初始位置
        floor.transform.position = new Vector3(Random.Range(-5f, 8.5f), -5, 0);
        floor.GetComponent<Floor>().setAdd();
    }

    public void recyleFloor(GameObject floor)
    {
        floorQueue.Enqueue(floor);
    }
}
