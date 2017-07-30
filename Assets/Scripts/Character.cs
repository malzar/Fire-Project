using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Character : MonoBehaviour {

    [SerializeField]
    private Vector3 _Direcction;
    [SerializeField]
    private float _Speed;
    [SerializeField]
    private int _DirectionRotation;
    [SerializeField]
    private float _OffsetCard;
    [SerializeField]
    private ParticleSystem _Dead;
    private bool _Check;
    // Use this for initialization
    void Start() {
        _OffsetCard = 0.4f;
        _Check = true;
    }

    // Update is called once per frame
    void Update() {
        transform.position += this.transform.forward * _Speed * Time.deltaTime;        
    }



    void OnTriggerEnter(Collider other) {
        if (other.tag == "Wall") {
            RotateCharacter();
            Vector3 dir;
            if (Mathf.Abs(other.transform.position.x) > 0) {
                if (other.transform.position.x > 0)
                    dir = Vector3.left;
                else
                    dir = Vector3.right;
            } else {
                if (other.transform.position.z > 0)
                    dir = Vector3.back;
                else
                    dir = Vector3.forward;
            }            
            transform.position += dir*0.025f;
        }else if (other.tag == "FinishCell") {
            other.GetComponent<Cell>().SendMiniomSafe();
            Destroy(this.gameObject);
        }
    }

    void OnTriggerStay(Collider other) {
        if (other.tag == "Cell" && _Check) {
            Vector3 offset = this.transform.position - other.transform.position;
            float distance = offset.sqrMagnitude;
            if (distance < _OffsetCard) {
                SetDirection(other.GetComponent<Cell>().GetDirection());
                this.transform.position = new Vector3(other.transform.position.x, this.transform.position.y, other.transform.position.z);
                _Check = false;
            }
        }
    }

    void OnTriggerExit(Collider other) {
        _Check = true;
    }

    public void SetDirection(Vector3 Direction) {

        int rotation = (int) (Direction.x * 10 + Direction.z);
                
        switch (rotation) {
            case 10:
                transform.rotation = Quaternion.Euler(0, 90f, 0);
                break;
            case -10:
                transform.rotation = Quaternion.Euler(0, 270f, 0);
                break;
            case 1:
                transform.rotation = Quaternion.Euler(0, 0f, 0);
                break;
            case -1:
                transform.rotation = Quaternion.Euler(0, 180f, 0);
                break;
            default:
                throw new System.Exception("La piña no tiene dirección");
        }
    }

    public void SetDirection(Direction dir) {  

        switch (dir) {
            case Direction.DOWN:
                transform.rotation = Quaternion.Euler(0, 180f, 0);
                break;
            case Direction.UP:
                transform.rotation = Quaternion.Euler(0, 0f, 0);
                break;
            case Direction.LEFT:
                transform.rotation = Quaternion.Euler(0, 270f, 0);
                break;
            case Direction.RIGTH:
                transform.rotation = Quaternion.Euler(0, 90f, 0);
                break;
            default:
                throw new System.Exception("La piña no tiene dirección");
        }
    }

    private void RotateCharacter() {
        //posible caos si metemos random para direccio
        transform.Rotate(Vector3.up, -90);
    }

}
