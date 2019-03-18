using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private Image healthBar;
    private Rigidbody rb;
    private float health = 100;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveHori = Input.GetAxis("Horizontal") * moveSpeed;
        float moveVert = Input.GetAxis("Vertical") * moveSpeed;

        Vector3 movement = new Vector3(moveHori,0, moveVert);

        rb.AddForce(movement);
    }

    public void TakeDamage(float dmg)
    {
        health -= dmg;
        healthBar.fillAmount = health / 100f;
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
