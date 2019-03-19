using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float rotSpeed = 2f;
    [SerializeField] private Image healthBar;
    private Rigidbody rb;
    private int health = 100;

    private Vector3 targetPos;
    private Vector3 lookAtTarget;
    private Quaternion playerRot;
    private bool isMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            SetTargetPosition();
        }
        if(isMoving)
            MovePlayer();
    }

    private void SetTargetPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, 1000))
        {
            targetPos = hit.point;
            targetPos.y = transform.position.y;
            lookAtTarget = new Vector3(targetPos.x - transform.position.x, transform.position.y, targetPos.z - transform.position.z);
            playerRot = Quaternion.LookRotation(lookAtTarget);
            playerRot.x = 0;
            playerRot.z = 0;
            isMoving = true;
        }
    }

    private void MovePlayer()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, playerRot, rotSpeed * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * moveSpeed);

        if((transform.position - targetPos).magnitude < 2f)
        {
            isMoving = false;
        }
    }

    public void TakeDamage(int dmg)
    {
        health -= dmg;
        healthBar.fillAmount = health / 100f;
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
