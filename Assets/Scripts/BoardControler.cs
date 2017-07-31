using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BoardStates {
    WATING_PLAYER=0,
    PUSHING_FINIS_CELL=1,
    GAMING=2
}

public class BoardControler : MonoBehaviour {

    public List<List<Cell>> _Cells;
    public List<GameObject> _Cards;

    [SerializeField]
    private List<GameObject> _PlayersPointersPrefab;
    private List<Transform> _PlayerCursor;
    [SerializeField]
    private List<GameObject> _Minioms;
    [SerializeField]
    private GameObject _Particles;
    // Use this for initialization
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
        foreach(GameObject elem in _PlayersPointersPrefab) {
            GameObject playCursor = Instantiate(elem, _Cells[0][0].transform.position, Quaternion.identity, this.transform);
            playCursor.name = "pj"+(i+1).ToString();
            _PlayerCursor.Add(playCursor.transform);
            i++;
        }
    }

    private void Start() {

        PutFinish(0, 0, Color.blue);
        InvokeRepeating("test", 0.75f, 1f);
    }

    void test() {
        InstantiateMinon( 5, 4);
    }
    // Update is called once per frame
    bool once = true;
	void Update () {

	}

    public bool InstantiateMinon(int xPos, int yPos) {
        if (!_Cells[xPos][yPos].IsOcupated()) {
            GameObject miniom = Instantiate(_Minioms[Random.Range(0, _Minioms.Count)], this.transform);
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

    public bool PutFinish(int xPos, int yPos, Color playerColor) {
        if (!_Cells[xPos][yPos].IsOcupated()) {
            _Cells[xPos][yPos].SetFinish(playerColor, _Particles);
            return true;
        }else {
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
