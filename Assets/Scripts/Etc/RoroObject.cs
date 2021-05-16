using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoroObject : MonoBehaviour
{

    private new Rigidbody2D rigidbody2D;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    private bool gravityChanged = false;
    private bool blinked = false;

    public string destroyKey;
    public int Keyrandomized;

    public static float maxTime;
    private float timeKill;
    private float timeBlink;
    private bool askedToLose;

    private void Start()
    {
        timeKill = Time.time + maxTime;
        timeBlink = Time.time + (maxTime / 2);

        rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        rectTransform = gameObject.GetComponent<RectTransform>();
        canvasGroup = gameObject.GetComponent<CanvasGroup>();


        StartCoroutine(Entrance());
    }

    private void Update()
    {
        if (!blinked)
        {
            StartCoroutine(Blink());
            blinked = true;
        }
        if (Time.time > timeKill)
        {
            StartCoroutine(DestroyObj());
            if (!askedToLose)
            {
                askedToLose = true;
                GameSceneManager.RoundLose();
            }
        }
        if (!gravityChanged)
        {
            StartCoroutine(Gravity());
            gravityChanged = true;
        }
        if (rectTransform.localPosition.y > 520) 
        {
            rectTransform.localPosition = new Vector2 (rectTransform.localPosition.x, 520);
        }
        else if (rectTransform.localPosition.y < -370)
        {
            rectTransform.localPosition = new Vector2(rectTransform.localPosition.x, -370);
        }
        
        if(Input.GetKeyDown(destroyKey))
        {
            StartCoroutine(DestroyObj());
        }

    }

    public void ClickDestroy()
    {
        StartCoroutine(DestroyObj());
    }

    IEnumerator Blink()
    {

        while (canvasGroup.alpha != 0f)
        {
            canvasGroup.alpha -= 0.08f;
            yield return new WaitForSeconds(0.01f);
        }

        while (canvasGroup.alpha < 0.799f)
        {
            canvasGroup.alpha += 0.08f;
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.1f * (timeKill - Time.time));
        blinked = false;
    }

    IEnumerator Entrance()
    {
        canvasGroup.alpha = 0.8f;
        for (float i = 0f; i < 1.21f; i += 0.1f)
        {
            rectTransform.localScale = new Vector3(i, i, i);
            yield return new WaitForSeconds(0.01f);
        }
        rectTransform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
        yield return new WaitForSeconds(0.01f);

        rectTransform.localScale = new Vector3(1f, 1f, 1f);
        yield return new WaitForSeconds(0.01f);

    }

    IEnumerator DestroyObj()
    {
        rectTransform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
        yield return new WaitForSeconds(0.01f);

        rectTransform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        yield return new WaitForSeconds(0.01f);

        for (float i = 1.1f; i > 0; i -= 0.1f)
        {
            rectTransform.localScale = new Vector3(i,i,i);
            yield return new WaitForSeconds(0.01f);
        }
        RoroSpawner.keySummoned[Keyrandomized] = 0;
        Destroy(gameObject);
    }

    IEnumerator Gravity()
    {
        rigidbody2D.gravityScale = Random.Range(-5, 5) * 5;
        rigidbody2D.velocity = new Vector2(0, rigidbody2D.gravityScale > 0 ? 200 : -200);
        yield return new WaitForSeconds(1f);
        gravityChanged = false;
    }
}
