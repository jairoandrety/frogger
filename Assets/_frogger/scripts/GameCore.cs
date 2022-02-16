using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Cinemachine;

public class GameCore : MonoBehaviour
{
    [SerializeField] private List<Frog> frogs = new List<Frog>();
    [SerializeField] private List<Platform> platforms = new List<Platform>();

    [SerializeField] private CinemachineVirtualCamera camera;

    [SerializeField] private GameCoreView view;

    [SerializeField] private int lifes = 0;
    [SerializeField] private float timer = 0;

    private Frog currentFrog;
    private int frogSelected = 0;

    private int platformSelected = 0;
    private int platformsCompleted = 0;

    private int points = 0;
    private int currentLifes = 0;
    private float currentTimer = 0;

    private int selected = 0;
    private Point pointSelected;

    #region UnityBehaviour
    void Start()
    {
        ResetValues();
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

                if (ValidateMovement(direction))
                {
                    currentFrog.Move(pointSelected.gameObject, direction);
                    view.SetUIValues(points, currentLifes);
                }
            }

            if (vertical)
            {
                float value = Input.GetAxisRaw("Vertical");
                direction = new Vector3(0, 0, value);
                selected = platformSelected + (int)direction.z;

                if (ValidateMovement(direction))
                {
                    currentFrog.Move(pointSelected.gameObject, direction);

                    if (value > 0)
                    {
                        platformSelected++;
                        if (!platforms[platformSelected].IsReached)
                        {
                            platforms[platformSelected].SetReached(true);
                            platformsCompleted++;                            
                        }
                    }
                    else
                    {
                        platformSelected--;
                    }

                    points = platformsCompleted * 10;
                    view.SetUIValues(points, currentLifes);
                }
            }
        }

        currentTimer -= Time.deltaTime;
        view.SetTimeValue(currentTimer / timer);

        if(currentTimer <= 0)
        {
            Dead();
        }
    }
    #endregion

    private void Grounded()
    {
        if (platformsCompleted >= 14)
            NextFrog();
    }

    private void Dead()
    {
        Debug.Log("dead");

        currentFrog.ResetValues();

        if (pointSelected != null)
            pointSelected.UsePoint(false);

        pointSelected = null;
        platforms.ForEach(i => i.SetReached(false));

        currentTimer = timer;
        currentLifes--;
        platformsCompleted = 0;
        selected = 0;
        platformSelected = 0;

        view.SetUIValues(points, currentLifes);

        if (currentLifes <= 0)
            GameOver();       
    }

    private void GameOver()
    {
        Debug.Log("Game Over");
        view.ActivePanels(1);
    }

    private void NextFrog()
    {
        if (currentFrog != null)
        {
            currentFrog.OnDead -= Dead;
            currentFrog.OnGrounded -= Grounded;
        }

        if (frogSelected < frogs.Count -1)
        {
            frogSelected++;
            SelectFrog();
        }
        else
        {
            Win();
        }

        pointSelected = null;
        platforms.ForEach(i => i.SetReached(false));

        currentTimer = timer;
        currentLifes = lifes;
        platformsCompleted = 0;
        selected = 0;
        platformSelected = 0;
        view.SetUIValues(points, currentLifes);
    }

    private void SelectFrog()
    {
        currentFrog = frogs[frogSelected];
        camera.Follow = currentFrog.transform;

        currentFrog.OnDead += Dead;
        currentFrog.OnGrounded += Grounded;
    }    

    private void Win()
    {
        Debug.Log("Win");
        //ResetValues();
        view.ActivePanels(2);
    }

    public void ResetGame()
    {
        ResetValues();
    }

    private void ResetValues()
    {
        pointSelected = null;

        currentTimer = timer;
        currentLifes = lifes;
        frogSelected = -1;
        platformsCompleted = 0;
        selected = 0;
        platformSelected = 0;

        platforms.ForEach(i => i.ResetValues());
        frogs.ForEach(i => i.ResetValues());

        NextFrog();
        view.ActivePanels(0);
    }

    private bool ValidateMovement(Vector3 direction)
    {
        Point currentPoint = pointSelected;
        Point point = platforms[selected].FindPoint(currentFrog.transform.position + (direction * currentFrog.DirectionForce));

        if (point != null)
        {
            if(point.IsUsed)
            {
                selected = platformSelected;
                Debug.Log("reset selected");
            }
            else
            {
                if (pointSelected != null)
                    pointSelected.UsePoint(false);

                pointSelected = point;
                pointSelected.UsePoint(true);
            }            
        }

        if(currentPoint != pointSelected)
        {
            return true;
        }
        else
        {
            pointSelected = currentPoint;
            return false;
        }
    }
}