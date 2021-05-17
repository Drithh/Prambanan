using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public static class ExtensionMethods
{
	public static Transform[] FindChildren(this Transform transform, string name)
	{
		return transform.GetComponentsInChildren<Transform>().Where(t => t.name == name).ToArray();
	}
}



public class GuideLine : MonoBehaviour
{
	public static int totalCandi = 0;
	protected CandiArea DropArea;
	protected bool candiDirectionAsk;
	protected bool candiDirectionAskState;

	private CandiDrag candiItem;

	private bool guideSpawned = false;

	private void Start()
    {
		gameObject.GetComponent<Image>().color = new Color32(255, 255, 225, 0);
		gameObject.GetComponent<Image>().material = Resources.Load("Material/LightEffect") as Material;
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
		candiItem = draggable;
		draggable.transform.position = transform.position;

		GameObject[] parents = GameObject.FindGameObjectsWithTag("Blueprint1");
		foreach (GameObject oneParent in parents)
        {
			FindAllChildsName(oneParent.transform);
        }


		Destroy(gameObject);
	}

	private void FindAllChildsName(Transform parent)
	{
		foreach (Transform child in parent)
		{
			if (child.gameObject.name.Contains(transform.name))
			{
                candiItem.transform.SetParent(parent);
				candiItem.GetComponent<CandiDrag>().enabled = false;
				break;
			}
			FindAllChildsName(child);
		}
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


