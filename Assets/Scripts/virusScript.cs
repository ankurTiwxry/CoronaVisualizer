using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class virusScript : MonoBehaviour
{

    GameMan player;

    private void Start()
    {
        player =  GameObject.Find("GameMan").GetComponent<GameMan>();
    }

    private void FixedUpdate()
    {
        transform.localPosition = transform.localPosition + new Vector3(0.0f, -1.0f, 0.0f) * player.speed;
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.name == "Ground")
        {
            player.Score += 10;
            //Debug.Log("Here");
            Destroy(gameObject, 0.1f);

        }

        if (col.collider.name == "Player")
        {
            Destroy(col.collider.gameObject, 0.3f);

        }

    }
}
