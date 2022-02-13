using System.Collections;
using UnityEngine;

public class Bounce:MonoBehaviour
{
    [SerializeField] private float bounceValue = 0;
    [SerializeField] private float speed = 0;
    public AnimationCurve curve;
    public Vector3 end;
    Vector3 start;

    float time;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            SendBouncing();

        //time += speed * Time.deltaTime;
        //Vector3 pos = Vector3.Lerp(start, end, time);
        //pos.y += curve.Evaluate(time) * 2;
        //transform.position = pos;

        //if (time >= 1)
        //{
        //    time = 0;
        //    start = end;
        //    end = end + Vector3.forward;
        //}
    }

    public void SendBouncing()
    {
        StartCoroutine(Bouncing());
    }

    Vector3 pos = Vector3.zero;

    private IEnumerator Bouncing()
    {
        float elapsedTime = 0;

        pos = Vector3.zero;
        start = transform.position;

        while (elapsedTime < speed)
        {
            pos = Vector3.Lerp(start, start, (elapsedTime / speed));
            pos.y += curve.Evaluate(elapsedTime / speed) * bounceValue;
            transform.position = pos;
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        yield return null;
    }
}