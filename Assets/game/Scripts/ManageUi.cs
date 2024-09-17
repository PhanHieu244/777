using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ManageUi : MonoBehaviour
{
    public GameObject winpanel;
    public GameObject losepanel;
    public GameObject ingame;
    public GameObject swipe;
    public GameObject skins_panel;
    public Text score_text;
    public Text total_score;
    public Text coincornerTxt_ingame;
    public GameObject joystick;

    public bool isactive;
    // Start is called before the first frame update
    void Start()
    {
        coincornerTxt_ingame.text = GameManager.instance.getcoin().ToString();

    }

    private void Update()
    {
        
    }

    public void BTN_Skins()
    {
        skins_panel.SetActive(true);
        swipe.SetActive(false);
    }

    public void BTN_Restart()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BTN_Restart_lose()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BTN_Next()
    {
        GameManager.instance.setLevel(GameManager.instance.getLevel() + 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BTN_hide_tap_to_play()
    {
        hide_swipe_panel();
    }


    public void show_win()
    {
        StartCoroutine(show_win_panel());
    }
     IEnumerator show_win_panel()
    {
        yield return new WaitForSeconds(2f);
        winpanel.SetActive(true);
        ingame.SetActive(false);

    }

    public void show_lose()
    {
        StartCoroutine(show_lose_panel());
    }
    IEnumerator show_lose_panel()
    {
        yield return new WaitForSeconds(2f);
        losepanel.SetActive(true);
        ingame.SetActive(false);

    }

    public void hide_swipe_panel()
    {
        joystick.SetActive(true);
        FindObjectOfType<PlayerController>().game_run = true;
        FindObjectOfType<Enemy>().game_run = true;

        ingame.SetActive(true);
        swipe.SetActive(false);
        isactive = true;
    }

    private void CompleteMethod(bool completed, string advertiser)
    {
        Debug.Log("Closed rewarded from: " + advertiser + " -> Completed " + completed);

        if (completed == true)
        {
            // next level
            GameManager.instance.setLevel(GameManager.instance.getLevel() + 1);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            // no reward
        }
    }

    public void btn_skip()
    {
        // sound
        SoundManager.instance.Play("openskin");
        
    }

}
