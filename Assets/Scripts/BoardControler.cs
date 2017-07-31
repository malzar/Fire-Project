using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BoardStates {
    WATING_PLAYER=0,
    PUSHING_FINISH_CELL=1,
    GAMING=2,
    END=3
}

public class BoardControler : MonoBehaviour {

    private List<List<Cell>> _Cells;
    private BoardStates _ActualSate;
    private float _Time;
    private bool _SetP1Finish;
    private bool _SetP2Finish;

    [SerializeField]
    private float _SpawnTime;
    [SerializeField]
    private float _ReSpawnTime;
    [SerializeField]
    private List<GameObject> _PlayersCursorPrefab;
    private List<Transform> _PlayerCursor;
    [SerializeField]
    private List<GameObject> _Minioms;
    [SerializeField]
    private GameObject _Particles;
    [SerializeField]
    private Text _ScoreTextP1;
    [SerializeField]
    private Text _ScoreTextP2;
    [SerializeField]
    private Text _WinerTex;

    void Awake() {
        int i = 0;
        Transform cells = this.transform.Find("Cells");

        _Cells = new List<List<Cell>>();
        foreach (Transform child in cells) {
            _Cells.Add(new List<Cell>());
            foreach (Transform grandChild in child.transform) {
                _Cells[i].Add(grandChild.GetComponent<Cell>());
            }
            i++;
        }

        _PlayerCursor = new List<Transform>();
        i = 0;
        foreach(GameObject elem in _PlayersCursorPrefab) {
            GameObject playCursor = Instantiate(elem, _Cells[0][0].transform.position, Quaternion.identity, this.transform);
            playCursor.name = "pj"+(i+1).ToString();
            _PlayerCursor.Add(playCursor.transform);
            i++;
        }
        _Time = 0;
        _ActualSate = BoardStates.PUSHING_FINISH_CELL;
        _SetP1Finish = _SetP2Finish = false;
    }


    void Update() {
        if (_ActualSate == BoardStates.PUSHING_FINISH_CELL) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                if(_SetP1Finish && _SetP2Finish)
                    _ActualSate++;
            }
        }
        if (_ActualSate == BoardStates.GAMING) {
            if (_Time < _SpawnTime) {
                if (_Time == 0) {
                    InvokeRepeating("PutRandomMiniom", 0, _ReSpawnTime);
                }
                _Time += Time.deltaTime;
            }else {
                _ActualSate++;
                CancelInvoke("PutRandomMiniom");
            }
        }
        if(_ActualSate == BoardStates.END) {
            if (!GameObject.FindWithTag("Miniom")) {
                string winerText;
                int resultP1, resultP2;

                int.TryParse(_ScoreTextP1.text, out resultP1);
                int.TryParse(_ScoreTextP2.text, out resultP2);

                if (resultP1 > resultP2)
                    winerText = "Player1 Win";
                else if (resultP2 < resultP1)
                    winerText = "Player2 Win";
                else
                    winerText = "Tie";
                _WinerTex.text = winerText;
            }
        }
    }

    public void SetScore(int score, int playerNumber) {
        if (playerNumber == 0) {
            _ScoreTextP1.text = score.ToString();
        } else {
            _ScoreTextP2.text = score.ToString();
        }
    }

    public BoardStates GetState() {
        return _ActualSate;
    }

    public void PutRandomMiniom() {
        while (true) {
            if (InstantiateMinon(Random.Range(0, 10), Random.Range(0, 10))) {
                break;
            }
        }
    }

    public bool InstantiateMinon(int xPos, int yPos) {
        if (!_Cells[xPos][yPos].IsOcupated()) {
            GameObject miniom = Instantiate(_Minioms[Random.Range(0, _Minioms.Count)], this.transform);
            miniom.name = "miniom";
            miniom.tag = "Miniom";
            miniom.transform.position = _Cells[xPos][yPos].transform.position;
            miniom.GetComponent<Character>().SetDirection((Direction)Random.Range(0, 4));
            return true;
        } else {
            return false;
        }
    }

    public Cell PutCard(int player, int CursorX, int CursorY, GameObject newCard) {
        if (!_Cells[CursorX][CursorY].IsOcupated()) {                         
            _Cells[CursorX][CursorY].SetCard(newCard);
            return _Cells[CursorX][CursorY];
        } else
            return null;
    }

    public bool PutFinish(int xPos, int yPos, Player playerUsed) {
        if (!_Cells[xPos][yPos].IsOcupated()) {
            _Cells[xPos][yPos].SetFinish(playerUsed, _Particles);
            if (playerUsed.GetPlayerNumber() == 0) {
                _SetP1Finish = true;
            }else {
                _SetP2Finish = true;
            }
            return true;
        } else {
            return false;
        }
    }


    public bool CellOcupated(int xPos, int yPos) {
        return _Cells[xPos-1][yPos-1];
    }

    public void MovePlayerCursor(int pj, int CursorX, int CursorY) {
            _PlayerCursor[pj].transform.position = _Cells[CursorX][CursorY].transform.position;
    }
}
