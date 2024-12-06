using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfinderCubeOld : MonoBehaviour {

    [SerializeField] private FrogObject frogObjectReference;
    [SerializeField] private GrapePathfinder grapePathfinderReference;
    public enum FacingDirectionEnum {
        Front,
        Left,
        Right
    }
    public FacingDirectionEnum facingDirectionEnum;

    private void OnTriggerEnter(Collider other) {
        // get color type of the frog with a method
        if (other.tag.Equals(TagStrings.GRAPE)) {
            CollectibleGrape grape = other.GetComponent<CollectibleGrape>();

            if ((int)frogObjectReference.GetColorType() == ((int)grape.GetColorType())) {
                if (!grapePathfinderReference.collidedObjectList.Contains(grape.gameObject)) {
                    //ManageMovementAndRotation();

                }
                else {
                    print("This grape was already collided with");

                }
            }
            else {
                print("Cannot find a good route");
            }
        }

    }

    //private void ManageMovementAndRotation() {
    //    switch (facingDirectionEnum) {
    //        case FacingDirectionEnum.Front:
    //            // pathfinder moving forward and collided with the same type
    //            // of grape, continue moving forward
    //            grapePathfinderReference.MovePathfinderForward();
    //            print("Moved Pathfinder Forward");
    //            break;
    //        case FacingDirectionEnum.Left:
    //            // pathfinder was moving and collided with the same type
    //            // of grape on the left, turn the pathfinder to the left
    //            grapePathfinderReference.TurnPathfinderLeft();
    //            print("Rotated and Moved Pathfinder Left");
    //            break;
    //        case FacingDirectionEnum.Right:
    //            // pathfinder was moving and collided with the same type
    //            // of grape on the right, turn the pathfinder to the right
    //            grapePathfinderReference.TurnPathfinderRight();
    //            print("Rotated and Moved Pathfinder Right");
    //            break;

    //    }

    //}

}
