using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    Vector2 currentPos; //pozycja followersa

    [SerializeField] GameObject player; //obiekt za którym ma podążać

    private Transform playerPos; //pozycja powyższego obiektu
    public float distance; //na jakim dystansie ma podążać
    public float speedEnemy; //prędkość z jaką ma podążać

    void Start()
    {
        playerPos = player.GetComponent<Transform>(); //pozycja gracza
        currentPos = GetComponent<Transform>().position; //pozycja wroga
    }

    void Update()
    {

        if (Vector2.Distance(transform.position, playerPos.position) < distance) 
        {
            transform.position = Vector2.MoveTowards(transform.position, playerPos.position, speedEnemy * Time.deltaTime);
        }
        //gdy brak gracza na określonym dystansie
        else
        {
            if (Vector2.Distance(transform.position, currentPos) <= 0)
            {
                //jeśli followers jest na początkowym miejscu nic nie robi
            }
            else
            {
                //wróc na początkową pozycję
                transform.position = Vector2.MoveTowards(transform.position, currentPos, speedEnemy * Time.deltaTime);
            }
        }
    }
}
