using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class DinamicObstacles: MonoBehaviour
{
    [SerializeField] private int speed = 0;
    [SerializeField] private int size = 0;
    [SerializeField] private Direction direction = Direction.None;

    [Range(0, 1)] [SerializeField] private float maxValueRandom = 0;
    [SerializeField] private List<int> trunkSizes = new List<int>();

    [SerializeField] private Disabler disabler;
    [SerializeField] private GameObject GeneratorPoint;
    [SerializeField] private List<Obstacle> obstacles = new List<Obstacle>();

    private ObjectPooler pooler;
    private float lastSize = 0;

    private float currentPos = 0;
    private float value = 0;
    private Obstacle obstacle;

    private void Start()
    {
        pooler = ObjectPooler.SharedInstance;
        disabler.OnDisablerObstacle += Remove;
        //i.OnDisabler += GenerateTrunk;

        switch (direction)
        {
            case Direction.None:

                break;
            case Direction.Right:
                currentPos = 40;
                GeneratorPoint.transform.localPosition = new Vector3(currentPos, 0, 0);
                disabler.transform.localPosition = new Vector3(60, 0, 0);
                break;
            case Direction.Left:
                currentPos = -40;
                GeneratorPoint.transform.localPosition = new Vector3(currentPos, 0, 0);
                disabler.transform.localPosition = new Vector3(-60, 0, 0);
                break;
            default:
                break;
        }

        StartCoroutine(Init());
    }

    private IEnumerator Init()
    {
        Debug.Log("init Obstacles");

        switch (direction)
        {
            case Direction.Right:
                while (currentPos > -40)
                {
                    value = Random.value;

                    //size = trunkSizes[Random.Range(0, trunkSizes.Count - 1)];

                    if (obstacles.Count > 0)
                    {
                        float add = (lastSize / 2) + (size / 2);
                        currentPos = obstacles[obstacles.Count - 1].transform.localPosition.x - add;
                    }

                    Generate();
                    lastSize = size;

                    yield return new WaitForSeconds(0.1f);
                }
                break;
            case Direction.Left:
                while (currentPos < 40)
                {
                    Debug.Log("Generate Platforms");
                    value = Random.value;

                    //size = trunkSizes[Random.Range(0, trunkSizes.Count - 1)];

                    if (obstacles.Count > 0)
                    {
                        float add = (lastSize / 2) + (size / 2);
                        currentPos = obstacles[obstacles.Count - 1].transform.localPosition.x + add;
                    }

                    Generate();
                    lastSize = size;

                    yield return new WaitForSeconds(0.1f);
                }
                break;
            default:
                break;
        }

        obstacles.ForEach(i => i.StartMove());
        yield return null;
    }

    private void Remove(Obstacle item)
    {
        switch (direction)
        {
            case Direction.None:
                break;
            case Direction.Right:
                obstacles.Remove(item);
                currentPos = obstacles[obstacles.Count - 1].transform.localPosition.x;
                obstacles.Add(item);
                this.obstacle = item;

                break;
            case Direction.Left:
                obstacles.Remove(item);
                currentPos = obstacles[obstacles.Count - 1].transform.localPosition.x;
                obstacles.Add(item);
                this.obstacle = item;
                break;
            default:
                break;
        }

        //GenerateTrunk();
        Move();
    }

    [SerializeField] private string obstacleName = string.Empty;

    private void Generate()
    {
        GameObject newObstacle = pooler.GetPooledObject(obstacleName);
        obstacle = newObstacle.GetComponent<Obstacle>();
        obstacle.gameObject.SetActive(true);
        obstacle.Enable(value <= maxValueRandom);
        obstacle.transform.SetParent(this.transform);

        obstacle.transform.localPosition = new Vector3(currentPos, 0, 0);

        obstacle.SetupTrunk(size, direction == Direction.None ? Vector3.zero : direction == Direction.Right ? Vector3.right : Vector3.left, speed);
        obstacles.Add(obstacle);
    }

    private void Move()
    {
        int add = 0;
        size = trunkSizes[Random.Range(0, trunkSizes.Count - 1)];

        switch (direction)
        {
            case Direction.Right:
                obstacle.SetupTrunk(size, Vector3.right, speed);

                add = (obstacles[obstacles.Count - 2].Size / 2) - (size / 2);
                if (obstacles.Count > 0)
                    currentPos -= size + add;
                break;

            case Direction.Left:
                obstacle.SetupTrunk(size, Vector3.left, speed);

                add = (obstacles[obstacles.Count - 2].Size / 2) - (size / 2);
                if (obstacles.Count > 0)
                    currentPos += size + add;
                break;

            default:
                break;
        }

        obstacle.gameObject.transform.localPosition = new Vector3(currentPos, 0, 0);
        obstacle.gameObject.SetActive(true);
    }
}