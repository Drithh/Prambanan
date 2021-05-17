using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Blueprint1Block : MonoBehaviour
{

    public int[] totalCandi;
    private bool transitionAsked = false;

    void Start()
    {
        CandiSpawner.totalSpawnBlock[0] += totalCandi[0];
        CandiSpawner.totalSpawnBlock[1] += totalCandi[1];
    }

    private void Update()
    {
        Transform child = transform.GetChild(0);

        if (!child.CompareTag("GuideParent") && !transitionAsked)
        {
            transitionAsked = true;
            StartCoroutine(TransitionCandi());
        }
    }

    IEnumerator TransitionCandi()
    {
        for (int i = 0; i < 15; i++)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 1.2f, transform.position.z);
            transform.localScale = new Vector3(transform.localScale.x + 0.005f, transform.localScale.y + 0.005f, 0);
            transform.GetComponent<CanvasGroup>().alpha -= 0.05f;
            yield return new WaitForSecondsRealtime(0.001f * i);
        }
        transform.GetComponent<AudioSource>().Play();
        for (int i = 0; i < 20; i++)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 1.2f, transform.position.z);
            transform.localScale = new Vector3(transform.localScale.x - 0.005f, transform.localScale.y - 0.005f, 0);
            transform.GetComponent<CanvasGroup>().alpha += 0.05f;


            yield return new WaitForSecondsRealtime(0.0001f * i);
        }
        for (int i = 0; i < 5; i++)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 1.2f, transform.position.z);
            transform.localScale = new Vector3(transform.localScale.x + 0.005f, transform.localScale.y + 0.005f, 0);

            yield return new WaitForSecondsRealtime(0.001f * i);
        }

    }

}


