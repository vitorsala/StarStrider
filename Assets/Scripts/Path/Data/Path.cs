﻿using UnityEngine;
using System;

public enum BezierControlPointMode {
    Free, Aligned, Mirrored
};

public enum SegmentType {
	Curve, Line
};

public class Path : MonoBehaviour {
	
    [SerializeField] private Vector3[] points;
	[SerializeField] private SegmentType[] types;
    [SerializeField] private BezierControlPointMode[] modes;

    private bool _loop;
    public bool loop {
        get {
            return _loop;
        }
        set {
            _loop = value;
            if (value == true) {
                modes[modes.Length - 1] = modes[0];
                SetControlPoint(0, points[0]);
            }
        }
    }

    public int ControlPointCount {
        get {
            return points.Length;
        }
    }

    public int SegmentCount {
        get {
            return (points.Length - 1) / 3;
        }
    }

    void Reset() {
        points = new Vector3[] {
            new Vector3 (0.1f, 0, 0),
            new Vector3 (0.2f, 0, 0),
            new Vector3 (0.3f, 0, 0),
            new Vector3 (0.4f, 0, 0)
        };
        modes = new BezierControlPointMode[] {
            BezierControlPointMode.Free,
            BezierControlPointMode.Free
        };
		types = new SegmentType[] {
			SegmentType.Curve
		};
    }

    private int GetIndex(ref float t) {
        int i;
        if (t >= 1f) {
            t = 1f;
            i = points.Length - 4;
        }
        else {
            t = Mathf.Clamp01(t) * SegmentCount;
            i = (int)t;
            t -= i;
            i *= 3;
        }
        return i;
    }

    public Vector3 GetPoint(float t) {
        int i = GetIndex(ref t);
		int segi = i / 3;
		if (types[segi] == SegmentType.Curve)
			return transform.TransformPoint(Bezier.GetPoint(t, points[i], points[i + 1], points[i + 2], points[i + 3]));
		else
			return transform.TransformPoint(Vector3.Lerp(points[i], points[i + 3], t));
    }
    
    public Vector3 GetVelocity(float t) {
        int i = GetIndex(ref t);
        return transform.TransformPoint(Bezier.GetDerivative(t, points[i], points[i + 1], points[i + 2], points[i + 3]) - transform.position);
    }

    public Vector3 GetDirection(float t) {
        return GetVelocity(t).normalized;
    }

    public Vector3 GetControlPoint(int index) {
        return points[index];
    }

    public void SetControlPoint(int index, Vector3 point) {
        if (index % 3 == 0) {
            Vector3 delta = point - points[index];
            if (loop) {
                if (index == 0) {
                    points[1] += delta;
                    points[points.Length - 2] += delta;
                    points[points.Length - 1] = point;
                }
                else if (index == points.Length - 1) {
                    points[0] = point;
                    points[1] += delta;
                    points[index - 1] += delta;
                }
                else {
                    points[index - 1] += delta;
                    points[index + 1] += delta;
                }
            }
            else {
				if (index > 0) {
					points[index - 1] += delta;
				}
				else if (index + 1 < points.Length) {
					points[index + 1] += delta;
				}
            }
        }


        if (index == points.Length && GetSegmentType(index) == SegmentType.Line) {
            int nodeSegIni =  index / 3;
            nodeSegIni *= 3;
            points[nodeSegIni + 1] = Vector3.Lerp(points[nodeSegIni], points[nodeSegIni + 3], 0.333f);
            points[nodeSegIni + 2] = Vector3.Lerp(points[nodeSegIni], points[nodeSegIni + 3], 0.666f);
		}

        if(index - 3 >= 0 && GetSegmentType(index - 3) == SegmentType.Line) {

            int nodeSegIni =  (index - 3) / 3;
            nodeSegIni *= 3;
            points[nodeSegIni + 1] = Vector3.Lerp(points[nodeSegIni], points[nodeSegIni + 3], 0.333f);
            points[nodeSegIni + 2] = Vector3.Lerp(points[nodeSegIni], points[nodeSegIni + 3], 0.666f);
        }

        points[index] = point;
        EnforceMode(index);
    }

