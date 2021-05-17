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
        originalY = title.localPosition.y;
        StartCoroutine(AnimateMenu());
    }

    private void Update()
    {
        title.localPosition = new Vector3(title.localPosition.x, originalY + ((float)Mathf.Sin(Time.time) * floatStrength), title.localPosition.z);
    }

    public void Options()
    {
        GameSceneManager.askOptions = true;
    }

    public void Play()
    {
        GameSceneManager.nextLevel = true;
    }

    public void Exit()
    {
        Debug.Log("asda");
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



