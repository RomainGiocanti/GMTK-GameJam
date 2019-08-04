using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenuScript : MonoBehaviour
{
    public GameObject[] menu;
    public Animator[] anim;
    public bool onSplash, onMain, onControl, onExit;
    bool countIt;

    public float timer = 0.3f;
    int toDestroy;

    
    private void Start()
    {
        menu[0].SetActive(true);
        menu[1].SetActive(false);
        menu[2].SetActive(false);
        menu[3].SetActive(false);
        //[4].SetActive(true);
        onSplash = true;
    }

    private void Update()
    {
       if(onSplash) GoToMainMenu();
        if (countIt)
        {
            if (timer >= 0)
            {
                timer -= Time.deltaTime;
            }
            if (timer <= 0)
            {
                menu[toDestroy].SetActive(false);
                timer = 0.3f;
                countIt = false;
            }
        }
    }


    void GoToMainMenu()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetFalse(0);
            onMain = true;
            menu[1].SetActive(true);
            onSplash = false;
            onMain = true;
        }
    }

    public void SetActive(int index)
    {
        menu[index].SetActive(true);
    }
    public void SetFalse(int index)
    {
        print("Worked");
        anim[index].GetComponent<Animator>().SetTrigger("Outing");
        countIt = true;
        toDestroy = index;
    }
    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void EndApplication()
    {
        Application.Quit();
    }
}
