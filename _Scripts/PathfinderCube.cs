using System;
using System.Collections.Generic;
using UnityEngine;

public class PathfinderCube : MonoBehaviour {

    [SerializeField] private FrogObject frogObjectReference;
    [SerializeField] private GrapePathfinder grapePathfinderReference;
    public event EventHandler OnObjectAddedToList;

    public enum ArrowDirection {
        Forward,
        Backward,
        Left,
        Right
    }

    public ArrowDirection movingDirectionEnum;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag(TagStrings.GRAPE)) {
            HandleGrapeCollision(other);
        }
        else if (other.CompareTag(TagStrings.ARROW)) {
            HandleArrowCollision(other);
        }
    }

    private void HandleGrapeCollision(Collider grapeCollider) {
        var grape = grapeCollider.GetComponent<CollectibleGrape>();

        if (grape == null || (int)frogObjectReference.GetColorType() != (int)grape.GetColorType()) {
            Debug.Log("Collided with an unmatched grape; aborting pathfinding.");
            grapePathfinderReference.patfindingOperationSuccessful = false;
            grapePathfinderReference.AddObjectToList(grape.GetGrapeCellManager().GetCellGeneratorReference().gameObject);
            grapePathfinderReference.AddGrapeToList(grape);
            Debug.Log("Added the last wrongly collided object.");

            OnObjectAddedToList?.Invoke(this, EventArgs.Empty);
            return;
        }

        if (!grapePathfinderReference.collidedObjectList.Contains(grape.gameObject)) {
            grapePathfinderReference.patfindingOperationSuccessful = true;
            grapePathfinderReference.AddObjectToList(grape.GetGrapeCellManager().GetCellGeneratorReference().gameObject);
            grapePathfinderReference.AddGrapeToList(grape);

            OnObjectAddedToList?.Invoke(this, EventArgs.Empty);
        }
        else {
            Debug.Log("This grape was already processed.");
        }
    }

    private void HandleArrowCollision(Collider arrowCollider) {
        var arrow = arrowCollider.GetComponent<ArrowObject>();

        if (arrow == null || (int)frogObjectReference.GetColorType() != (int)arrow.GetColorType()) {
            Debug.Log("Collided with an unmatched arrow; aborting pathfinding.");
            grapePathfinderReference.patfindingOperationSuccessful = false;
            grapePathfinderReference.AddObjectToList(arrow.GetArrowCellManager().GetCellGeneratorReference().gameObject);
            OnObjectAddedToList?.Invoke(this, EventArgs.Empty);
            Debug.Log("Added the last wrongly collided arrow.");
            return;
        }

        if (!grapePathfinderReference.collidedObjectList.Contains(arrow.gameObject)) {
            grapePathfinderReference.patfindingOperationSuccessful = true;
            grapePathfinderReference.AddObjectToList(arrow.GetArrowCellManager().GetCellGeneratorReference().gameObject);
            OnObjectAddedToList?.Invoke(this, EventArgs.Empty);
            Debug.Log("Added the arrow to the pathfinding list.");
            movingDirectionEnum = (ArrowDirection)arrow.GetArrowDirection();
            ManageMovementAndRotation();
        }
        else {
            Debug.Log("This arrow was already processed.");
        }
    }

    private void ManageMovementAndRotation() {
        switch (movingDirectionEnum) {
            case ArrowDirection.Forward:
                grapePathfinderReference.SetPathfinderForward();
                Debug.Log("Pathfinder set to move forward.");
                break;
            case ArrowDirection.Backward:
                grapePathfinderReference.SetPathfinderBackward();
                Debug.Log("Pathfinder set to move backward.");
                break;
            case ArrowDirection.Left:
                grapePathfinderReference.SetPathfinderLeft();
                Debug.Log("Pathfinder set to move left.");
                break;
            case ArrowDirection.Right:
                grapePathfinderReference.SetPathfinderRight();
                Debug.Log("Pathfinder set to move right.");
                break;
        }
    }
}
