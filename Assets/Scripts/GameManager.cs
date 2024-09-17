using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

[DisallowMultipleComponent]
public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    public GameObject[] LevelsContenu;
    public GameObject[] PlayersList;
    public GameObject effectPipe, smokeEffect;
    GameObject player;
    GameObject helmetln;
    Vector3 Origin_player_Position;
    Vector3 origin_player_Scale;

    private void Awake()
    {
        instance = this;
        //if (instance != null)
        //    Destroy(gameObject);
        //else
        //    instance = this;
        //DontDestroyOnLoad(instance);

        //OnInit();

        //Instantiate level
        instantiateLevel();
    }

    private void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
        //helmetln = GameObject.FindGameObjectWithTag("helmetln");
        //Origin_player_Position = player.transform.position;
        //Origin_player_Position.z = 2.2f;
        //origin_player_Scale = player.transform.localScale;
        
        //activetedSkin();

        print(getactivSkin());
        print(origin_player_Scale);
    }
    //private void OnInit()
    //{
    //    instance = this;
    //    //if (instance != null)
    //    //    Destroy(gameObject);
    //    //else
    //    //    instance = this;
    //    //DontDestroyOnLoad(instance);


    //    //LevelToLoad();

    //}
    //private void Start()
    //{
    //    instantiateLevel();
    //}

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            resetall();
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            setLevel(getLevel() + 1);
            if (LevelsContenu.Length <= getLevel())
                return;
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            setcoin(5000);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    
    public void LevelToLoad()
    {
        if (PlayerPrefs.HasKey("Level"))
        {
            SceneManager.LoadScene(PlayerPrefs.GetInt("Level"));
        }
        else
        {
            SceneManager.LoadScene(1);
        }
    }

    public void onstartfirsttime()
    {
        if(!PlayerPrefs.HasKey("firsttime"))
        {
            PlayerPrefs.SetInt("count", 1);
            PlayerPrefs.SetInt("coin", 0);
            PlayerPrefs.SetInt("level", 0);
            PlayerPrefs.SetInt("firsttime", 0);
            PlayerPrefs.SetInt("pourcentage", 0);
            PlayerPrefs.SetInt("activSkin", 0);
            PlayerPrefs.SetInt("skin0", 1);
            for (int i = 1; i < 9; i++)
            {
                PlayerPrefs.SetInt("skin" + i, 0);
            }
        }
    }

    // count Active skin
    public int getcountActive()
    {
        return PlayerPrefs.GetInt("count");
    }
    public void setcountActive(int nbr)
    {
        PlayerPrefs.SetInt("count", nbr);
    }

    // coin
    public int getcoin()
    {
        return PlayerPrefs.GetInt("coin");
    }
    public void setcoin(int nbr)
    {
        PlayerPrefs.SetInt("coin", nbr);
    }

    // menu active
    public int getMenuActive()
    {
        return PlayerPrefs.GetInt("menu");
    }
    public void setMenuActive(int nbr)
    {
        PlayerPrefs.SetInt("menu", nbr);
    }
    //pourcentage get set
    public void setpourcentage(int pourcentage)
    {
        PlayerPrefs.SetInt("pourcentage", pourcentage);
    }

    public int getpourcentage()
    {
        return PlayerPrefs.GetInt("pourcentage");
    }

    //skin variables

    public void setskin(int numSkin , int active)
    {
        PlayerPrefs.SetInt("skin"+ numSkin, active);
    }

    public int getskin(int numSkin)
    {
        return PlayerPrefs.GetInt("skin"+ numSkin);
    }


    // active skin

    public void setactivSkin(int activSkin)
    {
        PlayerPrefs.SetInt("activSkin", activSkin);
    }

    public int getactivSkin()
    {
        return PlayerPrefs.GetInt("activSkin");
    }
    // level number

    public void setLevel(int nbr)
    {
        PlayerPrefs.SetInt("level", nbr);
    }

    public int getLevel()
    {
        return PlayerPrefs.GetInt("level");
    }

    // reset
    public void resetall()
    {
        PlayerPrefs.SetInt("count", 1);
        PlayerPrefs.SetInt("coin", 0);
        PlayerPrefs.DeleteKey("firsttime");
        PlayerPrefs.SetInt("pourcentage", 0);
        PlayerPrefs.SetInt("level", 0);
        PlayerPrefs.SetInt("skin0", 1);
        PlayerPrefs.SetInt("activSkin", 0);
        for (int i = 1; i < 12; i++)
        {
            PlayerPrefs.SetInt("skin" + i, 0);
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //public void activetedSkin()
    //{
    //    Destroy(player);
    //    GameObject go =  Instantiate(PlayersList[getactivSkin()], Origin_player_Position, Quaternion.identity) as GameObject;
    //    go.transform.localScale = origin_player_Scale;
    //    if (helmetln != null)
    //        helmetln.transform.SetParent(go.transform);

    //}

    public void instantiateLevel()
    {
        if (LevelsContenu.Length > getLevel())
            Instantiate(LevelsContenu[getLevel()]);
            //LevelsContenu[getLevel()].SetActive(true);
        print(LevelsContenu.Length);
        print(getLevel());
    }
}


