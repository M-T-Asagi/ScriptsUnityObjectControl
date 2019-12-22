using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickingMainCamera : MonoBehaviour {

    [SerializeField]
    Transform parent;

    [SerializeField]
    bool basedParent = false;

    Transform thisTransform;

    Vector3 defaultPos;
    Quaternion defaultRotation;

	// Use this for initialization
	void Start () {
        thisTransform = this.transform;

        if (parent == null)
            parent = Camera.main.transform;

        defaultPos = (basedParent) ? thisTransform.position -  parent.position : thisTransform.position;
        defaultRotation = (basedParent) ? thisTransform.rotation * Quaternion.Inverse(parent.rotation) : thisTransform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
        thisTransform.position = parent.position + defaultPos;
        thisTransform.rotation = parent.rotation * defaultRotation;
	}
}
