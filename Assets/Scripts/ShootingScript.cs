using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootingScript : MonoBehaviour
{
    public GameObject shootingCanvas;
    public Image timerBar;
    public Transform firepoint;
    public GameObject arrowPrefab;
    public float offset;
    public bool hasArrow = true;
    public float buttonHold = 1f;
    float maximumHoldTime;
    [HideInInspector] public float buttonHoldDefault;

    private void Start()
    {
        buttonHoldDefault = buttonHold;
        maximumHoldTime = buttonHoldDefault;
        shootingCanvas.SetActive(false);
    }
    void Update()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);

        if (Input.GetButton("Fire1"))
        {
            if (buttonHold >= 0)
            {
                shootingCanvas.SetActive(true);
                buttonHold -= Time.deltaTime;
                timerBar.fillAmount = buttonHold / maximumHoldTime;
                if (buttonHold <= maximumHoldTime / 2) timerBar.color = Color.red;
            }

            if (hasArrow == true && buttonHold <= 0)
            {
                shootingCanvas.SetActive(false);
                Shoot();
                hasArrow = false;
            }

        }
        if (Input.GetButtonUp("Fire1"))
        {

            if (hasArrow == true)
            {
                buttonHold = buttonHoldDefault;
                Shoot();
                hasArrow = false;
                shootingCanvas.SetActive(false);
            }


        }
        

        void Shoot()
        {
            Instantiate(arrowPrefab, firepoint.position, firepoint.rotation);
            timerBar.color = Color.yellow;
        }
    }
}
