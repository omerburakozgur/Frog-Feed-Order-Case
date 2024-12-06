using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class SplineTest : MonoBehaviour {


    [Header("FrogObjectReference")]
    [SerializeField] private FrogCellManager frogCellManager;
    [SerializeField] private GrapePathfinder grapePathfinder;

    [Header("Spline Arrays and Lists")]
    public Transform splineTipPoint;
    public List<Transform> targetObjectPositions;
    public List<Vector3> knotPositionsList;
    public List<BezierKnot> splineKnotsList;
    public List<CollectibleGrape> grapeList;

    [Header("Spline Generation Variables")]
    [SerializeField] float splineFillSpeed = 0.5f;
    [SerializeField] float reverseEnableTimer = 0.5f;
    [SerializeField] bool splineOperationDisabled = true;

    private Spline spline;
    private SplineExtrude splineExtrude;

    private float splineCurrentRange = 0.0f;
    private bool splineIsFilled = false;
    private bool reverseEnabled = false;
    private bool splineIsCreated = false;
    public bool grapeCollectionSuccessful = false;
    public List<GameObject> collidedObjects;

    void Start() {
        grapePathfinder.OnPathfindingOperationDone += GrapePathfinder_OnPathfindingOperationDone;
        frogCellManager.OnFrogClickEvent += FrogCellManager_OnFrogClickEvent;
        splineExtrude = GetComponent<SplineExtrude>();
        splineExtrude.Rebuild();
    }

    private void ResetSplinePosition() {
        Vector2 cellgridPosition = frogCellManager.GetCellGeneratorReference().GetCellGridPosition();
        transform.position = new Vector3(transform.position.x - cellgridPosition.x, 0, transform.position.z - cellgridPosition.y);
    }

    private void FixedUpdate() {
        if (splineIsCreated) {
            GenerateSpline();
            UpdateSplineTipPoint();
        }

    }

    private void GrapePathfinder_OnPathfindingOperationDone(object sender, GrapePathfinder.CollidedObjectsEventArgs e) {
        if (!grapeCollectionSuccessful) {

            grapeCollectionSuccessful = true;
            collidedObjects = e.collidedObjects;
            grapeList = e.collidedGrapes;
            AddGrapePositionsToList();
            InitializeSplineKnotList();
            SetupSpline();
            AddKnotsToSpline();
            splineExtrude.Rebuild();
            splineIsCreated = true;
            ResetSplinePosition();
            splineExtrude.Rebuild();
            //grapePathfinder.DisablePathfindingOperation();
        }


    }

    private void FrogCellManager_OnFrogClickEvent(object sender, EventArgs e) {
        splineFillSpeed = 5f;
        EnableSplineOperation();
    }

    private void EnableSplineOperation() {
        splineOperationDisabled = false;
    }

    private void AddGrapePositionsToList() {
        // starting position of the spline
        foreach (GameObject gameObject in collidedObjects) {
            targetObjectPositions.Add(gameObject.transform);
        }
    }
    private void InitializeSplineKnotList() {
        // Clear previous data
        knotPositionsList.Clear();
        splineKnotsList.Clear();

        // Add frog's initial position as the starting point
        knotPositionsList.Add(transform.position);
        splineKnotsList.Add(new BezierKnot(transform.position));

        // Add target positions
        foreach (Transform target in targetObjectPositions) {

            knotPositionsList.Add(target.position);
            splineKnotsList.Add(new BezierKnot(target.position));
        }
    }


    private void SetupSpline() {
        spline = GetComponent<SplineContainer>().Spline;
    }

    private void AddKnotsToSpline() {
        foreach (BezierKnot knot in splineKnotsList) {
            spline.Add(knot);
        }
    }

    private void GenerateSpline() {
        if (!splineOperationDisabled) {
            float splineLength = spline.GetLength(); // Total length of the spline
            float speedFactor = splineLength > 0 ? splineFillSpeed / splineLength : 1f;

            if (splineIsFilled && reverseEnabled) {
                splineCurrentRange -= speedFactor * Time.fixedDeltaTime;
            }
            else if (!splineIsFilled) {
                splineCurrentRange += speedFactor * Time.fixedDeltaTime;
            }

            if (splineCurrentRange >= 1f) {
                splineIsFilled = true;
                reverseEnableTimer -= Time.fixedDeltaTime;
                if (reverseEnableTimer <= 0f) {
                    reverseEnabled = true;
                }
            }
            else if (splineCurrentRange <= 0) {
                print(grapeCollectionSuccessful + "grapeCollectionSuccessful");
                if (grapeCollectionSuccessful) {
                    frogCellManager.GetCellGeneratorReference().RemoveTopMostCell();
                }
                splineOperationDisabled = true;
                Debug.Log("Spline operation is done");
            }

            splineExtrude.Range = new Vector2(0f, Mathf.Clamp(splineCurrentRange, 0f, 1f));
            splineExtrude.Rebuild();
        }
    }


    private void UpdateSplineTipPoint() {
        if (spline != null && splineCurrentRange >= 0f && splineCurrentRange <= 1f) {
            Vector3 evaluatedPosition = spline.EvaluatePosition(splineCurrentRange);
            splineTipPoint.position = evaluatedPosition;
        }
    }


    public bool GetReverseEnabled() {
        return reverseEnabled;
    }
}
