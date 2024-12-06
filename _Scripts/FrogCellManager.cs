using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FrogCellManager : MonoBehaviour, ICellObject {

    [SerializeField] private FrogObject frogReference;
    [SerializeField] private CellGenerator cellGeneratorReference;
    public event EventHandler OnFrogClickEvent;
    // THIS VECTOR IS X AND Z, NOT X AND Y
    [SerializeField] private Vector2 frogFacingDirection;

    

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    


    private void OnMouseDown() {
        FrogInteract();
    }

    public void FrogInteract() {
        print("Frog Interact");
        OnFrogClickEvent.Invoke(this, EventArgs.Empty);
        GameManager.Instance.DecreaseMoveCount();
    }

    public void EnableObject() {
        frogReference.gameObject.SetActive(true);

    }

    public void GetObject() {
        throw new System.NotImplementedException();
    }

    public GameObject GetParentGameobject() {
        return gameObject;
    }

    public void DestroyCell() {
        Destroy(gameObject);
    }

    public void RemoveObject() {
        Destroy(frogReference);
    }

    public void SetCellGeneratorReference(CellGenerator cellGenerator) {
        cellGeneratorReference = cellGenerator;
    }

    public CellGenerator GetCellGeneratorReference() {
        return cellGeneratorReference;
    }
}
