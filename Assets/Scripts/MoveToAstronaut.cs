using System;
using UnityEngine;

public class MoveToAstronaut : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] private bool follow = true;
    private Vector3 playerPosition;
    private Transform playerTransform;
    private void Start()
    {
        Astronaut currentPlayer = FindObjectOfType<Astronaut>();
        if (currentPlayer)
        {
            playerTransform = currentPlayer.transform;
        }
    }

    private void Update()
    {
        if (playerTransform && follow)
        {
            playerPosition = playerTransform.position;
            var movementThisFrame = moveSpeed * Time.deltaTime;
            transform.right = playerPosition - transform.position;
            transform.position = Vector2.MoveTowards(transform.position, playerPosition, movementThisFrame);    
        }
    }

    public bool Follow
    {
        get => follow;
        set => follow = value;
    }
}