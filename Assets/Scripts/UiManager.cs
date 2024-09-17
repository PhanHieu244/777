using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;
    public Text LevelText;
    public Text coin_text;
    public bool skinEnter;
    public GameObject ingamepanel;
    public GameObject playerSelectionPanel;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {

        LevelText.text =(GameManager.instance.getcoin()).ToString();
        LevelText.text = "Level " + (GameManager.instance.getLevel() + 1);
    }

    

    public void skinmenu()
    {
        // sound
        SoundManager.instance.Play("click");
        skinEnter = true;
        playerSelectionPanel.SetActive(true);
        ingamepanel.SetActive(false);
    }

    public void btn_retry()
    {

        // sound
        SoundManager.instance.Play("click");

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
