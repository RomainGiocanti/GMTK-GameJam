using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugScript : MonoBehaviour
{
    private Image m_Healthbar;
    private bool m_Decrease = false;
    private float m_MaxLife = 1000;
    private float m_CurrentLife = 1000;

    // Start is called before the first frame update
    void Start()
    {
        m_Healthbar = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            m_Decrease = true;
        }

        if (m_Decrease)
        {
            m_CurrentLife--;
            if (m_CurrentLife <= 0)
            {
                return;
            }

            m_Healthbar.fillAmount = m_CurrentLife / m_MaxLife;
        }
    }
}
