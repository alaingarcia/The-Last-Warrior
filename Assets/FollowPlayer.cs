using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    private Transform player;
    private Vector3 initial_position;
    private Vector3 initial_player_position;

    void Start()
    {
        player = GameObject.Find("Player").transform;
        initial_position = transform.position;
        initial_player_position = player.position;
    }

    void Update()
    {
        Vector3 newPosition = player.position + initial_position - initial_player_position;
        transform.position = newPosition;
    }
}
