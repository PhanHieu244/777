using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class manageskin : MonoBehaviour
{
    public GameObject[] players , activebackground;
    //public GameObject topBanner;
    //public GameObject buyButton;
    public Text coincorner , coinsBuy;
    int saveSkin;
    public List<int> price;

    private void Update()
    {
        coincorner.text = GameManager.instance.getcoin().ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
        coincorner.text = GameManager.instance.getcoin().ToString();
        //coinsBuy.text = GameManager.instance.getcoin().ToString() + " / 500";
        //if (GameManager.instance.getcoin() >= 20)
        //{
        //    buyButton.GetComponent<Button>().interactable = true;
        //}

        //GameManager.instance.setskin(2, 1);

        //check skin active
        for (int i = 1; i < 6; i++)
        {
            players[i].transform.GetChild(3).GetChild(0).GetComponent<Text>().text = price[i-1].ToString();
            if (GameManager.instance.getskin(i)==1)
            {
                //prices[i].SetActive(false);
                //selectes[i].SetActive(true);
                print(players[i].transform.name);
                players[i].transform.GetChild(2).gameObject.SetActive(true);
                players[i].transform.GetChild(3).gameObject.SetActive(false);
            }
            else
            {
                players[i].transform.GetChild(2).gameObject.SetActive(false);
                players[i].transform.GetChild(3).gameObject.SetActive(true);
            }

        }
        activebackground[GameManager.instance.getactivSkin()].SetActive(true);
    }

    public void btn_buy(int player)
    {
        if (GameManager.instance.getcoin() >= price[player -1])
        {

            GameManager.instance.setcountActive(GameManager.instance.getcountActive() + 1);
            // sound
            SoundManager.instance.Play("openskin");
            //for (int i = 1; i < 9; i++)
            //{
            //    if (GameManager.instance.getskin(i) == 0)
            //    {
            //        saveSkin = i;
            //        break;
            //    }
            //}
            GameManager.instance.setskin(player, 1);

            for (int i = 0; i < 6; i++)
            {
                if (GameManager.instance.getskin(i) == 1)
                {
                    //prices[i].SetActive(false);
                    //selectes[i].SetActive(true);
                    players[i].transform.GetChild(2).gameObject.SetActive(true);
                    players[i].transform.GetChild(3).gameObject.SetActive(false);
                }
                else
                {
                    players[i].transform.GetChild(2).gameObject.SetActive(false);
                    players[i].transform.GetChild(3).gameObject.SetActive(true);
                }

            }
            activebackground[GameManager.instance.getactivSkin()].SetActive(true);
            
            GameManager.instance.setcoin(GameManager.instance.getcoin() - price[player -1]);
            coincorner.text = GameManager.instance.getcoin().ToString();
        }
        
        
    }

    public void selectSkin(int i)
    {
        // sound
        SoundManager.instance.Play("click");
        activebackground[GameManager.instance.getactivSkin()].SetActive(false);
        GameManager.instance.setactivSkin(i);
        activebackground[i].SetActive(true);
    }

    public void getback()
    {
        // sound
        SoundManager.instance.Play("click");
        //if (UiManager.instance.skinEnter == false)
        //{
        //    //if (SceneManager.sceneCountInBuildSettings >= SceneManager.GetActiveScene().buildIndex + 1)
        //    //{

        //    //}
        //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //}
        //else
        //{
        //    UiManager.instance.skinEnter = false;
        //    //Debug.Log(this.gameObject.name);
        //    //this.gameObject.SetActive(false);
        //    //topBanner.SetActive(true);
        //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //}
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    

    public void btn_reward_coins()
    {
        
        //GameManager.instance.setcoin(GameManager.instance.getcoin() + 40);
        //coincorner.text = GameManager.instance.getcoin().ToString();
    }

    private void CompleteMethod(bool completed, string advertiser)
    {
        Debug.Log("Closed rewarded from: " + advertiser + " -> Completed " + completed);

        if (completed == true)
        {
            GameManager.instance.setcoin(GameManager.instance.getcoin() + 40);
            coincorner.text = GameManager.instance.getcoin().ToString();
        }
        else
        {
            // no reward
        }
    }
}
