using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CELLTYPE{
    SPAWN =1,
    FINISH=-1,
    NORMAL=0,
    OCUPATED=2
}


public class Cell : MonoBehaviour {
    [SerializeField]
    private CELLTYPE _Type;
    [SerializeField]
    private Card _ActualCard;
    [SerializeField]
    private Points _PointsController;
    [SerializeField]
    private GameObject _Particles;
    [SerializeField]
    private Color _PlayerColor;

    public CELLTYPE GetTipe() { return _Type; }

    public Vector3 GetDirection(){

        Direction dir = _ActualCard.GetComponent<Card>().GetDirection();
        switch (dir)
        {
            case Direction.DOWN:
                return Vector3.back;
            case Direction.UP:
                return Vector3.forward;
            case Direction.LEFT:
                return Vector3.left;
            case Direction.RIGTH:
                return Vector3.right;
            default:
                throw new System.Exception("La piña no tiene dirección");
        }

    }

    public void RemoveCard() {
        _ActualCard = null;
        this.transform.tag = "";
        transform.GetComponent<BoxCollider>().isTrigger = false;
    }

    public bool IsOcupated() {
        if (_ActualCard != null || _Type != CELLTYPE.NORMAL)
            return true;
        else
            return false;
    }

    public void SetCard(GameObject cardPrefab){
        _ActualCard = cardPrefab.GetComponent<Card>();
        cardPrefab.transform.position = this.transform.position;
        this.transform.tag = "Cell";
        transform.GetComponent<BoxCollider>().isTrigger = true;    
    }

    public void SetFinish(Color playerColor, GameObject particles) {
        _PlayerColor = playerColor;
        _Particles = particles;
        _Type = CELLTYPE.FINISH;
        this.transform.tag = "FinishCell";
        transform.GetComponent<BoxCollider>().isTrigger = true;
        GameObject ligthExit = new GameObject();
        ligthExit.transform.position = this.transform.position + Vector3.up * 3f;
        ligthExit.transform.Rotate(Vector3.right, 90f);
        Light ligthComp= ligthExit.AddComponent<Light>();
        ligthComp.type = LightType.Spot;
        ligthComp.range = 20f;
        ligthComp.spotAngle = 40;
        ligthComp.color = playerColor;
    }

    public void SendMiniomSafe() {
        if (_PointsController)
            _PointsController.GetComponent<PlayerData>().IncrementScore();
        GameObject part = Instantiate(_Particles);
        part.transform.position = this.transform.position;
        //part.GetComponent<ParticleSystem>().startColor = Color.white;
        Destroy(part, 1.5f);
    }
}
