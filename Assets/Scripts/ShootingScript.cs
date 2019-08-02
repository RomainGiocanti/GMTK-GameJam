using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    public Transform firepoint;
    public GameObject arrowPrefab;
    public float offset;
    public bool hasArrow = true;

    void Update()
    {

        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);

            if (Input.GetButtonDown("Fire1"))
            {
            if(hasArrow == true)
                Shoot();
            hasArrow = false;
            }
    }

    void Shoot()
    {
        Instantiate(arrowPrefab, firepoint.position, firepoint.rotation);
    }
}
