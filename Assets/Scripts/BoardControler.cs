using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardControler : MonoBehaviour {

    public List<List<Cell>> _Cells;
    public List<GameObject> _Cards;
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
    }

    private void Start() {
        foreach (GameObject go in _Cards) {
            int x = Random.Range(0, 9);
            int y = Random.Range(0, 9);
            PutCard(go, x, y);
        }
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

    public bool PutCard(GameObject go, int xPos, int yPos) {
        if (!_Cells[xPos][yPos].IsOcupated()) {
            _Cells[xPos][yPos].SetCard(go);
            return true;
        } else
            return false;
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
}
