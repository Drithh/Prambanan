using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    float originalY;
    Transform title;
    private float floatStrength = 7;
    private void Start()
    {
        title = transform.GetChild(0).GetComponent<Transform>();
        originalY = title.position.y;
        StartCoroutine(AnimateMenu());
    }

    private void Update()
    {
        title.position = new Vector3(transform.position.x, originalY + ((float)Mathf.Sin(Time.time) * floatStrength), transform.position.z);
    }

    public void Play()
    {
        GameSceneManager.nextLevel = true;
    }

    public void Exit()
    {
        Application.Quit();
    }



    IEnumerator AnimateMenu()
    {
        yield return new WaitForSecondsRealtime(1f);

        for (int i = 1; i < 4; i++)
        {
            Transform menuButton = transform.GetChild(i).GetComponent<Transform>(); 
            StartCoroutine(AnimateButton(menuButton));
            yield return new WaitForSecondsRealtime(0.5f);
        }
    }

    IEnumerator AnimateButton(Transform transform)
    {
        for(int i = -850; i < 1; i += 10)
        {
            transform.localPosition = new Vector3(i, transform.localPosition.y , 0);
            yield return new WaitForSecondsRealtime(0.01f);
        }
    }
}



