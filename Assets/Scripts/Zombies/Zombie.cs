using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    private float health = 100.0f;
    private Rigidbody rb;
    private float moveSpeed = 7.0f;
    private GameObject player;
    private float damage = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void FixedUpdate()
    {
        MoveToPlayer();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GetDamaged(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }


    void MoveToPlayer()
    {
        Vector3 playerPosition = player.transform.position;
        Vector3 moveDirection = (playerPosition - transform.position).normalized;
        // Move to player and prevent ignore gravity
        rb.velocity = moveDirection * moveSpeed + transform.up * rb.velocity.y;
    }

    void StopMoving()
    {
        moveSpeed = 0;
        StartCoroutine(WaitAfterAttack());
    }

    IEnumerator WaitAfterAttack()
    {
        yield return new WaitForSeconds(1);
        moveSpeed = 7.0f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StopMoving();
            Rigidbody playerRb = player.GetComponent<Rigidbody>();
            PlayerInfo playerInfoScript = player.GetComponent<PlayerInfo>();

            Vector3 forceDirection = (player.transform.position - transform.position).normalized;
            float force = 3.0f;
            playerRb.AddForce(forceDirection * force, ForceMode.Impulse);

            playerInfoScript.GetDamaged(damage);
        }
    }
}
