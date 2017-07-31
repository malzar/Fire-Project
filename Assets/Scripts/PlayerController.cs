using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Player _Player;

    [SerializeField]
    private KeyCode _Up;
    [SerializeField]
    private KeyCode _Down;
    [SerializeField]
    private KeyCode _Left;
    [SerializeField]
    private KeyCode _Right;

    [SerializeField]
    private KeyCode _UpCard;
    [SerializeField]
    private KeyCode _DownCard;
    [SerializeField]
    private KeyCode _LeftCard;
    [SerializeField]
    private KeyCode _RightCard;

    void Awake() {
        _Player = this.transform.GetComponent<Player>();
    }


    // Update is called once per frame
    void Update () {

        if (Input.GetKeyDown(_Up))
            _Player.MoveCursor(Direction.UP);

        if (Input.GetKeyDown(_Down))
            _Player.MoveCursor(Direction.DOWN);

        if (Input.GetKeyDown(_Left))
            _Player.MoveCursor(Direction.LEFT);

        if (Input.GetKeyDown(_Right))
            _Player.MoveCursor(Direction.RIGTH);

        if (Input.GetKeyDown(_UpCard))
            _Player.PutCard(Direction.UP);

        if (Input.GetKeyDown(_DownCard))
            _Player.PutCard(Direction.DOWN);

        if (Input.GetKeyDown(_LeftCard))
            _Player.PutCard(Direction.LEFT);

        if (Input.GetKeyDown(_RightCard))
            _Player.PutCard(Direction.RIGTH);

    }
}
