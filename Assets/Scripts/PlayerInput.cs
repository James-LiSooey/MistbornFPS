using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Vector2 input
    {
        get
        {
            Vector2 i = Vector2.zero;
            i.x = Input.GetAxis("Horizontal");
            i.y = Input.GetAxis("Vertical");
            i *= (i.x != 0.0f && i.y != 0.0f) ? .7071f : 1.0f;
            return i;
        }
    }

    public Vector2 down
    {
        get { return _down; }
    }

    public Vector2 raw
    {
        get
        {
            Vector2 i = Vector2.zero;
            i.x = Input.GetAxisRaw("Horizontal");
            i.y = Input.GetAxisRaw("Vertical");
            i *= (i.x != 0.0f && i.y != 0.0f) ? .7071f : 1.0f;
            return i;
        }
    }

    public float elevate
    {
        get
        {
            return Input.GetAxis("Elevate");
        }
    }

    public bool run
    {
        get { return Input.GetButton("Sprint"); }
    }

    public bool crouch
    {
        get { return Input.GetButtonDown("Crouch"); }
    }

    public bool crouching
    {
        get { return Input.GetButton("Crouch"); }
    }

    public KeyCode interactKey
    { 
        get { return KeyCode.E; }
    }

    public bool lockOn
    {
        get { return Input.GetButton("lockOn"); }
    }

    public bool AimEquipped
    {
        get { return (Input.GetAxis("AimEquipped")>.1f || Input.GetMouseButton(0)); }
    }

    public bool interact
    {
        get { return Input.GetKeyDown(interactKey); }
    }

    public bool reload
    {
        get { return Input.GetKeyDown(KeyCode.R); }
    }

    public bool aim
    {
        get { return Input.GetMouseButtonDown(1); }
    }

    public bool aiming
    {
        get { return Input.GetMouseButton(1); }
    }

    // public bool shooting
    // {
    //     get { return Input.GetMouseButton(0); }
    // }

    // public float mouseScroll
    // { 
    //     get { return Input.GetAxisRaw("Mouse ScrollWheel"); }
    // }

    public bool push
    {
        get { return Input.GetButton("Push"); }
    }

    public bool pushRelease
    {
        get { return Input.GetButtonUp("Push"); }
    }

    public bool pull
    {
        get { return Input.GetButton("Pull"); }
    }

    public bool senseMetal
    {
        get { return Input.GetButton("SenseMetal");  }
    }

    public bool pauseMenuEnabled = false;

    private Vector2 previous;
    private Vector2 _down;

    private int jumpTimer;
    private bool jump;

    GameObject pauseMenu;

    void Start()
    {
        pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu");
        pauseMenu.SetActive(false);
        jumpTimer = -1;
    }

    void Update()
    {
        _down = Vector2.zero;
        if (raw.x != previous.x)
        {
            previous.x = raw.x;
            if (previous.x != 0)
                _down.x = previous.x;
        }
        if (raw.y != previous.y)
        {
            previous.y = raw.y;
            if (previous.y != 0)
                _down.y = previous.y;
        }

        if (Input.GetButtonDown("Pause")) {
            pauseMenuEnabled = !pauseMenuEnabled;
            if(pauseMenuEnabled) {
                pauseMenu.SetActive(true);
            } else {
                pauseMenu.SetActive(false);
            }
        }        
    }

    public void FixedUpdate()
    {
        if (!Input.GetButton("Jump"))
        {
            jump = false;
            jumpTimer++;
        }
        else if (jumpTimer > 0)
            jump = true;
    }

    public bool Jump()
    {
        return jump;
    }

    public void ResetJump()
    {
        jumpTimer = -1;
    }
}
