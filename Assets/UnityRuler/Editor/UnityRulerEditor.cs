using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

[CustomEditor(typeof(UnityRuler))]
public sealed class UnityRulerEditor : Editor {
	
	void OnEnable() {
	}
	
	public override void OnInspectorGUI() {
		UnityRuler rulerComponent = target as UnityRuler;
		
		DrawDefaultInspector();
		EditorGUILayout.Space();
		EditorGUILayout.Space();
		EditorGUILayout.Space();
		if (GUILayout.Button("Add a new ruler point")) {
			GameObject rulerPoint = new GameObject("UnityRulerPoint");
			rulerPoint.transform.parent = rulerComponent.gameObject.transform;
			rulerPoint.transform.localPosition = Vector3.zero;
			rulerPoint.transform.localRotation = Quaternion.identity;
			Selection.activeGameObject = rulerPoint;
		}
		EditorGUILayout.Space();
		if (GUILayout.Button("Clear all ruler points")) {
			List<Transform> buffer = new List<Transform>();
			foreach (Transform t in rulerComponent.transform) {
				buffer.Add(t);
			}
			buffer.ForEach(t => DestroyImmediate(t.gameObject));
		}
	}
	
	void OnSceneGUI() {
		UnityRuler rulerComponent = target as UnityRuler;
		RaycastHit hitInfo;
		
		Vector3 origin = rulerComponent.transform.position;
		Vector3 normal = Vector3.up;
		if (Physics.Raycast(rulerComponent.transform.position, Vector3.down, out hitInfo)) {
			origin = hitInfo.point;
			normal = hitInfo.normal;
		}
		
		Vector3 labelPosition = rulerComponent.transform.position + Vector3.up*rulerComponent.markerRadius*2.0f;
		string labelString = string.Format("Origin\n{0:0.0}*", Vector3.Angle(Vector3.up, normal));
		Handles.Label(labelPosition, labelString, rulerComponent.markerStyle);
		
		foreach (Transform t in rulerComponent.transform) {
			Vector3 o = t.position;
			Vector3 n = Vector3.up;
			if (Physics.Raycast(t.position, Vector3.down, out hitInfo)) {
				o = hitInfo.point;
				n = hitInfo.normal;
			}
			
			float horizontialDistance = Vector3.Distance(new Vector3(origin.x, 0, origin.z), new Vector3(o.x, 0, o.z));
			float verticalDistance = o.y - origin.y;
			
			Vector3 labelP = t.position + Vector3.up*rulerComponent.markerRadius*1.0f;
			string labelS = string.Format("{0:0.0}u\n{1}{2:0.00}\n{3:0.0}*", horizontialDistance, ((verticalDistance>0)?"+":""), verticalDistance, Vector3.Angle(Vector3.up, n));
			Handles.Label(labelP, labelS, rulerComponent.markerStyle);
		}
	}
}
