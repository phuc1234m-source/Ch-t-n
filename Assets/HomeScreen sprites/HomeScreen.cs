using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class HomeController : MonoBehaviour
{
    public Transform shipper;

    public Vector3 stopPosition;

    public float speed = 3f;

    bool introFinished = false;

    void Start()
    {
        StartCoroutine(DriveIn());
    }

    IEnumerator DriveIn()
    {
        Vector3 start = shipper.position;

        float duration = 2.5f;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;

            float t = time / duration;

            t = Mathf.SmoothStep(0f, 1f, t);

            shipper.position = Vector3.Lerp(start, stopPosition, t);

            yield return null;
        }

        shipper.position = stopPosition;

        introFinished = true;
    }
    IEnumerator EngineVibrate(float duration)
    {
        Vector3 original = shipper.position;

        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;

            float x = Random.Range(-0.02f, 0.02f);
            float y = Random.Range(-0.02f, 0.02f);

            shipper.position = original + new Vector3(x, y, 0);

            yield return null;
        }

        shipper.position = original;
    }
    public void StartGame()
    {
        if (!introFinished) return;

        StartCoroutine(LeaveSequence());
    }

    IEnumerator LeaveSequence()
    {
        yield return StartCoroutine(EngineVibrate(0.8f));

        yield return StartCoroutine(DriveOut());
    }

    IEnumerator DriveOut()
    {
        Vector3 start = shipper.position;

        float top = Camera.main.orthographicSize;
        float offscreenY = Camera.main.transform.position.y + top + 2f;

        Vector3 end = new Vector3(start.x, offscreenY, 0);

        float distance = Vector3.Distance(start, end);

        float duration = distance / speed;

        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;

            float t = time / duration;
            t = Mathf.SmoothStep(0f, 1f, t);

            shipper.position = Vector3.Lerp(start, end, t);

            yield return null;
        }

        SceneManager.LoadScene("Gameplay");
    }
}