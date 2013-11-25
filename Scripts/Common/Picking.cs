using UnityEngine;
using System.Collections;

public class Picking : Object
{
	public static GameObject GetTouchedObject(Vector2 touchPos, string layer)
	{
		Ray ray = Camera.mainCamera.ScreenPointToRay(touchPos);

		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, 1 << LayerMask.NameToLayer(layer)))
		{
			return hit.collider.gameObject;
		}

		return null;
	}

	public static GameObject GetTouchedObject(Vector2 touchPos)
	{
		return GetTouchedObject(touchPos, "");
	}
}

