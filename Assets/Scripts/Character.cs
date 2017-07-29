﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Character : MonoBehaviour {

    public Vector3 _Direcction;
    public float _Speed;
    public int _DirectionRotation;

    // Use this for initialization
    void Start() {
    }

    // Update is called once per frame
    void Update() {

        transform.position += _Direcction * _Speed * Time.deltaTime;

    }



    void OnTriggerEnter(Collider other) {
        if (other.tag == "Wall") {
            RotateCharacter();
        } else if (other.tag == "Cell") {
            SetDirection(other.GetComponent<Cell>().GetDirection());
        }

    }

    private void SetDirection(Vector3 Direction) {
        _Direcction = Direction;
        if (Mathf.Abs(Direction.x) == 1) {

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
