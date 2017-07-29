using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction {
    UP = 0,
    DOWN = 1,
    RIGTH = 2,
    LEFT = 3
}

public class Card : MonoBehaviour {

    [SerializeField]
    private Direction _Direction;

    public Direction GetDirection() { return _Direction; }

    public void SetDirection(Direction dir) {
        _Direction = dir;
    }

}
