using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class Player : MonoBehaviour {

    private PlayerData _Data;
    private PlayerController _Controller;

    [SerializeField]
    private int _PlayerNumber;
    [SerializeField]
    BoardControler _Board;
    [SerializeField]
    private int _NumOfUpCard;
    [SerializeField]
    private int _NumOfDownCard;
    [SerializeField]
    private int _NumOfLeftCard;
    [SerializeField]
    private int _NumOfRightCard;
    [SerializeField]
    private Color _PlayerColor;

    private Deck _DownDeck;
    private Deck _LeftDeck;
    private Deck _RightDeck;
    private Deck _UpDeck;
    private bool _FisnisCell;

    void Awake() {
        _LeftDeck = this.transform.Find("DeckLeft").GetComponent<Deck>();
        _RightDeck = this.transform.Find("DeckRight").GetComponent<Deck>();
        _UpDeck = this.transform.Find("DeckUp").GetComponent<Deck>();
        _DownDeck = this.transform.Find("DeckDown").GetComponent<Deck>(); 

        Ininitialize(10,10,0);
    }

    public Color GetPlayerColor() {
        return _PlayerColor;
    }

    public int GetPlayerNumber() {
        return _PlayerNumber;
    }

    public void Ininitialize(int boardSizeX, int boardSiceY, int playerNumber) {
        _Data = new PlayerData(boardSizeX,boardSiceY);
        //_PlayerNumber = playerNumber;
        _LeftDeck.SetNumberOfCard(_NumOfLeftCard);
        _RightDeck.SetNumberOfCard(_NumOfRightCard);
        _UpDeck.SetNumberOfCard(_NumOfUpCard);
        _DownDeck.SetNumberOfCard(_NumOfDownCard);
        _FisnisCell = false;
    }

    public void ButtonPresed(Direction dir) {
        BoardStates boardState = _Board.GetState();
        if (boardState == BoardStates.PUSHING_FINISH_CELL && !_FisnisCell) {
            _Board.PutFinish(GetCursorX(), GetCursorY(), this);
            _FisnisCell = true;
        }else if(boardState == BoardStates.GAMING|| _Board.GetState() == BoardStates.END) {
            PutCard(dir);
        }
    }

    public void IncrementScore() {
        _Data.IncrementScore();
        _Board.SetScore(_Data.GetScore(),_PlayerNumber);
    }

    public void MoveCursor(Direction dir) {
        if (_Data.MoveCursor(dir)) {
            _Board.MovePlayerCursor(_PlayerNumber, GetCursorX(), GetCursorY());
        }
    }

    public int GetCursorX() { return _Data.GetXPosition(); }
    public int GetCursorY() { return _Data.GetYPosition(); }

    private void PutCard(Direction dir) {
        GameObject cardToUse;
        Cell cellToUse;
        switch (dir) {
            case Direction.UP:
                cardToUse = _UpDeck.GetCard(_PlayerNumber);
                break;
            case Direction.DOWN:
                cardToUse = _DownDeck.GetCard(_PlayerNumber);
                break;
            case Direction.RIGTH:
                cardToUse = _RightDeck.GetCard(_PlayerNumber);
                break;
            case Direction.LEFT:
                cardToUse = _LeftDeck.GetCard(_PlayerNumber);
                break;
            default:
                cardToUse = null;
                break;
        }
        cellToUse = _Board.PutCard(_PlayerNumber, GetCursorX(), GetCursorY(), cardToUse);
        switch (dir) {
            case Direction.UP:
                _UpDeck.PutCard(cellToUse, cardToUse);
                break;
            case Direction.DOWN:
                _DownDeck.PutCard(cellToUse, cardToUse);
                break;
            case Direction.RIGTH:
                _RightDeck.PutCard(cellToUse, cardToUse);
                break;
            case Direction.LEFT:
                _LeftDeck.PutCard(cellToUse, cardToUse);
                break;
            default:
                cardToUse = null;
                break;
        }
    }
}