    public BezierControlPointMode GetControlPointMode(int index) {
        return modes[(index + 1) / 3];
    }

    public void SetControlPointMode(int index, BezierControlPointMode mode) {
        int modeIndex = (index + 1) / 3;
        modes[modeIndex] = mode;
        if (loop) {
            if(modeIndex == 0) {
                modes[modes.Length - 1] = mode;
            }
            else if(modeIndex == modes.Length - 1) {
                modes[0] = mode;
            }
        }
        EnforceMode(index);
    }

	public void SetSegmentType(int index, SegmentType type){
		int segmentIndex = index / 3;
		int nodeIndex =  segmentIndex * 3;
		if (segmentIndex >= SegmentCount) {
			segmentIndex--;
		}

		points[nodeIndex + 1] = Vector3.Lerp(points[nodeIndex], points[nodeIndex + 3], 0.333f);
		points[nodeIndex + 2] = Vector3.Lerp(points[nodeIndex], points[nodeIndex + 3], 0.666f);

		types[segmentIndex] = type;
	}

	public SegmentType GetSegmentType(int index){
		int segmentIndex = index / 3;
		if (segmentIndex >= SegmentCount) {
			segmentIndex--;
		}
		return types[segmentIndex];
	}

    public void AddSegment() {
        Vector3 point = points[points.Length - 1];
        Array.Resize(ref points, points.Length + 3);
        point.x += 0.1f;
        points[points.Length - 3] = point;
        point.x += 0.2f;
        points[points.Length - 2] = point;
        point.x += 0.3f;
        points[points.Length - 1] = point;

        Array.Resize(ref modes, modes.Length + 1);
        modes[modes.Length - 1] = modes[modes.Length - 2];
        EnforceMode(points.Length - 4);

		Array.Resize(ref types, types.Length + 1);
		types[types.Length - 1] = types[types.Length - 2];


        if (loop) {
            points[points.Length - 1] = points[0];
            modes[modes.Length - 1] = modes[0];
            EnforceMode(0);
        }
    }

    public void RemoveSegment() {
        if (SegmentCount > 1) {

            Array.Resize(ref points, points.Length - 3);
            Array.Resize(ref modes, modes.Length - 1);

            if (loop) {
                points[points.Length - 1] = points[0];
                modes[modes.Length - 1] = modes[0];
                EnforceMode(0);
            }
        }
    }

    private void EnforceMode(int index) {

        int modeIndex = (index + 1) / 3;
        BezierControlPointMode mode = modes[modeIndex];

        if (mode == BezierControlPointMode.Free || !loop && (modeIndex == 0 || modeIndex == modes.Length - 1)) {
            return;
        }

        int middleIndex = modeIndex * 3;
        int fixedIndex, enforcedIndex;

        if (index <= middleIndex) {
            fixedIndex = middleIndex - 1;
            if(fixedIndex < 0) {
                fixedIndex = points.Length - 2;
            }
            enforcedIndex = middleIndex + 1;
            if(enforcedIndex >= points.Length) {
                enforcedIndex = 1;
            }
        }
        else {
            fixedIndex = middleIndex + 1;
            if(fixedIndex >= points.Length) {
                fixedIndex = 1;
            }
            enforcedIndex = middleIndex - 1;
            if(enforcedIndex < 0) {
                enforcedIndex = points.Length - 2;
            }
        }

        Vector3 middle = points[middleIndex];
        Vector3 enforcedTangent = middle - points[fixedIndex];
        if (mode == BezierControlPointMode.Aligned) {
            enforcedTangent = enforcedTangent.normalized * Vector3.Distance(middle, points[enforcedIndex]);
        }
        points[enforcedIndex] = middle + enforcedTangent;
    }
}
