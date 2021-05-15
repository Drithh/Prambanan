using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuideLine : MonoBehaviour
{
	public static int totalCandi = 0;
	protected CandiArea DropArea;
	protected bool candiDirectionAsk;
	protected bool candiDirectionAskState;

	private bool guideSpawned = false;

	private void Start()
    {
		gameObject.GetComponent<Image>().color = new Color32(255, 255, 225, 0);
		gameObject.GetComponent<Image>().material = Resources.Load("Materials/LightEffect") as Material;
	}

	protected virtual void Awake()
	{
		DropArea = GetComponent<CandiArea>() ?? gameObject.AddComponent<CandiArea>();
		DropArea.OnDropHandler += OnItemDropped;
	}

    private void Update()
    {
		Transform child = transform.parent.GetChild(0);
		if(child.childCount == 0 && !guideSpawned)
        {
			guideSpawned = true;
			StartCoroutine(Waiter());
		}
		else if (child.childCount > 0)
		{
			guideSpawned = false;
		}
	}

    private void OnItemDropped(CandiDrag draggable)
	{
		Transform child = transform.parent.GetChild(0);
		if ((candiDirectionAsk && candiDirectionAskState != draggable.horizontalState) || child.childCount > 0) return;
		
		totalCandi++;
		draggable.transform.position = transform.position;
		Destroy(gameObject);
	}

	IEnumerator Waiter()
	{
		yield return new WaitForSeconds(Random.Range(0f, 0.4f));
		for (float i = 0; i < 1.3f; i += 0.05f)
		{
			if (i == 0) gameObject.GetComponent<Image>().color = new Color32(255, 255, 225, 255);
			gameObject.GetComponent<RectTransform>().localScale = new Vector3(i, i, i);
			yield return new WaitForSeconds(0.01f);

		}
		gameObject.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
	}
}
