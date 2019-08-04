using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Arrow : MonoBehaviour
{
    public float speed = 1f;
    private int played = 1;
    public Rigidbody2D rb;
    public float slowDown = 5f;
    public float decreaseValue;
    public UnityEvent arrowFeedBack;
    Animator anim;
    GameObject m_SoundManager;
    [SerializeField] private AudioClip m_PickUpSound;
    [SerializeField] private AudioClip m_HitSound;
    [SerializeField] private AudioClip m_BounceSound;
    [SerializeField] private AudioClip m_ShotSound;
    [SerializeField] private AudioClip m_TensionSound;

    void Start()
    {
        rb.velocity = transform.right * speed;
        anim = GetComponent<Animator>();

        m_SoundManager = GameObject.Find("SoundManager");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        speed = 0f;
        rb.velocity = Vector2.zero;
        anim.SetTrigger("Hit");
        arrowFeedBack.Invoke();

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject[] Spikes = GameObject.FindGameObjectsWithTag("Spike");
            foreach (GameObject spike in Spikes)
            {
                spike.GetComponent<SpikeScript>().timer = spike.GetComponent<SpikeScript>().timerDf;
                spike.GetComponent<SpikeScript>().GetComponent<Animator>().SetFloat("Spiker", spike.GetComponent<SpikeScript>().timerDf);
            }
            GameObject.FindGameObjectWithTag("Weapon").GetComponent<ShootingScript>().hasArrow = true;
            GameObject.FindGameObjectWithTag("Weapon").GetComponent<ShootingScript>().buttonHold =
                GameObject.FindGameObjectWithTag("Weapon").GetComponent<ShootingScript>().buttonHoldDefault;

            ///
            //AkSoundEngine.PostEvent("arrow_pickup", gameObject);
            ///

            if (m_SoundManager.GetComponent<SoundManager>())
            {
                m_SoundManager.GetComponent<SoundManager>().PlayOneShot(m_PickUpSound);
            }

            Destroy(gameObject);

        }
    }
    private void Update()
    {
        if (slowDown >= 0)
        {
            slowDown -= Time.deltaTime;
        }
        if (slowDown <= 0)
        {
            if (speed >= 0)
            {
                speed -= decreaseValue;
            }

            if (speed < 0)
            {
                speed = 0;
                transform.GetComponent<Collider2D>().isTrigger = true;
                anim.SetTrigger("Hit");
                if (played == 1)
                {
                    arrowFeedBack.Invoke();
                    played--;
                }
            }
            rb.velocity = transform.right * speed;
        }
    }

    public void PlayHitSound()
    {
        if (m_SoundManager.GetComponent<SoundManager>())
        {
            m_SoundManager.GetComponent<SoundManager>().PlayOneShot(m_HitSound);
        }
    }

    public void PlayBounceSound()
    {
        if (m_SoundManager.GetComponent<SoundManager>())
        {
            m_SoundManager.GetComponent<SoundManager>().PlayOneShot(m_BounceSound);
        }
    }

    public void PlayShotSound()
    {
        if (m_SoundManager.GetComponent<SoundManager>())
        {
            m_SoundManager.GetComponent<SoundManager>().PlayOneShot(m_ShotSound);
        }
    }

}
