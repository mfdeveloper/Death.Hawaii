using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxInput : MonoBehaviour
{
     public FreeParallax parallax;
    [Tooltip("Initial object to starts move when play the game (e.g. A cloud, a car...)")]
    public GameObject initialMoveObject;

    public float speed = 15.0f;

    [Tooltip("Is a infinite camera movement? Great to Infinite Runners games")]
    public bool infiniteMove = false;

    void Awake() {
        
        if (parallax == null)
        {
            parallax = GetComponentInChildren<FreeParallax>();
        }    
    }

    // Use this for initialization
    void Start()
    {
        if (initialMoveObject != null)
        {
            initialMoveObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0.1f, 0.0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (parallax != null)
        {

            if (infiniteMove)
            {
                parallax.Speed = -speed;
            } else {

                InputManager.DownHeld("Horizontal", (result) => {

                    if (result != Vector3.zero)
                    {
                        parallax.Speed = result.x > 0 ? -speed : speed;
                    } else {
                        parallax.Speed = 0.0f;
                    }
                });
            }
        } else {
            Debug.LogError("A FreeParallax script reference is required to use parallax effect");
        }
    }
}
