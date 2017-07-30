using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerData: MonoBehaviour{

    private int _ActualX;
    private int _ActualY;
    private int _MaxX;
    private int _MaxY;
    private int _Score;
    private List<Cell> _CUp;
    private List<Cell> _CDown;
    private List<Cell> _CLeft;
    private List<Cell> _CRight;
    [SerializeField]
    private Text _ScoreText;
    [SerializeField]
    private GameObject _LeftCard;
    [SerializeField]
    private GameObject _RigtCard;
    [SerializeField]
    private GameObject _UpCard;
    [SerializeField]
    private GameObject _DownCard;

    public void Inicializate(int maxX, int maxY) {
        _ActualX = 0;
        _ActualY = 0;
        _MaxX    = maxX;
        _MaxY    = maxY;
        _Score   = 0;
        _CUp     = new List<Cell>();
        _CDown   = new List<Cell>();
        _CLeft   = new List<Cell>();
        _CRight  = new List<Cell>();
    }

    public int GetXPosition() {
        return _ActualX;
    }

    public int GetYPosition() {
        return _ActualY;
    }

    public void IncrementScore() {
        _Score++;
        _ScoreText.text = _Score.ToString();
    }

    public GameObject PutLeftCard(Cell cellToAdd) {
        _CLeft.Add(cellToAdd);
        return Instantiate(_LeftCard);
    }
    public GameObject PutRightCard(Cell cellToAdd) {
        _CRight.Add(cellToAdd);
        return Instantiate(_RigtCard);
    }
    public GameObject PutUpCard(Cell cellToAdd) {
        _CUp.Add(cellToAdd);
        return Instantiate(_UpCard);
    }
    public GameObject PutDownCard(Cell cellToAdd) {
        _CDown.Add(cellToAdd);
        return Instantiate(_DownCard);
    }

    public bool MovePj(Direction dir) {
        bool canMoveIt = false;
        switch (dir) {
            case Direction.UP:
                if (_ActualX > 0) {
                    _ActualX--;
                    canMoveIt = true;
                }
                break;
            case Direction.DOWN:
                if (_ActualX < _MaxX) {
                    _ActualX++;
                    canMoveIt = true;
                }
                break;
            case Direction.LEFT:
                if (_ActualY > 0) {
                    _ActualY--;
                    canMoveIt = true;
                }
                break;
            case Direction.RIGTH:
                if (_ActualY < _MaxY) {
                    _ActualY++;
                    canMoveIt = true;
                }
                break;
            default:
                break;
        }
        return canMoveIt;
    }
}
