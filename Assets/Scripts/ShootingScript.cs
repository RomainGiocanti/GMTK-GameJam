using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    public Transform firepoint;
    public GameObject arrowPrefab;
    public float offset;
    public bool hasArrow = true;
    public float buttonHold = 1f;
    private float buttonHoldDefault;

    private void Start()
    {
        buttonHoldDefault = buttonHold;
    }
    void Update()
    {

        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);

        if (Input.GetButton("Fire1"))
        {
            if (buttonHold > 0)
            {
                buttonHold -= Time.deltaTime;
            }

            if (hasArrow == true && buttonHold <= 0)
            {
                Shoot();
                hasArrow = false;
            }
            if (hasArrow)
            {
                Time.timeScale = 0;
            }
        }
        if (Input.GetButtonUp("Fire1"))
        {
            if (hasArrow == true)
                Shoot();
            hasArrow = false;

        }

        void Shoot()
        {
            Instantiate(arrowPrefab, firepoint.position, firepoint.rotation);
            buttonHold = buttonHoldDefault;
            Time.timeScale = 1;
        }
    }
}
