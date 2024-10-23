using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformX : MonoBehaviour
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
            yield return StartCoroutine(Move1());
            yield return new WaitForSeconds(1f);
            yield return StartCoroutine(Move2());
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator Move1()
    {
        float time = 0f;
        Vector3 endPos = startPos - new Vector3(moveDistance, 0f, 0f);

        while (time < pauseTime)
        {
            time += Time.deltaTime * moveSpeed;
            transform.position = Vector3.Lerp(startPos, endPos, time);
            yield return null;
        }
        transform.position = endPos;
    }

    IEnumerator Move2()
    {
        float time = 0f;
        Vector3 endPos = startPos;

        while (time < pauseTime)
        {
            time += Time.deltaTime * moveSpeed;
            transform.position = Vector3.Lerp(startPos - new Vector3(moveDistance, 0f, 0f), endPos, time);
            yield return null;
        }
        transform.position = endPos;
    }
}
