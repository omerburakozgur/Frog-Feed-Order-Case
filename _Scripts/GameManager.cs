using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    // create a counter
    // increase the counter on on every berry cell spawn
    // or search for every berry periodially and create the logic around it
    // Start is called before the first frame update

    public static GameManager Instance;

    public event EventHandler OnMoveCountChange;

    [SerializeField] private int totalGrapeCount;
    [SerializeField] private int moveCount;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        OnMoveCountChange?.Invoke(this, EventArgs.Empty);

    }

    public int GetGrapeCount() {
        return totalGrapeCount;

    }

    public void IncreaseGrapeCount() {
        totalGrapeCount++;
    }

    public void DecreaseGrapeCount() {
        totalGrapeCount--;
        CheckGrapeCounter();

    }

    public void PrintTotalGrapeCount() {
        print($"Total Grape Count: {totalGrapeCount}");
    }

    private void CheckGrapeCounter() {
        if (totalGrapeCount <= 0) {
            print("All grapes are eaten, game over!");
        }
    }

    public void DecreaseMoveCount() {
        moveCount--;
        CheckMoveCounter();
        OnMoveCountChange?.Invoke(this, EventArgs.Empty);

    }

    private void CheckMoveCounter() {
        if (moveCount <= 0) {
            print("No moves left, game over!");
        }
    }

    public int GetMoveCount() {
        return moveCount;
    }
}
