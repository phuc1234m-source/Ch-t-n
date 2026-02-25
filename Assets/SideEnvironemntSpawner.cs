using UnityEngine;

public class SideEnvironmentSpawner : MonoBehaviour
{
    [Header("House Prefabs")]
    public GameObject[] housePrefabs;

    [Header("Spawn Settings")]
    public float spawnY = 8f;            // top of screen
    public float verticalSpacing = 3f;   // match house height
    public float leftX = -5f;
    public float rightX = 5f;

    [Header("Movement")]
    public float moveSpeed = 5f;

    [Header("Density")]
    [Range(0f, 1f)]
    public float spawnChance = 0.3f;
    public float densityIncrease = 0.01f;
    public float maxSpawnChance = 0.85f;

    private float spawnTimer;
    private float spawnInterval;
    private int lastLeftIndex = -1;
    private int lastRightIndex = -1;
    void Start()
    {
        spawnInterval = verticalSpacing / moveSpeed;
    }

    void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            SpawnRow();
            spawnTimer = 0f;
        }
    }

    void SpawnRow()
    {
        spawnChance = Mathf.Min(
            spawnChance + densityIncrease * Time.deltaTime,
            maxSpawnChance
        );

        if (Random.value < spawnChance)
            SpawnHouse(leftX, true);

        if (Random.value < spawnChance)
            SpawnHouse(rightX, false);
    }

    void SpawnHouse(float xPos, bool isLeft)
    {
        if (housePrefabs.Length == 0) return;

        int newIndex;

        if (housePrefabs.Length == 1)
        {
            newIndex = 0;
        }
        else
        {
            do
            {
                newIndex = Random.Range(0, housePrefabs.Length);
            }
            while (
                (isLeft && newIndex == lastLeftIndex) ||
                (!isLeft && newIndex == lastRightIndex)
            );
        }

        GameObject prefab = housePrefabs[newIndex];

        GameObject house = Instantiate(
            prefab,
            new Vector3(xPos, spawnY, 0f),
            Quaternion.identity
        );

        // Save memory
        if (isLeft)
            lastLeftIndex = newIndex;
        else
            lastRightIndex = newIndex;

        // Movement
        SideObjectMover mover = house.GetComponent<SideObjectMover>();
        if (mover != null)
            mover.moveSpeed = moveSpeed;

        // Flip left
        if (isLeft)
        {
            SpriteRenderer sr = house.GetComponent<SpriteRenderer>();
            if (sr != null)
                sr.flipX = true;
        }
    }
}