using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    ControlManagerUnity is the central controller for game input.
    It is necessary to facilitate custom key bindings and alternative input systems
    Input handling is very engine-specific, so this one has "Unity" in the name
    Should this game presentation framework continue to evolve, other non-unity control managers could exist.
*/

public class ControlManagerUnity : MonoBehaviour
{
    public GameManager game;
    public bool anyInput()
    {
         return Input.anyKey;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
}
