using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float moveDistance = 1f;
    public float moveSpeed = 1f;
    public float pauseTime = 2f;
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
        StartCoroutine(MoveObject());
    }

    IEnumerator MoveObject()
    {
        while (true)
        {
            yield return StartCoroutine(MoveDown());
            yield return new WaitForSeconds(1f);
            yield return StartCoroutine(MoveUp());
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator MoveDown()
    {
        float time = 0f;
        Vector3 endPos = startPos - new Vector3(0f, moveDistance, 0f);

        while (time < pauseTime)
        {
            time += Time.deltaTime * moveSpeed;
            transform.position = Vector3.Lerp(startPos, endPos, time);
            yield return null;
        }
        transform.position = endPos;
    }

    IEnumerator MoveUp()
    {
        float time = 0f;
        Vector3 endPos = startPos;

        while (time < pauseTime)
        {
            time += Time.deltaTime * moveSpeed;
            transform.position = Vector3.Lerp(startPos - new Vector3(0f, moveDistance, 0f), endPos, time);
            yield return null;
        }
        transform.position = endPos;
    }
}
