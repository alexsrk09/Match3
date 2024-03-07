using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Piece : MonoBehaviour
{

    public int x;
    public int y;
    public Board board;

    public enum type
    {
        elephant,
        giraffe,
        hippo,
        monkey,
        panda,
        parrot,
        penguin,
        pig,
        rabbit,
        snake
    };

    public type pieceType;

    public void Setup(int x_, int y_, Board board_)
    {
        this.x = x_;
        this.y = y_;
        this.board = board_;
    }

    public void Move(int desx, int desy)
    {
        transform.DOMove(new Vector3(desx, desy, -5f), 0.25f).
            SetEase(Ease.InOutCubic).onComplete = () =>{
                x = desx;
                y = desy;
        };
    }

    [ContextMenu("Test Move")]
    public void MoveTest()
    {
        Move(0,0);
    }
    private void Update() {
            // check x
            if (
                // if the piece is not null
                board.GetPiece(x+1,y) != null &&
                board.GetPiece(x-1,y) != null &&
                // if the piece is the same type
                board.GetPiece(x+1,y).pieceType == pieceType && board.GetPiece(x-1,y).pieceType == pieceType)
            {
                // destroy object
                Destroy(board.GetPiece(x+1,y).gameObject);
                Destroy(board.GetPiece(x-1,y).gameObject);
                Destroy(gameObject);
            }
            // check y
            if (
                // if the piece is not null
                board.GetPiece(x,y+1) != null &&
                board.GetPiece(x,y-1) != null &&
                // if the piece is the same type
                board.GetPiece(x,y+1).pieceType == pieceType && board.GetPiece(x,y-1).pieceType == pieceType)
            {
                // destroy object
                Destroy(board.GetPiece(x,y+1).gameObject);
                Destroy(board.GetPiece(x,y-1).gameObject);
                Destroy(gameObject);
            }
            // oblicuo 1
            if (
                // if the piece is not null
                board.GetPiece(x+1,y+1) != null &&
                board.GetPiece(x-1,y-1) != null &&
                // if the piece is the same type
                board.GetPiece(x+1,y+1).pieceType == pieceType && board.GetPiece(x-1,y-1).pieceType == pieceType)
            {
                // destroy object
                Destroy(board.GetPiece(x+1,y+1).gameObject);
                Destroy(board.GetPiece(x-1,y-1).gameObject);
                Destroy(gameObject);
            }
            // oblicuo 2
            if (
                // if the piece is not null
                board.GetPiece(x-1,y+1) != null &&
                board.GetPiece(x+1,y-1) != null &&
                // if the piece is the same type
                board.GetPiece(x-1,y+1).pieceType == pieceType && board.GetPiece(x+1,y-1).pieceType == pieceType)
            {
                // destroy object
                Destroy(board.GetPiece(x-1,y+1).gameObject);
                Destroy(board.GetPiece(x+1,y-1).gameObject);
                Destroy(gameObject);
            }
    }
}
