using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardControler : MonoBehaviour {

    public List<List<Cell>> _Cells;
    public List<GameObject> _Cards;

    [SerializeField]
    private List<GameObject> _PlayersPointersPrefab;
    private List<Transform> _PlayerPointers;
    [SerializeField]
    private List<PlayerData> _PlayerData;
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

        _PlayerPointers = new List<Transform>();
        i = 0;
        foreach(GameObject elem in _PlayersPointersPrefab) {
            GameObject playPoin = Instantiate(elem, _Cells[0][0].transform.position, Quaternion.identity, this.transform);
            playPoin.name = "pj"+(i+1).ToString();
            _PlayerPointers.Add(playPoin.transform);
            _PlayerData[i].Inicializate(10, 10);
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
        if (once) {
            for (int i = 0; i < 20; i++) {
                Invoke("test", 0.75f);
            }
            once = false;
        }

	}

    public void InstantiateMinon(int xPos, int yPos) {
        GameObject miniom = Instantiate(_Minioms[Random.Range(0,_Minioms.Count)], this.transform);
        miniom.transform.position = _Cells[xPos][yPos].transform.position;
        miniom.GetComponent<Character>().SetDirection((Direction)Random.Range(0, 4));
    }

    public Cell PutCard(Direction dir,int player) {
        int xPos = _PlayerData[player].GetXPosition();
        int yPos = _PlayerData[player].GetYPosition();

        if (!_Cells[xPos][yPos].IsOcupated()) {
            GameObject newCard;
            switch (dir) {
                case Direction.UP:
                   newCard= _PlayerData[player].PutUpCard(_Cells[xPos][yPos]);
                    break;
                case Direction.DOWN:
                    newCard = _PlayerData[player].PutDownCard(_Cells[xPos][yPos]);
                    break;
                case Direction.LEFT:
                    newCard = _PlayerData[player].PutLeftCard(_Cells[xPos][yPos]);
                    break;
                case Direction.RIGTH:
                    newCard = _PlayerData[player].PutRightCard(_Cells[xPos][yPos]);
                    break;
                default:
                    newCard = null;
                    break;
            }
             
            _Cells[xPos][yPos].SetCard(newCard);
            return _Cells[xPos][yPos];
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

    public Cell GetCell(int xPos, int yPos) {
        return _Cells[xPos-1][yPos-1];
    }

    public void MovePj(Direction dir, int pj) {
        if (_PlayerData[pj].MovePj(dir))
            _PlayerPointers[pj].transform.position = _Cells[_PlayerData[pj].GetXPosition()][_PlayerData[pj].GetYPosition()].transform.position;
    }
}
