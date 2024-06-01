using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallsDownManager : MonoBehaviour
{
    private WallDownUpMovement[] WallDownUpMovements;
    private float WallDownOffset = 2.0f;
    private int index = 0;
    private bool StartWallsDown = false;
    private float elapsedTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        WallDownUpMovements = GetComponentsInChildren<WallDownUpMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (StartWallsDown)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime >= WallDownOffset)
            {
                elapsedTime = 0f;

                if (index < WallDownUpMovements.Length)
                {
                    WallDownUpMovements[index].goDown();
                    index++;
                }
                else
                {
                    StartWallsDown = false; // Stop the process if all walls have been moved down
                }
            }
        }
    }

    // Method to start the walls down process
    public void StartWallsDownProcess()
    {
        StartWallsDown = true;
        index = 0; // Reset index to start from the beginning
    }
}
