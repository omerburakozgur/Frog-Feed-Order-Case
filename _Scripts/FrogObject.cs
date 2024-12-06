using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogObject : MonoBehaviour {

    [SerializeField] private FrogCellManager frogCellManagerReference;

    public enum ColorType {
        Blue,
        Green,
        Purple,
        Red,
        Yellow
    }

    [SerializeField] ColorType colorType;
    // Start is called before the first frame update
    void Start() {
        frogCellManagerReference.OnFrogClickEvent += FrogCellManagerReference_OnFrogClickEvent;
    }

    private void FrogCellManagerReference_OnFrogClickEvent(object sender, System.EventArgs e) {
        FrogInteract();
    }

    // Update is called once per frame
    void Update() {

    }

    public void FrogInteract() {
        print("Frog Interact");
    }

    public ColorType GetColorType() {
        return colorType;
    }

    public FrogCellManager GetFrogCellManagerReference() {
        return frogCellManagerReference;
    }
}
