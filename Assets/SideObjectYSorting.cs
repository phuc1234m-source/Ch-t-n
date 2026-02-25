using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class YSortSprite : MonoBehaviour
{
    private SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void LateUpdate()
    {
        // Lower Y = higher sorting order
        sr.sortingOrder = Mathf.RoundToInt(-transform.position.y * 100);
    }
}