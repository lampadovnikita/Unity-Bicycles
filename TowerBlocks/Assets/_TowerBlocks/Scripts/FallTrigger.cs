using UnityEditor;
using UnityEngine;

// Class to indicate collisions with an block fall trigger
public class FallTrigger : MonoBehaviour
{
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.black;

#if UNITY_EDITOR
		Handles.Label(transform.position, "Fall Trigger");
#endif

		Gizmos.DrawLine(
			transform.position - new Vector3(transform.localScale.x / 2f, 0f, 0f),
			transform.position + new Vector3(transform.localScale.x / 2f, 0f, 0f));
	}
}
