using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] Camera cam;
    private float shootRange = 20.0f;
    private float damage = 15.0f;
    // Start is called before the first frame update
    void Start()
    {
        HideMouse();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    // Hide Mouse
    void HideMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Shot Bullet
    void Shoot()
    {
        RaycastHit hitInfo;
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo, shootRange))
        {
            if (hitInfo.transform.name == "Zombie")
            {
                Zombie zombieScript = hitInfo.transform.GetComponent<Zombie>();

                if (zombieScript != null)
                {
                    zombieScript.GetDamaged(damage);
                }
            }
        }
    }
}
