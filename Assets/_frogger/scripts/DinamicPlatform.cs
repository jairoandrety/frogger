using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    None =0,
    Right =1,
    Left = 2
}

public class DinamicPlatform : Platform
{
    [SerializeField] private int speed = 0;
    [SerializeField] private int size = 0;
    [SerializeField] private Direction direction = Direction.None;

    [Range(0,1)][SerializeField] private float maxValueRandom = 0;
    [SerializeField] private List<int> trunkSizes = new List<int>();

    [SerializeField] private Disabler disabler;
    [SerializeField] private GameObject GeneratorPoint;
    [SerializeField] private List<Trunk> trunks = new List<Trunk>();

    private ObjectPooler pooler;
    private float lastSize = 0;

    private float currentPos = 0;
    private float value = 0;
    private Trunk trunk;

    private void Start()
    {
        pooler = ObjectPooler.SharedInstance;
        disabler.OnDisabler += RemoveTrunk;
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
                disabler.transform.localPosition = new Vector3(-60, 0,0);
                break;
            default:                
                break;
        }

        StartCoroutine(InitPlatforms());
    }

    private IEnumerator InitPlatforms()
    {
        Debug.Log("init Platforms");

        switch (direction)
        {
            case Direction.Right:
                while (currentPos > -40)
                {
                    Debug.Log("Generate Platforms");
                    value = Random.value;

                    size = trunkSizes[Random.Range(0, trunkSizes.Count - 1)];

                    if (trunks.Count > 0)
                    {
                        float add = (lastSize / 2) + (size / 2);
                        currentPos = trunks[trunks.Count - 1].transform.localPosition.x - add;
                    }

                    GenerateTrunk();
                    lastSize = size;

                    yield return new WaitForSeconds(0.1f);
                }
                break;
            case Direction.Left:
                while (currentPos < 40)
                {
                    Debug.Log("Generate Platforms");
                    value = Random.value;

                    size = trunkSizes[Random.Range(0, trunkSizes.Count - 1)];

                    if (trunks.Count > 0)
                    {
                        float add = (lastSize / 2) + (size / 2);
                        currentPos = trunks[trunks.Count - 1].transform.localPosition.x + add;
                    }

                    GenerateTrunk();
                    lastSize = size;

                    yield return new WaitForSeconds(0.1f);
                }
                break;
            default:
                break;
        }

        trunks.ForEach(i => i.StartMove());
        yield return null;
    }

    private void RemoveTrunk(Trunk trunk)
    {
        switch (direction)
        {
            case Direction.None:
                break;
            case Direction.Right:
                trunks.Remove(trunk);
                currentPos = trunks[trunks.Count - 1].transform.localPosition.x;
                trunks.Add(trunk);
                this.trunk = trunk;

                break;
            case Direction.Left:
                 trunks.Remove(trunk);
                currentPos = trunks[trunks.Count - 1].transform.localPosition.x;
                trunks.Add(trunk);
                this.trunk = trunk;
                break;
            default:
                break;
        }

        //GenerateTrunk();
        MoveTrunk();
    }   

    private void GenerateTrunk()
    {
        GameObject newTrunk = pooler.GetPooledObject("trunk");             
        trunk = newTrunk.GetComponent<Trunk>();
        trunk.gameObject.SetActive(true);
        trunk.Enable(value <= maxValueRandom);
        trunk.transform.SetParent(this.transform);

        trunk.transform.localPosition = new Vector3(currentPos, 0, 0);

        trunk.SetupTrunk(size, direction == Direction.None ? Vector3.zero : direction == Direction.Right ? Vector3.right : Vector3.left, speed);
        trunks.Add(trunk);
        points.AddRange(trunk.Points);
    }  

    private void MoveTrunk()
    {
        int add = 0;
        size = trunkSizes[Random.Range(0, trunkSizes.Count - 1)];

        switch (direction)
        {
            case Direction.Right:
                trunk.SetupTrunk(size, Vector3.right, speed);

                add = (trunks[trunks.Count - 2].Size / 2) - (size / 2);
                if (trunks.Count > 0)
                    currentPos -= size + add;
                break;

            case Direction.Left:
                trunk.SetupTrunk(size, Vector3.left, speed);

                add = (trunks[trunks.Count - 2].Size / 2) - (size / 2);
                if (trunks.Count > 0)
                    currentPos += size + add;
                break;

            default:
                break;
        }
        
        trunk.gameObject.transform.localPosition = new Vector3(currentPos, 0, 0);
        trunk.gameObject.SetActive(true);
    }
}