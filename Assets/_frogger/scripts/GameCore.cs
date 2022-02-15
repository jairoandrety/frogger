using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Cinemachine;

public class GameCore : MonoBehaviour
{
    [SerializeField] private List<Frog> frogs = new List<Frog>();
    [SerializeField] private List<Platform> platforms = new List<Platform>();

    [SerializeField] private CinemachineVirtualCamera camera;


    private Frog currentFrog;
    private int frogSelected = 0;
    private int platformSelected = 0;

    private int lifes = 0;
    private int currentLifes = 0;


    void Start()
    {
        //platformSelected = -1;
        SelectFrog();
    }

    private void SelectFrog()
    {
        currentFrog = frogs[frogSelected];
        camera.Follow = currentFrog.transform;
    }

    void Update()
    {
        Vector3 direction = Vector3.zero;

        if (!currentFrog.IsMove && currentFrog.Body.IsGrounded)
        {
            bool horizontal = Input.GetButtonDown("Horizontal");
            bool vertical = Input.GetButtonDown("Vertical");

            if (horizontal)
            {
                float value = Input.GetAxisRaw("Horizontal");
                direction = new Vector3(value, 0, 0);

                if (ValidateMovementHorizontal(direction))
                    currentFrog.Move(pointSelected.gameObject, direction);
            }

            if (vertical)
            {
                float value = Input.GetAxisRaw("Vertical");
                direction = new Vector3(0, 0, value);

                bool check = ValidateMovementVertical(direction);
                Debug.Log(check);
                if (check)
                {
                    currentFrog.Move(pointSelected.gameObject, direction);

                    if (value > 0)
                        platformSelected++;
                    else
                        platformSelected--;
                }
            }
        }      
    }

    private bool ValidateMovementHorizontal(Vector3 direction)
    {
        //selected = platformSelected + (int)direction.x;
        pointSelected = platforms[selected].FindPoint(currentFrog.transform.position + (direction * currentFrog.DirectionForce));
        return pointSelected != null;
    }

    int selected = 0;
    private Point pointSelected;

    private bool ValidateMovementVertical(Vector3 direction)
    {
        selected = platformSelected + (int)direction.z;
        pointSelected = platforms[selected].FindPoint(currentFrog.transform.position + (direction * currentFrog.DirectionForce));
        return pointSelected != null;
    }
}