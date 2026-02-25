using UnityEngine;

public class CarObstacle : MonoBehaviour
{
    public enum Direction
    {
        Up,
        Down
    }

    public Direction moveDirection;
    public float upSpeed = 4f;
    public float downSpeed = 6f;
    public Sprite[] carSprites; // Pick car sprites 
    private float topLimit;
    private float bottomLimit;

    void Start()
    {
        float camHeight = Camera.main.orthographicSize;
        float camY = Camera.main.transform.position.y;
        AssignRandomSprite();
        topLimit = camY + camHeight + 2f;
        bottomLimit = camY - camHeight - 2f;
    }

    void Update()
    {
        Move();
        CheckIfOffScreen();
    }

    void Move()
    {
        if (moveDirection == Direction.Up)
        {
            transform.Translate(Vector3.up * upSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.down * downSpeed * Time.deltaTime);
        }
    }

    void CheckIfOffScreen()
    {
        if (transform.position.y > topLimit ||
            transform.position.y < bottomLimit)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Game Over");
            Time.timeScale = 0f;
        }
    }

    void AssignRandomSprite()
    {
        if (carSprites.Length == 0) return;

        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        int randomIndex = Random.Range(0, carSprites.Length);
        sr.sprite = carSprites[randomIndex];
    }
}