using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance; // Singleton để dễ dàng truy cập từ script khác

    public List<GameObject> pooledObjects; // Danh sách chứa các vật thể (rìu)
    public GameObject objectToPool;       // Prefab của chiếc rìu
    public int amountToPool;             // Số lượng rìu muốn tạo sẵn (ví dụ: 10)

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < amountToPool; i++)
        {
            // Tạo sẵn các chiếc rìu nhưng ở trạng thái ẩn (false)
            GameObject obj = Instantiate(objectToPool);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }

    // Hàm để lấy một chiếc rìu đang rảnh (đang ẩn) ra để sử dụng
    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null; // Trả về null nếu tất cả rìu đều đang được sử dụng
    }
}