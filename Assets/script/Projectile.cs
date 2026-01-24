using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 15f;          // Tốc độ bay của rìu
    public float lifeTime = 2f;        // Sau 2 giây sẽ tự biến mất (để tái sử dụng)
    private float timer;

    void OnEnable()
    {
        // Mỗi khi rìu được "lấy ra" từ Pool, ta reset lại thời gian
        timer = lifeTime;
    }

    void Update()
    {
        // Rìu bay về phía trước theo hướng của nó
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        // Đếm ngược thời gian tồn tại
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            // Thay vì Destroy, ta chỉ ẩn nó đi để Pool quản lý 
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Nếu chạm vào kẻ địch (Mechas) hoặc môi trường, rìu cũng biến mất
        if (other.CompareTag("Enemy") || other.CompareTag("Environment"))
        {
            gameObject.SetActive(false);
        }
    }
}