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
    private bool _Check;
    // Use this for initialization
    void Start() {
        _OffsetCard = 0.5f+transform.position.y* transform.position.y;
        _Check = true;
    }

    // Update is called once per frame
    void Update() {

        transform.position += _Direcction * _Speed * Time.deltaTime;
        
    }



    void OnTriggerEnter(Collider other) {
        if (other.tag == "Wall") {
            RotateCharacter();
        } 
    }

    void OnTriggerStay(Collider other) {
        if (other.tag == "Cell"&& _Check) {
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

    private void SetDirection(Vector3 Direction) {
        Vector3 dirToLook;
        _Direcction = Direction;
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
                break;
        }
    }

    private void RotateCharacter() {
        //posible caos si metemos random para direccion
        _DirectionRotation += 90;
        _DirectionRotation = Mathf.Abs(_DirectionRotation) % 360;
        _Direcction = new Vector3(Mathf.Cos(Mathf.Deg2Rad * _DirectionRotation), 0, Mathf.Cos(Mathf.Deg2Rad * (_DirectionRotation - 90)));
        transform.Rotate(Vector3.up, -90);
    }
}
