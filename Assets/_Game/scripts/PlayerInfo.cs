using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public float speed_player;
    public int player_stamina;
    public GameObject[] stamina_sprites_list;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void delete_sprite_stamina(int nbr)
    {
        stamina_sprites_list[nbr].SetActive(false);
    }
}
