using System.Collections;
using UnityEngine;

public class Bounce:MonoBehaviour
{
    private bool isBouncing = false;

    [SerializeField] private float bounceValue = 0;
    [SerializeField] private float speed = 0;
    [SerializeField] private AnimationCurve curve;
    
    private Vector3 currentPos = Vector3.zero;
    private Vector3 initPos = Vector3.zero;

    private void Start()
    {
        initPos = transform.position;
        pos = transform.position;
    }

    [ContextMenu("SendBouncing")]
    public void SendBouncing()
    {
        if (isBouncing)
            return;

        currentPos = new Vector3(initPos.x, initPos.y, initPos.z);
        pos = transform.position;

        StartCoroutine(Bouncing());
    }

    Vector3 pos = Vector3.zero;

    private IEnumerator Bouncing()
    {
        isBouncing = true;
        float elapsedTime = 0;

        while (elapsedTime < speed)
        {
            pos = Vector3.Lerp(currentPos, currentPos, (elapsedTime / speed));
            pos.y += curve.Evaluate(elapsedTime / speed) * bounceValue;
            transform.position = pos;
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        isBouncing = false;
        transform.position = initPos;
        yield return null;
    }


}