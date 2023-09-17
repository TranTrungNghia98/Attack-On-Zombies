using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHookShoot : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] GameObject hookPrefab;
    private float shootRange = 30.0f;
    private float moveSpeed = 10.0f;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        MoveToTheHook();
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
    }

    void CheckInput()
    {
        // Press Right Mouse To Shoot Hook Wire
        if (Input.GetMouseButtonDown(1))
        {
            ShootHookWire();
        }

        // Press E to Stop Move To The Hook
        else if (Input.GetKeyDown(KeyCode.E))
        {
            StopMovingToTheHook();
        }
    }

    public GameObject CheckHookExist()
    {
        GameObject hook = GameObject.FindGameObjectWithTag("Hook");
        return hook;
    }

    void ShootHookWire()
    {
        GameObject hook = CheckHookExist();

        // Destroy hook if hook exist then spawn new hook.
        if (hook != null)
        {
            Destroy(hook);
        }

        // Spawn new hook
        RaycastHit hitInfo;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo, shootRange))
        {
            // Player only can shoot hook on hook Area
            if (hitInfo.transform.CompareTag("HookArea"))
            {
                Instantiate(hookPrefab, hitInfo.point, Quaternion.identity);
            }
        }
    }

    void MoveToTheHook()
    {
        GameObject hook = CheckHookExist();
        if (hook != null)
        {
            // Prevent player move to fast
            Vector3 moveDirection = (hook.transform.position - transform.position).normalized;
            // Disable Gravity so player can move to the air
            DisableGravity();
            rb.AddForce(moveDirection * moveSpeed);
        }
    }

    void StopMovingToTheHook()
    {
        GameObject hook = CheckHookExist();
        Destroy(hook);
        EnableGravity();
    }

    void DisableGravity()
    {
        rb.useGravity = false;
    }

    void EnableGravity()
    {
        rb.useGravity = true;
    }

}
