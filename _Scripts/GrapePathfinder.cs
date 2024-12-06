using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;
using static PathfinderCube;

public class GrapePathfinder : MonoBehaviour {


    public event EventHandler<CollidedObjectsEventArgs> OnPathfindingOperationDone;
    public class CollidedObjectsEventArgs : EventArgs {
        public List<GameObject> collidedObjects;
        public List<CollectibleGrape> collidedGrapes;

    }

    // Start is called before the first frame update
    [SerializeField] private Vector3 pathfinderMovementVector = new Vector3(0, 0, -1);

    [SerializeField] private Vector3 facingDirection;
    [SerializeField] private float pathfinderMovespeed;
    [SerializeField] private PathfinderCube pathfinderCube;
    [SerializeField] private int minimumGrapeObjectCount = 5;
    private float patfinderCompletionTimerMax = 1f;
    [SerializeField] float patfinderCompletionTimer;
    [SerializeField] private bool patfindingOperationEnabled = false;
    [SerializeField] private bool patfindingOperationComplete = false;

    [SerializeField] private float patfindingRestartTimerMax = 1f;
    [SerializeField] private float patfindingRestartTimer = 1f;
    [SerializeField] private bool pathfindingEnded = false;

    private Vector3 vectorForward = new Vector3(0, 0, -1);
    private Vector3 vectorRight = new Vector3(-1, 0, 0);
    private Vector3 vectorLeft = new Vector3(1, 0, 0);
    private Vector3 vectorBackward = new Vector3(0, 0, 1);


    private bool pathfindingReseted = false;
    private bool pathfindingResetTimerStarted = false;
    public bool patfindingOperationSuccessful = false;


    public List<GameObject> collidedObjectList;
    public List<CollectibleGrape> collidedGrapeList;



    private bool invokedEvent = false;
    private void Update() {
        if (!pathfindingEnded) {
            MovePathfinder();
            CheckIfPathfindingSuccessful();
            AutoCheckAndStartPathfinding();
        }
        if (Math.Abs(pathfinderCube.transform.position.x) > 100 || Math.Abs(pathfinderCube.transform.position.z) > 100) {
            ResetPathfinder();
        }

    }

    private void CheckIfPathfindingSuccessful() {
        if (collidedObjectList.Count >= minimumGrapeObjectCount && patfindingOperationComplete && !invokedEvent) {
            if (patfindingOperationSuccessful) {
                print("invoked GrapePathfinder_OnPathfindingOperationDone event");
                OnPathfindingOperationDone?.Invoke(this, new CollidedObjectsEventArgs { collidedObjects = collidedObjectList, collidedGrapes = collidedGrapeList });
                invokedEvent = true;
                pathfindingResetTimerStarted = true;
            }
            else {
                pathfindingResetTimerStarted = true;

            }

        }
    }

    private void Start() {
        patfinderCompletionTimer = patfinderCompletionTimerMax;
        pathfinderCube.OnObjectAddedToList += PathfinderCube_OnObjectAddedToList;

    }

    private void PathfinderCube_OnObjectAddedToList(object sender, EventArgs e) {
        ResetPathfinderTimer();
    }

    private void MovePathfinder() {

        if (patfindingOperationEnabled) {
            if (patfinderCompletionTimer >= 0) {
                patfinderCompletionTimer -= Time.fixedDeltaTime;

            }
            else if (patfinderCompletionTimer < 0 && !patfindingOperationComplete) {
                patfindingOperationComplete = true;
                //ResetPathfinder();

            }
            pathfinderCube.transform.localPosition += pathfinderMovementVector * pathfinderMovespeed * Time.fixedDeltaTime;
        }

    }

    public void SetPathfinderForward() {
        pathfinderMovementVector = vectorForward;
    }

    public void SetPathfinderBackward() {
        pathfinderMovementVector = vectorBackward;

    }

    public void SetPathfinderRight() {
        pathfinderMovementVector = vectorRight;

    }

    public void SetPathfinderLeft() {
        pathfinderMovementVector = vectorLeft;

    }

    public void AddObjectToList(GameObject gameobject) {
        collidedObjectList.Add(gameobject);
    }
    public void AddGrapeToList(CollectibleGrape collectibleGrape) {
        collidedGrapeList.Add(collectibleGrape);
    }
    public void ResetPathfinderTimer() {
        patfinderCompletionTimer = patfinderCompletionTimerMax;
    }

    private void AutoCheckAndStartPathfinding() {
        if (pathfindingResetTimerStarted) {
            if (patfindingRestartTimer >= 0) {
                patfindingRestartTimer -= Time.deltaTime;

            }
            else if (patfindingRestartTimer < 0 || patfinderCompletionTimer <= 0) {
                ResetPathfinder();
                StartPathfindingOperation();
                patfindingRestartTimer = patfindingRestartTimerMax;
                pathfindingReseted = false;
                pathfindingResetTimerStarted = false;

            }
        }
    }

    public void ResetPathfinder() {
        collidedObjectList.Clear();
        collidedGrapeList.Clear();

        patfindingOperationComplete = false;
        ResetPathfinderTimer();
        invokedEvent = false;
        pathfinderCube.gameObject.transform.localPosition = Vector3.zero;
        pathfindingReseted = true;
    }

    public void StartPathfindingOperation() {
        patfindingOperationEnabled = true;

        pathfinderCube.gameObject.SetActive(true);

    }

    public void DisablePathfindingOperation() {
        //pathfindingEnded = true;
        //ResetPathfinder();
        //gameObject.SetActive(false);

    }
}
