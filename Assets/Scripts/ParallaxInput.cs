using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ParallaxInput : MonoBehaviour
{
    public FreeParallax parallax;

    [Tooltip("Initial object to starts move when play the game (e.g. A cloud, a car...)")]
    public GameObject initialMoveObject;

    public float speed = 15.0f;

    [Tooltip("Is a infinite camera movement? Great to Infinite Runners games")]
    public bool infiniteMove = false;

    [Tooltip("The player rigidbody to move")]
    public GameObject player;

    public GameObject[] lanes;

    private List<GameObject> lanePlayer;

    void Awake() 
    {
        
        if (parallax == null)
        {
            parallax = GetComponentInChildren<FreeParallax>();
        }

        if (lanes.Length > 0)
        {
            lanePlayer = new List<GameObject>();
            for (int i = 0; i < lanes.Length; i++)
            {
                lanePlayer.Insert(i, null);
            }

            if (player != null)
            {
                lanePlayer[0] = player;
            }
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

            if (infiniteMove && parallax.IsHorizontal)
            {
                parallax.Speed = -speed;

                InputManager.DownHeld("Vertical", (result) => {

                    if (result != Vector3.zero)
                    {

                        if (lanes.Length > 0)
                        {
                            GameObject laneToMove;
                            var indexCurrentLane = lanePlayer.FindIndex(p => p != null && p.Equals(player));
                            if (result.y > 0)
                            {
                               laneToMove = lanes[indexCurrentLane + 1];
                            } else {
                                laneToMove = lanes[indexCurrentLane - 1];
                            }
                                
                            // lanes[indexPlayer].transform.position
                            player.transform.position = new Vector2(
                                player.transform.position.x,
                                laneToMove.transform.position.y + 0.1f
                            );
                        }

                        // var y = result.y > 0 ? 62 : -62;

                        // player.AddForce(new Vector2(0.0f, y));
                    }
                });
                
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
