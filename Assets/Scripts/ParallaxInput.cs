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

    [Tooltip("The player gameobject to move")]
    public GameObject player;

    [Tooltip("You can pass Lanes game objects to teleport/move the player")]
    public GameObject[] lanes;

    private List<GameObject> lanesWithPlayer;

    void Awake() 
    {
        
        if (parallax == null)
        {
            parallax = GetComponentInChildren<FreeParallax>();
        }

        if (lanes.Length > 0)
        {
            lanesWithPlayer = new List<GameObject>();
            for (int i = 0; i < lanes.Length; i++)
            {
                lanesWithPlayer.Insert(i, null);
            }

            if (player != null)
            {
                lanesWithPlayer[0] = player;
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

                InputManager.Down("Vertical", (result) => {

                    if (result != Vector3.zero)
                    {

                        if (lanes.Length > 0)
                        {
                            GameObject laneToMove;
                            int indexLaneToMove;
                            var indexCurrentLane = lanesWithPlayer.FindIndex(p => p != null && p.Equals(player));
                            
                            if (indexCurrentLane != -1)
                            {
                                if (result.y > 0)
                                {
                                    indexLaneToMove = indexCurrentLane + 1;
                                } else {
                                    indexLaneToMove = indexCurrentLane - 1;
                                }
                                
                                if (indexLaneToMove >= 0 && indexLaneToMove < lanes.Length)
                                {
                                    laneToMove = lanes[indexLaneToMove];
                                
                                    player.transform.position = new Vector2(
                                        player.transform.position.x,
                                        laneToMove.transform.position.y + 0.15f
                                    );
                                    
                                    lanesWithPlayer[indexLaneToMove] = player;
                                    lanesWithPlayer[indexCurrentLane] = null;
                                }  
                            }
                        }
                    }
                }, player);
                
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
