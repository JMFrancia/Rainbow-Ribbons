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

    GameObject spawnedObj;

    private void OnEnable()
    {
        PlaceOnPlaneLP.onObjectPlaced += OnObjectPlaced;
    }

    private void OnDisable()
    {
        PlaceOnPlaneLP.onObjectPlaced -= OnObjectPlaced;
    }

    void OnObjectPlaced(Pose pose, PlaceOnPlaneLP.TargetDistance targetDistance) {
        objectToPlace.transform.position = pose.position;
        objectToPlace.transform.rotation = pose.rotation;

        objectToPlace.transform.localScale = Vector3.one * (targetDistance.isNear ? .01f : .1f);

        RotateObjectTowardCam(objectToPlace, arCamera);
        objectToPlace.SetActive(true);
    }

    private void Start()
    {
        objectToPlace.SetActive(false);
    }

    private void Update()
    {
        if (objectToPlace.activeInHierarchy) {
            RotateObjectTowardCam(objectToPlace, arCamera);
        }
    }

    void RotateObjectTowardCam(GameObject obj, Camera cam) {
        obj.transform.LookAt(new Vector3(cam.transform.position.x, obj.transform.position.y, cam.transform.position.z));
    }
}
