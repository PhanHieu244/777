using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    PlayerController player_script;
    public float distance_to_player = 10f, seeangle = 50f;
    public List<Transform> list_path;
    public int current_destination;
    public float time_wait;
    public bool can_move = true , can_shoot , player_detected;
    public GameObject blood;
    public GameObject field_view;
    NavMeshAgent agent;

    //enemy animation
    public Animator anim;
    string current_state_anim = "walk";
    const string Idle_anim = "idle", Run_anim = "Run",
        Attack_anim = "attack";

    //raycast
    public Transform[] raycast_list;
    public float ray_distance;
    public LayerMask player_layer;
    //bool ray1, ray2, ray3, ray;

    //bullet
    public GameObject bullet;
    public float bullet_speed;
    public Transform bullet_pos;

    // particle system
    public ParticleSystem hit_effect;
    public ParticleSystem emoji_effect;

    public bool is_killed , game_run;
    // Start is called before the first frame update
    void Start()
    {
        player_script = FindObjectOfType<PlayerController>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = 4f;
        //set first destination
        set_destination();
    }

    private void LateUpdate()
    {
        // hide mesh path
        hide_mesh_of_path_position();


        //if player detected
        if (player_detected)
        {
            transform.LookAt(player_script.transform);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (!FindObjectOfType<PlayerController>().game_run)
            return;

        //if enemy reach position
        if (agent.hasPath && agent.remainingDistance <= .1f && can_move)
        {
            print("in");
            can_move = false;
            agent.isStopped = true;
            StartCoroutine(animate_enemy_rotate_once_reach(time_wait));
        }

        if (!game_run)
            return;
        //raycast
        for (int i = 0; i < raycast_list.Length; i++)
        {
            check_player_by_raycast(raycast_list[i]);
        }

        

        //draw raycast for test
        Debug.DrawRay(raycast_list[0].position, raycast_list[0].forward * ray_distance, Color.red);
        Debug.DrawRay(raycast_list[1].position, raycast_list[1].forward * ray_distance, Color.red);
        Debug.DrawRay(raycast_list[2].position, raycast_list[2].forward * ray_distance, Color.red);
        Debug.DrawRay(raycast_list[3].position, raycast_list[3].forward * ray_distance, Color.red);
        Debug.DrawRay(raycast_list[3].position, raycast_list[4].forward * ray_distance, Color.red);
    }

    //void check_player_by_angle()
    //{
    //    Vector3 angDirection = player_script.transform.position - transform.position;
    //    float angle = Vector3.Angle(angDirection, this.transform.forward);

    //    if (angDirection.magnitude < distance_to_player && angle < seeangle)
    //    {

    //    }

        
    //}

    void set_destination()
    {
        if (!is_killed)
        {
            agent.SetDestination(list_path[current_destination].position);
            if (list_path.Count == current_destination + 1)
                current_destination = 0;
            else
                current_destination++;
        }
        
    }

    IEnumerator animate_enemy_rotate_once_reach(float time_wait)
    {
        
        // turn to right
        for (int i = 0; i < 100; i++)
        {
            Vector3 tmp = transform.eulerAngles;
            tmp.y += 1;
            transform.eulerAngles = tmp;
            yield return new WaitForSeconds(time_wait);
            if (is_killed)
                break;
        }

        // turn to left
        for (int i = 0; i < 100; i++)
        {
            Vector3 tmp = transform.eulerAngles;
            tmp.y -= 1;
            transform.eulerAngles = tmp;
            yield return new WaitForSeconds(time_wait);
            if (is_killed)
                break;
        }

        if (!is_killed)
        {
            //set destination
            agent.isStopped = false;
            can_move = true;
            set_destination();
        }
        
    }

    void change_animation_state(string state)
    {
        if (state != current_state_anim)
        {
            anim.Play(state);
            current_state_anim = state;
        }
    }

    void check_player_by_raycast(Transform ray_tr)
    {
        RaycastHit hit;
        Ray ray = new Ray(ray_tr.position, ray_tr.forward);

        
        if(Physics.Raycast(ray , out hit , ray_distance , player_layer))
        {
            if (!player_detected)
            {
                print("detect");

                //active effect emoji
                effect_emoji_statut(true);


                agent.isStopped = true;

                player_detected = true;

                // shoot player
                shooting_player();

                //wait for next shoot
                StartCoroutine(wait_to_next_shoot());
            }
            

        }
    }

    IEnumerator wait_to_next_shoot()
    {
        can_shoot = false;
        yield return new WaitForSeconds(.5f);
        player_detected = false;
        //return to patrol
        if(!is_killed)
            agent.isStopped = false;

        set_destination();
        //desactivate effect emoji
        effect_emoji_statut(false);

        can_shoot = true;

    }

    void shooting_player()
    {

        //sound
        SoundManager.instance.Play("bullet");
        GameObject blt = Instantiate(bullet , bullet_pos.position , bullet.transform.rotation) as GameObject;

        Vector3 dirc = player_script.transform.position - blt.transform.position;
        dirc.y = .5f;
        Rigidbody rb = blt.GetComponent<Rigidbody>();
        rb.AddForce(dirc.normalized * bullet_speed * Time.deltaTime, ForceMode.Impulse);
    }

    void effect_emoji_statut(bool st)
    {
        if (st)
        {
            if(emoji_effect.isStopped)
                emoji_effect.Play();
        }
        else
        {
            emoji_effect.Stop();
        }
    }

    public void effect_kill()
    {
        emoji_effect.gameObject.SetActive(false);
        hit_effect.Play();
        blood.SetActive(true);
        field_view.SetActive(false);
        agent.isStopped = true;
        agent.enabled = false;
        anim.gameObject.SetActive(false);
        this.enabled = false;
    }

    //IEnumerator kill_anim()
    //{
        

    //    yield return new WaitForSeconds(.5f);

    //}

    void hide_mesh_of_path_position()
    {
        for (int i = 0; i < list_path.Count; i++)
        {
            list_path[i].GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
