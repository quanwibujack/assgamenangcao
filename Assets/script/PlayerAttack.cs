using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    [Header("Cấu hình tấn công")]
    public Transform throwPoint;
    public GameObject handAxe; // Kéo vật thể 'HandAxe' ở Bước 1 vào đây 🎯
    public float delayTime = 0.2f;
    public float attackCooldown = 0.5f;
    public float throwForce = 15f;

    private Animator anim;
    private bool isAttacking = false;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isAttacking)
        {
            StartCoroutine(AttackRoutine());
        }
    }

    IEnumerator AttackRoutine()
    {
        isAttacking = true;

        if (anim != null)
        {
            anim.SetTrigger("Attack");
        }

        // Chờ tay vung ra
        yield return new WaitForSeconds(delayTime);

        // ẨN CHIẾC RÌU TRÊN TAY ĐI 🙈
        if (handAxe != null) handAxe.SetActive(false);

        // Tạo rìu bay đi từ Pool
        GameObject axe = ObjectPooler.Instance.GetPooledObject();
        if (axe != null)
        {
            axe.transform.position = throwPoint.position;
            axe.transform.rotation = throwPoint.rotation;
            axe.SetActive(true);

            Rigidbody rb = axe.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero;
                rb.AddForce(throwPoint.forward * throwForce, ForceMode.Impulse);
            }
        }

        // Chờ thời gian hồi chiêu
        yield return new WaitForSeconds(attackCooldown - delayTime);

        // HIỆN LẠI CHIẾC RÌU TRÊN TAY 🐵
        if (handAxe != null) handAxe.SetActive(true);

        isAttacking = false;
    }
}