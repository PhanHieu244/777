using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public List<Enemy> list_enemies;
    public int enemies_killed;


    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        get_all_enemies();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void get_all_enemies()
    {
        list_enemies = new List<Enemy>(FindObjectsOfType<Enemy>());
    }

    public void check_level_complete()
    {
        if (!FindObjectOfType<PlayerController>().game_run)
            return;

        enemies_killed++;

        if(enemies_killed == list_enemies.Count)
        {
            // stop game 
            FindObjectOfType<PlayerController>().game_run = false;
            FindObjectOfType<PlayerController>().joystick.gameObject.SetActive(false);
            FindObjectOfType<PlayerController>().coins_effect.Play();

            // win panel
            FindObjectOfType<ManageUi>().show_win();
            print("yoooou win");
        }
    }

    public void check_lose()
    {
        if (!FindObjectOfType<Enemy>().game_run)
            return;

        FindObjectOfType<PlayerController>().player_stamina--;
        int st = FindObjectOfType<PlayerController>().player_stamina;
        FindObjectOfType<PlayerController>().player_skin.GetComponent<PlayerInfo>().delete_sprite_stamina(st);
        

        if (FindObjectOfType<PlayerController>().player_stamina == 0)
        {
            // lose panel
            FindObjectOfType<ManageUi>().show_lose();
            print("yoooou lose");

            // stop game 
            FindObjectOfType<PlayerController>().game_run = false;
            FindObjectOfType<Enemy>().game_run = false;
            FindObjectOfType<PlayerController>().joystick.gameObject.SetActive(false);
            //animation
            FindObjectOfType<PlayerController>().change_animation_state("die");
            //blood
            FindObjectOfType<PlayerController>().blood.SetActive(true);
        }
    }
}
