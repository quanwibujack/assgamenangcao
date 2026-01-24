using UnityEngine;
using UnityEngine.AI;

public class MonsterAI : MonoBehaviour
{
    [Header("Cấu hình AI")]
    public Transform player;
    public float attackRange = 5.0f;
    public float fireRate = 1.0f;

    [Header("Cấu hình Vũ khí")]
    public GameObject bulletPrefab; // Kéo viên đạn mẫu vào đây
    public Transform firePoint;    // Kéo FirePoint vào đây
    public float bulletSpeed = 20f;

    private NavMeshAgent agent;
    private Animator anim;
    private float nextFireTime = 0f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        // Tự động tìm nhân vật Barbarian nếu chưa gán
        if (player == null)
            player = GameObject.Find("Barbarian").transform;
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= attackRange)
        {
            AttackPlayer();
        }
        else
        {
            ChasePlayer();
        }
    }

    void ChasePlayer()
    {
        agent.isStopped = false;
        agent.SetDestination(player.position);
        anim.SetFloat("Speed", agent.velocity.magnitude);
    }

    void AttackPlayer()
    {
        agent.isStopped = true;
        anim.SetFloat("Speed", 0);

        // Xoay robot hướng về phía người chơi
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

        // Kiểm tra thời gian hồi chiêu để bắn
        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        anim.SetTrigger("Attack"); // Kích hoạt hoạt ảnh bắn

        // Sinh ra viên đạn tại vị trí đầu nòng súng
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Đẩy viên đạn bay đi bằng vật lý
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = firePoint.forward * bulletSpeed;
        }

        // Xóa viên đạn sau 2 giây để tránh rác bộ nhớ
        Destroy(bullet, 2f);
    }
}