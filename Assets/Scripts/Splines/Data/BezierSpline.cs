﻿using UnityEngine;
using System.Collections;
using System;

public class BezierSpline : MonoBehaviour {

	[SerializeField] private Vector3[] points;

	public enum BezierControlPointMode {
		Free, Aligned, Mirrored
	};

	[SerializeField] BezierControlPointMode[] modes;

	public int ControlPointCount {
		get {
			return points.Length;
		}
	}

	public int CurveCount {
		get {
			return (points.Length - 1) / 3;
		}
	}

	void Reset(){
		points = new Vector3[] {
			new Vector3 (1, 0, 0),
			new Vector3 (2, 0, 0),
			new Vector3 (3, 0, 0),
			new Vector3 (4, 0, 0)
		};
		modes = new BezierControlPointMode[] {
			BezierControlPointMode.Free,
			BezierControlPointMode.Free
		};
	}

	public Vector3 GetPoint(float t){
		int i;
		if (t >= 1f) {
			t = 1f;
			i = points.Length - 4;
		}
		else {
			t = Mathf.Clamp01 (t) * CurveCount;
			i = (int)t;
			t -= i;
			i *= 3;
		}
		return transform.TransformPoint (Bezier.GetPoint (t, points [i], points [i + 1], points [i + 2], points [i + 3]));
	}

	public Vector3 GetVelocity(float t){
		int i;
		if (t >= 1f) {
			t = 1f;
			i = points.Length - 4;
		}
		else {
			t = Mathf.Clamp01 (t) * CurveCount;
			i = (int)t;
			t -= i;
			i *= 3;
		}
		return transform.TransformPoint (Bezier.GetDerivative (t, points [i], points [i + 1], points [i + 2], points [i + 3]) - transform.position);
	}

	public Vector3 GetControlPoint (int index){
		return points [index];
	}

	public void SetControlPoint(int index, Vector3 point){
		if (index % 3 == 0) {
			Vector3 delta = point - points [index];
			if (index > 0) {
				points [index - 1] += delta;
			}
			if (index + 1 < points.Length) {
				points [index + 1] += delta;
			}
		}
		points [index] = point;
		EnforceMode (index);
	}

	public BezierControlPointMode GetControlPointMode(int index){
 		return modes [(index + 1) / 3];
	}

	public void SetControlPointMode(int index, BezierControlPointMode mode){
		modes [(index + 1) / 3] = mode;
		EnforceMode (index);
	}

	public void AddCurve(){
		Vector3 point = points [points.Length - 1];
		Array.Resize(ref points, points.Length + 3);
		point.x += 1f;
		points [points.Length - 3] = point;
		point.x += 2f;
		points [points.Length - 2] = point;
		point.x += 3f;
		points [points.Length - 1] = point;

		Array.Resize(ref modes, modes.Length + 1);
		modes [modes.Length - 1] = modes [modes.Length - 2];
		EnforceMode (points.Length - 4);
	}

	public void RemoveCurve(){
		if (CurveCount > 1) {
			Array.Resize (ref points, points.Length - 3);
			Array.Resize (ref modes, modes.Length - 1);
		}
	}

	private void EnforceMode(int index){
		int modeIndex = (index + 1) / 3;
		BezierControlPointMode mode = modes [modeIndex];
		if (mode == BezierControlPointMode.Free || modeIndex == 0 || modeIndex == modes.Length - 1) {
			return;
		}
		int middleIndex = modeIndex * 3;
		int fixedIndex, enforcedIndex;

		if (index <= middleIndex) {
			fixedIndex = middleIndex - 1;
			enforcedIndex = middleIndex + 1;
		}
		else {
			fixedIndex = middleIndex + 1;
			enforcedIndex = middleIndex - 1;
		}

		Vector3 middle = points [middleIndex];
		Vector3 enforcedTangent = middle - points [fixedIndex];
		if (mode == BezierControlPointMode.Aligned) {
			enforcedTangent = enforcedTangent.normalized * Vector3.Distance (middle, points [enforcedIndex]);
		}
		points [enforcedIndex] = middle + enforcedTangent;
	}
}