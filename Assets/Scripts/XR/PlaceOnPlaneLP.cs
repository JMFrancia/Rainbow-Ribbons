using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class PlaceOnPlaneLP : MonoBehaviour
{
    [SerializeField] private Transform targetIndicator;
    [SerializeField] private float resetDelaySeconds = 1.0f;
    [SerializeField] private float distanceThreshold = 5f;

    [SerializeField] private GameObject defaultIndicator;
    [SerializeField] private GameObject nearIndicator;

    public static event Action<Pose, TargetDistance> onObjectPlaced;
    public static Status PlacementStatus { get; private set; }

    private ARSessionOrigin sessionOrigin;
    private static ARRaycastManager m_RaycastManager;
    private static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

    void Awake()
    {
        sessionOrigin = GetComponent<ARSessionOrigin>();
        m_RaycastManager = GetComponent<ARRaycastManager>();
    }

    /*
     * Start by activating the targetIndicator obj (IE portals)
     */
    void Start()
    {
        targetIndicator.gameObject.SetActive(false);
    }

    /*
     * On update, ShowTarget(), meaning 
     */
    void Update()
    {
        ShowTarget();
    }

    private void ShowTarget()
    {
        //If AR session running and device able to determine its orientation, send a raycast from center of screen
        if (ARSession.state == ARSessionState.SessionTracking &&
            m_RaycastManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), s_Hits, TrackableType.PlaneWithinPolygon))
        {
            //If we have a hit...
            if (s_Hits.Count > 0)
            {
                //Mark placement status (as able to place)
                if (PlacementStatus == Status.Finding)
                    PlacementStatus = Status.CanPlace;

                //Move target indicator (portals) to most recent position)
                var hitPose = s_Hits[s_Hits.Count - 1].pose;
                targetIndicator.position = hitPose.position;

                //Update the indicator based on whether or not target is in range
                var distanceVector = hitPose.position - sessionOrigin.camera.transform.position;
                var targetDistance = new TargetDistance
                {
                    distanceVector = distanceVector,
                    distanceThreshold = distanceThreshold
                };

                UpdateIndicator(distanceVector);

                //If user is touching a game object while the placement status is "CanPlace"
                if (!IsPointerOverGameObject() && PlacementStatus == Status.CanPlace)
                {
#if !UNITY_EDITOR
                    //If user is touching the screen
                    if (Input.touchCount > 0)
                    {
                        Touch touch = Input.GetTouch(0);
                        Rect topScreen = new Rect(0, Screen.height / 3, Screen.width, Screen.height);

                        //If the touch is on the top third of the screen...
                        if (touch.phase == TouchPhase.Began && topScreen.Contains(Input.GetTouch(0).position))
                        {
                            //Invoke onObjectPlaced event, set status accordingly
                            PlacementStatus = Status.Placed;
                            onObjectPlaced?.Invoke(hitPose, targetDistance);
                        }
                    }
#else
                    if (Input.GetMouseButtonDown(0))
                    {
                        Rect topScreen = new Rect(0, Screen.height / 3, Screen.width, Screen.height);
                        if (topScreen.Contains(Input.mousePosition))
                        {
                            PlacementStatus = Status.Placed;
                            onObjectPlaced?.Invoke(hitPose, targetDistance);
                        }
                    }
#endif
                }
            }
        }

        //Turn off target indicator if placement status is no longer canPlace
        targetIndicator.gameObject.SetActive(PlacementStatus == Status.CanPlace);
    }

    private void UpdateIndicator(Vector3 distanceVector)
    {
        var sqDistance = distanceVector.sqrMagnitude;
        var isNear = sqDistance < (distanceThreshold * distanceThreshold);

        defaultIndicator.SetActive(!isNear);
        nearIndicator.SetActive(isNear);
    }

    public void ResetTracking()
    {
        StartCoroutine(ResetTrackingAfterDelay(resetDelaySeconds));
    }

    private IEnumerator ResetTrackingAfterDelay(float seconds)
    {
        float timer = 0;
        while (timer < seconds)
        {
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        PlacementStatus = Status.Finding;

        yield return null;
    }

    private bool IsPointerOverGameObject()
    {
        // Check mouse
        if (EventSystem.current.IsPointerOverGameObject())
            return true;

        // Check touches
        for (int i = 0; i < Input.touchCount; i++)
        {
            var touch = Input.GetTouch(i);
            if (touch.phase == TouchPhase.Began)
                if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                    return true;
        }
        return false;
    }

    public enum Status
    {
        Finding,
        CanPlace,
        Placed
    }

    public struct TargetDistance
    {
        public Vector3 distanceVector;
        public float distanceThreshold;

        public bool isNear => distanceVector.sqrMagnitude < (distanceThreshold * distanceThreshold);
    }
}