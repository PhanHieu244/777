using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class rewardsystem : MonoBehaviour
{
    public Image img;
    public Text pourcentText;
    public GameObject lockscinpanel;
    public GameObject nextBTN;
    public Text lvlNumber;
    public Text coincornerTxt;
    
    public GameObject[] coinsAnim;
    bool addpourcent;
    int pourcent;
    // Start is called before the first frame update
    void Start()
    {
        lvlNumber.text = "LEVEL "+ (GameManager.instance.getLevel() +1);
        coincornerTxt.text = GameManager.instance.getcoin().ToString();
        
        pourcentText.text = GameManager.instance.getpourcentage().ToString() + "%";
        float pr = (float)GameManager.instance.getpourcentage() / 100;
        img.fillAmount = pr;
        pourcent = GameManager.instance.getpourcentage();
        StartCoroutine(animfill());
        
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    IEnumerator animfill()
    {
        GameManager.instance.setpourcentage(GameManager.instance.getpourcentage() + 10);
        for (int i = 0; i < 10; i++)
        {
            img.fillAmount += 0.01f;
            pourcent += 1;
            pourcentText.text = pourcent.ToString()+"%";
            yield return new WaitForSeconds(0.1f);
        }

        for (int i = 0; i < coinsAnim.Length; i++)
        {
            coinsAnim[i].SetActive(true);
            GameManager.instance.setcoin(GameManager.instance.getcoin() + 4);
            coincornerTxt.text = GameManager.instance.getcoin().ToString();
            yield return new WaitForSeconds(0.4f);
        }
        nextBTN.SetActive(true);
    }

    public void nextbutton()
    {
        // sound
        SoundManager.instance.Play("click");
        if (GameManager.instance.getpourcentage()<100)
        {
            print("c1");
            GameManager.instance.setLevel(GameManager.instance.getLevel() +1);
                //if (GameManager.instance.LevelsContenu.Length <= GameManager.instance.getLevel())
                //    return;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else if(GameManager.instance.getcountActive() != 12)
        {
            print("c2");
            GameManager.instance.setLevel(GameManager.instance.getLevel() + 1);
            lockscinpanel.SetActive(true);
            GameManager.instance.setpourcentage(0);
        }
        else
        {
            print("c3");
            GameManager.instance.setpourcentage(0);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            //if (SceneManager.sceneCountInBuildSettings >= SceneManager.GetActiveScene().buildIndex + 1)
            //{
            //    GameManager.instance.setLevel(GameManager.instance.getLevel() +1);
            //    if (GameManager.instance.LevelsContenu.Length <= GameManager.instance.getLevel())
            //        return;
            //}
        }
    }
    
    public void fct(int b)
    {
        Debug.Log(b);
    }

    IEnumerator Reward_percent()
    {
        nextBTN.SetActive(false);
        if (GameManager.instance.getpourcentage() < 100)
        {
            GameManager.instance.setpourcentage(GameManager.instance.getpourcentage() + 20);
            for (int i = 0; i < 20; i++)
            {
                img.fillAmount += 0.01f;
                pourcent += 1;
                pourcentText.text = pourcent.ToString() + "%";
                yield return new WaitForSeconds(0.07f);
            }
            nextBTN.SetActive(true);
        }
        
    }

    public void BTN_Reward_percent()
    {
        
    }

    private void CompleteMethod(bool completed, string advertiser)
    {
        Debug.Log("Closed rewarded from: " + advertiser + " -> Completed " + completed);

        if (completed == true)
        {
            StartCoroutine(Reward_percent());
        }
        else
        {
            // no reward
        }
    }


}
