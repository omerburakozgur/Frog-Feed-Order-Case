using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class CellGenerator : MonoBehaviour {


    [SerializeField] private Vector2 cellGridPosition;
    [SerializeField] private GameObject debugObject;
    [SerializeField] private GameObject defaultCell;
    [SerializeField] private GameObject[] cellSpawnQueue;
    private float cellSpawnOffset = 0.15f;
    private bool firstCellsAreSpawned = false;
    private bool removedTopItem = false;

    private List<ICellObject> spawnedCellList;
    // Start is called before the first frame update
    void Start() {

        spawnedCellList = new List<ICellObject>();
        //print("Spawned Cell Array Length: " + spawnedCellArray.Length);
        //print("Cell Spawn Queue: " + cellSpawnQueue.Length);

        GenerateStartingCells();
        debugObject.SetActive(false);

        if (cellSpawnQueue.Length == 0) {
            GenerateEmptyCell();

        }

    }

    // Update is called once per frames
    void Update() {

    }

    // remove the top most cell
    public void RemoveTopMostCell() {

        if (!spawnedCellList.Count.Equals(0)) {
            // check if the top most cell is a grape or not
            if (spawnedCellList.First().GetParentGameobject().tag.Equals(TagStrings.GRAPE_CELL)) {
                // decrease grape counter
                GameManager.Instance.DecreaseGrapeCount();
                print($"Grape Destroyed, total grape count: {GameManager.Instance.GetGrapeCount()}");
            }
            // remove the child object of the cell (grape, frog etc)
            spawnedCellList.First().RemoveObject();
            // destroy the cell itself
            spawnedCellList.First().DestroyCell();

            StartCoroutine(MoveNextCellUpwards());
        }

    }

    IEnumerator MoveNextCellUpwards() {
        // check if the list is empty or not
        if (spawnedCellList.Count > 1) {

            yield return new WaitForSeconds(0.5f); // magic number

            // there is still more cells underneath
            // get the next cell
            GameObject cell = spawnedCellList[spawnedCellList.IndexOf(spawnedCellList.First()) + 1].GetParentGameobject();
            // set the local position of the cell to match it with others
            cell.transform.localPosition += new Vector3(0, cellSpawnOffset, 0);
            // enable the child object of the cell
            spawnedCellList[spawnedCellList.IndexOf(spawnedCellList.First()) + 1].EnableObject();


        }
        else {
            yield return new WaitForSeconds(0.5f); // magic number
            GenerateEmptyCell();
            //StartCoroutine(GenerateEmptyCell());

            // there are no cells waiting
            // generate an empty cell

        }
    }


    // generates the starting cells 
    private void GenerateStartingCells() {
        // set spawn offset of the cell
        float spawnOffset = -cellSpawnOffset;

        // foreach loop to spawn all waiting cells
        foreach (GameObject cell in cellSpawnQueue) {
            // spawn the cell and set its parent as this cell generator
            GameObject spawnedCell = Instantiate(cell, transform);
            // check if the cell is a grape cell or not
            if (spawnedCell.tag.Equals(TagStrings.GRAPE_CELL)) {
                // Increase grape counter
                GameManager.Instance.IncreaseGrapeCount();
                //print($"Grape Spawned, total grape count: {GameManager.Instance.GetGrapeCount()}");
            }
            // set the spawned cells local position
            spawnedCell.transform.localPosition = new Vector3(0, spawnOffset, 0);
            // enable the spawned cell
            spawnedCell.SetActive(true);
            // get the ICellObject interface from the spawned cell gameobject
            ICellObject cellObjectReference = spawnedCell.GetComponent<ICellObject>();
            // set the cellObjectReference of the spawned cells child object as this
            cellObjectReference.SetCellGeneratorReference(this);
            // add the interface to spawned cell list
            spawnedCellList.Add(cellObjectReference);
            // modify the spawn offset 
            spawnOffset -= cellSpawnOffset;
            // check if this is the first cell or not
            if (!firstCellsAreSpawned) {
                // this is the first cell to get spawned
                // enable the child object of the spawned cell
                cellObjectReference.EnableObject();
                // set the boolean to prevent other cells from enabling their objects
                firstCellsAreSpawned = true;
            }
        }

    }

    // generate empty cell visual
    private void GenerateEmptyCell() {
        //yield return new WaitForSeconds(0.5f); // magic number
        // spawn the empty cell visual
        GameObject spawnedCell = Instantiate(defaultCell, transform);
        // set the local position of the cell to match with others
        spawnedCell.transform.localPosition = new Vector3(0, -cellSpawnOffset, 0);
        //spawnedCell.transform.localPosition = new Vector3(0, -cellSpawnOffset, 0);
        // activate the cell
        spawnedCell.SetActive(true);


    }

    // reset the spawned gameobjects local position to zero
    private void ResetObjectTransform(GameObject gameobject) {
        gameobject.transform.localPosition = Vector3.zero;
    }

    public Vector2 GetCellGridPosition() {
        return cellGridPosition;

    }
}
