using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenuScript : MonoBehaviour
{
    public GameObject[] menu;
    public Animator[] anim;
    public bool onSplash, onMain;
    bool countIt;
    bool countMenu;
    bool sceneCount;
    public bool inGame;

    public float timer = 0.3f;
    int toDestroy;


    private void Start()
    {
        if (!inGame)
        {
            menu[0].SetActive(true);
            menu[1].SetActive(false);
            menu[2].SetActive(false);
            menu[3].SetActive(false);
        }
        else
        {
            print("HI");
            menu[0].SetActive(false);
            menu[1].SetActive(false);
            menu[2].SetActive(false);
            menu[3].SetActive(false);
        }

        onSplash = true;
    }

    private void Update()
    {
        print(Time.timeScale);
        if (!inGame)
        {
            if (onSplash) GoToMainMenu();
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
        else
        {
            if (sceneCount)
            {
                if (timer >= 0)
                {
                    timer -= Time.deltaTime;
                }
                if (timer <= 0)
                {
                    timer = 2f;
                    sceneCount = false;
                    SceneManager.LoadScene(toDestroy);
                }
            }
            InGame();

            if (countIt)
            {
                if (timer >= 0)
                {
                    timer -= Time.deltaTime;
                }
                if (timer <= 0)
                {
                    timer = 0.3f;
                    countIt = false;
                    Time.timeScale = 0;
                }
            }
            if (countMenu)
            {
                if (timer >= 0)
                {
                    timer -= Time.deltaTime;
                }
                if (timer <= 0)
                {
                    menu[0].SetActive(false);
                    countMenu = false;
                    Time.timeScale = 1;
                    timer = 0.3f;
                }
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
        Time.timeScale = 1;
        toDestroy = index;
        anim[index].GetComponent<Animator>().SetTrigger("Outing");
        countIt = true;
    }
    public void LoadScene(int index)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(index);
    }
    public void resolveIt()
    {
        timer = 0.5f;
    }

    public void Resume(int index)
    {
        Time.timeScale = 1;
        timer = 0.8f;
        anim[index].GetComponent<Animator>().SetTrigger("Outing");
        countMenu = true;
        countIt = false;
    }
    public void InGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            timer = 0.8f;
            menu[0].SetActive(true);
            countIt = true;
        }
    }

    public void SetFalse1(int index)
    {
        menu[index].SetActive(false);
        countIt = false;
        menu[0].GetComponent<Image>().color = Color.white;
        Time.timeScale = 1;
    }

    public void EndApplication()
    {
        Application.Quit();
    }
}
