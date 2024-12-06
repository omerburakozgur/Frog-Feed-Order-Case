using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.MaterialProperty;

public class CollectibleGrape : MonoBehaviour {

    public enum ColorType {
        Blue,
        Green,
        Purple,
        Red,
        Yellow
    }

    [SerializeField] GrapeCellManager grapeCellManager;
    [SerializeField] ColorType colorType;
    [SerializeField] private bool splineCollisionEnabled = false;
    [SerializeField] private float collisionEnablerTimer = 0.5f;

    private void FixedUpdate() {
        EnableCollisionWithSpline();
    }

    private void OnTriggerEnter(Collider collider) {


        if (collider.tag.Equals(TagStrings.FROG_TONGUE)) {
            FrogTongue frogTongue = collider.GetComponent<FrogTongue>();
            FrogObject frogObject = frogTongue.GetFrogReference();
            print((int)frogObject.GetColorType());
            print((int)colorType);

            if ((int)frogObject.GetColorType() == ((int)colorType)) {
                print($"{collider.gameObject.name} ate {gameObject.name}, -1 berry count ");
                CellGenerator grapeCellGenerator = grapeCellManager.GetCellGeneratorReference();
                grapeCellGenerator.RemoveTopMostCell();

                // FOR TESTING PURPOSES
                // EXECUTE THIS CODE AFTER THE FROG ATE ALL THE BERRIES
                // Get frog tongue script, access frog manager reference, get frog cell manager, get cell manager
                CellGenerator frogCellGenerator = collider.GetComponent<FrogTongue>().GetFrogReference().GetFrogCellManagerReference().GetCellGeneratorReference();
                // remove the frog cell after it ate berries
                frogCellGenerator.RemoveTopMostCell();

            }
            else {
                print($"{collider.gameObject.name} collided with {gameObject.name} but colors don't match");

            }
        }

    }

    private void OnCollisionEnter(Collision collision) {
        print($"{collision.gameObject.name} collided with {gameObject.name}");

    }

    public GrapeCellManager GetGrapeCellManager() {
        return grapeCellManager;
    }

    public ColorType GetColorType() {
        return colorType;
    }

    private void EnableCollisionWithSpline() {
        if (collisionEnablerTimer >= 0) {
            collisionEnablerTimer -= Time.deltaTime;
        }
        else if (collisionEnablerTimer <= 0 && !splineCollisionEnabled) {
            splineCollisionEnabled = true;
        }
    }

    public bool GetSplineCollisionEnabled() {
        return splineCollisionEnabled;
    }
}
