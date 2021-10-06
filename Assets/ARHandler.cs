using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARHandler : MonoBehaviour
{

    [SerializeField] GameObject objectToPlace;
    [SerializeField] ARCameraManager arCameraManager;
    [SerializeField] Camera arCamera;
    [SerializeField] Transform arRoot;

    private void OnEnable()
    {
        PlaceOnPlaneLP.onObjectPlaced += OnObjectPlaced;
    }

    private void OnDisable()
    {
        PlaceOnPlaneLP.onObjectPlaced -= OnObjectPlaced;
    }

    void OnObjectPlaced(Pose pose, PlaceOnPlaneLP.TargetDistance targetDistance) {
        GameObject obj = Instantiate(objectToPlace, pose.position, pose.rotation, arRoot);
        obj.transform.localScale = Vector3.one * (targetDistance.isNear ? .1f : 1f);

        Vector3 relativePos = arCamera.transform.position - obj.transform.position;
        Quaternion lookAtRotation = Quaternion.LookRotation(relativePos);
        Quaternion lookAtRotationOnly_Y = Quaternion.Euler(obj.transform.rotation.eulerAngles.x, lookAtRotation.eulerAngles.y, obj.transform.rotation.eulerAngles.z);
        obj.transform.transform.rotation = lookAtRotationOnly_Y;
        obj.SetActive(true);
    }
}
