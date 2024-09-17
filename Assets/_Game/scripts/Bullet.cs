using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            print("hit player");
            collision.gameObject.GetComponent<PlayerController>().active_effect_hit();
            Destroy(gameObject);

            //check player lose
            GameController.instance.check_lose();
        }
        
    }
}
