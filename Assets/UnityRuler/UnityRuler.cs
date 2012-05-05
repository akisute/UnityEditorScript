using UnityEngine;

public sealed class UnityRuler : MonoBehaviour {
	
	public Color color = Color.red;
	public float markerRadius = 1.0f;
	public GUIStyle markerStyle;
	
	void OnDrawGizmos() {
		Color mainColor = color;
		Color lineColor = new Color(color.r, color.g, color.b, 0.4f);
		RaycastHit hitInfo;
		
		Vector3 origin = transform.position;
		Vector3 normal = Vector3.up;
		if (Physics.Raycast(transform.position, Vector3.down, out hitInfo)) {
			origin = hitInfo.point;
			normal = hitInfo.normal;
		}
		
		Gizmos.color = mainColor;
		Gizmos.DrawSphere(origin, markerRadius);
		Gizmos.DrawLine(origin, origin+(normal*markerRadius*3.0f));
		Gizmos.color = lineColor;
		Gizmos.DrawLine(transform.position, origin);
		
		foreach (Transform t in transform) {
			Vector3 o = t.position;
			Vector3 n = Vector3.up;
			if (Physics.Raycast(t.position, Vector3.down, out hitInfo)) {
				o = hitInfo.point;
				n = hitInfo.normal;
			}
			
			Gizmos.color = mainColor;
			Gizmos.DrawSphere(o, markerRadius/2);
			Gizmos.DrawLine(o, o+(n*markerRadius*1.5f));
			Gizmos.color = lineColor;
			Gizmos.DrawLine(t.position, o);
		}
	}
}
