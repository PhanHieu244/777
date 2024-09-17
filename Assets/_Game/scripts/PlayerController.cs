using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    CharacterController controller;
    public DynamicJoystick joystick;
    public float speed;
    Vector3 move;
    float vertical_gravity_y = 0f , gravity = 15f;
    public bool can_move = true , is_press , game_run;
    string current_state_anim = "idle";
    const string Idle_anim = "idle" , Run_anim = "Run" ,
        Attack_anim = "attack" , Die_anim = "die";
    public Animator anim;
    public ParticleSystem hit_effect;
    public ParticleSystem coins_effect;

    // raycast overlap sphere
    public Transform overlap_pos;
    public float overlap_radius;
    public LayerMask circles_layer;

    // player featured
    public GameObject player_skin;
    public float speed_player;
    public int player_stamina;
    public GameObject blood;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();

        player_skin = Instantiate(GameManager.instance.PlayersList[GameManager.instance.getactivSkin()], transform) as GameObject;

        //player_skin = transform.GetChild(0).gameObject;
        anim = player_skin.GetComponent<Animator>();
        speed_player = player_skin.GetComponent<PlayerInfo>().speed_player;
        player_stamina = player_skin.GetComponent<PlayerInfo>().player_stamina;

        // 
    }

    // Update is called once per frame
    void Update()
    {
        if (!can_move || !game_run)
            return;

        //
        Collider[] hitColliders = Physics.OverlapSphere(overlap_pos.position, overlap_radius, circles_layer);
        foreach (Collider hit in hitColliders)
        {
            if (!hit.GetComponent<Enemy>().is_killed && is_press)
            {
                hit.GetComponent<Enemy>().is_killed = true;
                
                StartCoroutine(kill_enemy_anim(hit.transform));
                //hit.GetComponent<Enemy>().effect_kill();
                print("enemy");
            }
        }

        
        //joystick
        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            // Move Player
            //is_move = true;
            is_press = true;
            //animation
            change_animation_state(Run_anim);

            //rotation forward
            transform.forward = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
        }
        else
        {
            is_press = false;
            change_animation_state(Idle_anim);
            //is_move = false;
        }

        
        //check if is grounded
        if (controller.isGrounded)
        {
            vertical_gravity_y = 0f;
        }
        else
        {
            vertical_gravity_y -= Time.deltaTime * gravity;
        }

        // movement
        move.x = joystick.Horizontal;
        move.z = joystick.Vertical;
        move.y = vertical_gravity_y;
        controller.Move(move * Time.deltaTime * speed_player);


    }

    public void change_animation_state(string state)
    {
        if(state != current_state_anim)
        {
            anim.Play(state);
            current_state_anim = state;
        }
    }
    
    public void active_effect_hit()
    {
        hit_effect.Play();
    }

    IEnumerator kill_enemy_anim(Transform enemy)
    {
        can_move = false;
        transform.LookAt(enemy);
        change_animation_state(Attack_anim);
        yield return new WaitForSeconds(.15f);
        //sound
        SoundManager.instance.Play("sword");

        yield return new WaitForSeconds(.5f);
        enemy.GetComponent<Enemy>().effect_kill();

        //check level complete
        GameController.instance.check_level_complete();

        change_animation_state(Idle_anim);
        can_move = true;
    }
}
