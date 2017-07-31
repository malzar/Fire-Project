using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour {
    private List<Cell> _UsedCells;
    private int _CardMax;
    private List<GameObject> _CardInGame;
    [SerializeField]
    private GameObject _CardPrefab;

    public Deck() {
        _UsedCells = new List<Cell>();
        _CardInGame = new List<GameObject>();
    }

    public void SetNumberOfCard(int numberOfCards) {
        _CardMax = numberOfCards;
    }

    public GameObject GetCard(int player) {
        GameObject newCard;

        if (_CardInGame.Count >= _CardMax) {
            newCard = _CardInGame[0];
        }else {
            newCard = Instantiate(_CardPrefab);
            newCard.name = "Dekc_Player" + player.ToString();
        }
        return newCard;
    }

    public void PutCard(Cell CellToUse, GameObject Card) {
        if (CellToUse) {
            if (_CardInGame.Count >= _CardMax) {
                Cell old = _UsedCells[0];
                _UsedCells.RemoveAt(0);
                old.RemoveCard();
                _CardInGame.RemoveAt(0);
            }
            _CardInGame.Add(Card);
            _UsedCells.Add(CellToUse);
        }else {
            if (_CardInGame.Count < _CardMax) {
                Destroy(Card);
            }
        }
    }
}
