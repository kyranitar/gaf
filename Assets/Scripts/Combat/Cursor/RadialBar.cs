using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RadialBar : MonoBehaviour {

  public GameObject SegmentPrefab;
  public float SegmentAngle;
  public float Radius;

  private float totalValue;
  public float TotalValue {
    get {
      return totalValue;
    }
    set {
      totalValue = value;
    }
  }

  private float currentValue;
  public float CurrentValue {
    get {
      return currentValue;
    }
    set {
      currentValue = value;
    }
  }

  private float minAngle;
  public float MinAngle {
    set {
      minAngle = value;
    }
  }

  private float maxAngle;
  public float MaxAngle {
    set {
      maxAngle = value;
    }
  }

  private uint segmentCount;
  private uint currentIndex;
  private List<GameObject> segments;

  public void Start() {

    segments = new List<GameObject>();
    segmentCount = (uint) Mathf.Floor((maxAngle - minAngle) / SegmentAngle);

    for (int i=0; i < segmentCount; i++) {
      Quaternion rotation = Quaternion.Euler(transform.rotation.eulerAngles + transform.up * (i * SegmentAngle + minAngle));
      GameObject segment = Instantiate(SegmentPrefab, transform.position, rotation) as GameObject;
      segment.transform.localScale = Vector3.one * Radius;
      segment.transform.parent = transform;
      segments.Add(segment);
    }
  }



  public void Update() {

    uint index = (uint) Mathf.Floor(currentValue / totalValue * segments.Count);
    if (index != currentIndex) {

      for (int i = 0; i < segments.Count; i++) {
        if (i <= index) {
          segments[i].renderer.enabled = true;
        } else {
          segments[i].renderer.enabled = false;
        }
      }

      currentIndex = index;
    }
  }

  public void LateUpdate() {

    transform.eulerAngles = new Vector3(0, 0, 0);
  }

}
