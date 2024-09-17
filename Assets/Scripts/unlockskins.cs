using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class unlockskins : MonoBehaviour
{
    public GameObject[] skins;
    int saveSkin;
    public GameObject panelskin, panelunlock;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 1; i <6; i++)
        {
            if(GameManager.instance.getskin(i) == 0)
            {
                skins[i-1].SetActive(true);
                saveSkin = i;
                break;
            }
        }
    }

    public void btn_unlock()
    {
        // sound
        SoundManager.instance.Play("openskin");
        //GameManager.instance.setskin(saveSkin, 1);
        //panelskin.SetActive(true);
        //panelunlock.SetActive(false);

    }

    public void btn_no()
    {
        // sound
        SoundManager.instance.Play("click");
        GameManager.instance.setpourcentage(0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    

    private void CompleteMethod(bool completed, string advertiser)
    {
        Debug.Log("Closed rewarded from: " + advertiser + " -> Completed " + completed);

        if (completed == true)
        {
            GameManager.instance.setskin(saveSkin, 1);
            panelskin.SetActive(true);
            panelunlock.SetActive(false);
        }
        else
        {
            // no reward
        }
    }
}
