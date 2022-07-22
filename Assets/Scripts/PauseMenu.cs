using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    public static bool Paused;
    void Update()
    {
        if (Input.GetButtonDown("Pause")){
            Paused = true;
        }
    }
}
