using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CELLTYPE{
    START =1,
    FINISH=-1,
    NORMAL=0
}


public class Cell : MonoBehaviour {
    [SerializeField]
    private CELLTYPE _Type;
    [SerializeField]
    private Card _ActualCard;

    void Awake(){
       
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

    public Vector3 GetDirection(){

        Direction dir = _ActualCard.GetComponent<Card>().GetDirection();
        switch (dir)
        {
            case Direction.DOWN:
                return Vector3.back;
                break;
            case Direction.UP:
                return Vector3.forward;
                break;
            case Direction.LEFT:
                return Vector3.left;
                break;
            case Direction.RIGTH:
                return Vector3.right;
                break;
            default:
                throw new System.Exception("La piña no tiene dirección");
                break;
        }

    }

    void SetCard(Card newCard){

        
    }
}
