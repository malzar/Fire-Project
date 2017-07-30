using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    private BoardControler _Board;
    [SerializeField]
    private int _PlayerNumber;

    void Awake() {
        _PlayerNumber = 0;
    }

    // Update is called once per frame
    void Update () {

        if (Input.GetKeyDown(KeyCode.W))
            _Board.MovePj(Direction.UP, _PlayerNumber);

        if (Input.GetKeyDown(KeyCode.S))
            _Board.MovePj(Direction.DOWN, _PlayerNumber);

        if (Input.GetKeyDown(KeyCode.A))
            _Board.MovePj(Direction.LEFT, _PlayerNumber);

        if (Input.GetKeyDown(KeyCode.D))
            _Board.MovePj(Direction.RIGTH, _PlayerNumber);

        if (Input.GetKeyDown(KeyCode.Keypad8))
            _Board.PutCard( Direction.UP, _PlayerNumber);

        if (Input.GetKeyDown(KeyCode.Keypad2))
            _Board.PutCard(Direction.DOWN, _PlayerNumber);

        if (Input.GetKeyDown(KeyCode.Keypad4))
            _Board.PutCard(Direction.LEFT, _PlayerNumber);

        if (Input.GetKeyDown(KeyCode.Keypad6))
            _Board.PutCard(Direction.RIGTH, _PlayerNumber);

    }
}
