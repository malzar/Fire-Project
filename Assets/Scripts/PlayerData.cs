public class PlayerData{
    private int _ActualX;
    private int _ActualY;
    private int _MaxX;
    private int _MaxY;
    private int _Score;    

    public PlayerData(int maxX, int maxY) {
        _MaxX = maxX-1;
        _MaxY = maxY-1;
        _ActualX = 0;
        _ActualY = 0;
        _Score = 0;
    }

    public int GetXPosition() {
        return _ActualX;
    }

    public int GetYPosition() {
        return _ActualY;
    }

    public int GetScore() {
        return _Score;
    }

    public void IncrementScore() {
        _Score++;
    }

    public bool MoveCursor(Direction dir) {
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
