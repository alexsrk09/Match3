using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Board : MonoBehaviour
{
    public int width;
    public int height;
    public GameObject tileObject;

    public float cameraSizeOffset;
    public float cameraVerticalOffset;

    public GameObject[] avalaiblePieces;

    Tile[,] Tiles;
    Piece[,] Pieces;

    Tile startTile;
    Tile endTile;

    // Start is called before the first frame update
    void Start()
    {
        Tiles = new Tile[width, height];    
        Pieces = new Piece[width, height];  
        SetupBoard();
        PositionCamera();
        SetupPieces();
    }
    private void Update() {
        FallDown();
        spawnPiece();
    }

    private void SetupPieces()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var selectedPiece = avalaiblePieces[UnityEngine.Random.Range(0,avalaiblePieces.Length)];
                var o = Instantiate(selectedPiece, new Vector3(x, y, -5), Quaternion.identity);
                o.transform.parent = transform;
                Pieces[x, y] = o.GetComponent<Piece>();
                Pieces[x, y]?.Setup(x, y, this);
            }

        }
    }

    private void PositionCamera()
    {
        float newPosX = (float)width / 2f;
        float newPosY = (float)height / 2f;
        Camera.main.transform.position = new Vector3 (newPosX -0.5f, newPosY- 0.5f + cameraVerticalOffset, -10f);

        float horizontal = width + 1;
        float vertical = (height/2) + 1;

        Camera.main.orthographicSize = horizontal > vertical ? horizontal + cameraSizeOffset : vertical;
    }

    private void SetupBoard()
    {
        for(int x=0; x<width;x++)
        {
            for(int y=0; y<height;y++)
            {
                var o = Instantiate(tileObject, new Vector3(x,y,-5), Quaternion.identity);
                o.transform.parent = transform;
                Tiles[x,y] = o.GetComponent<Tile>();
                Tiles[x,y]?.Setup(x, y, this);  
            }

        }
    }


    public void TileDown(Tile tile_)
    {
        startTile = tile_;

    }

    public void TileOver(Tile tile_)
    {
        endTile = tile_;
    }

    public void TileUp(Tile tile_)
    {
        if(startTile != null && endTile != null && IsCloseTo(startTile, endTile))
        {
            SwapTiles();
        }
    }

    private void SwapTiles()
    {
        var StarPiece = Pieces[startTile.x, startTile.y];
        var EndPiece = Pieces[endTile.x, endTile.y];

        StarPiece.Move(endTile.x, endTile.y);
        EndPiece.Move(startTile.x, startTile.y);

        Pieces[startTile.x, startTile.y] = EndPiece;
        Pieces[endTile.x, endTile.y] = StarPiece;

    }

    public bool IsCloseTo(Tile start, Tile end)
    {
        if(Math.Abs((start.x - end.x)) == 1 && start.y == end.y)
        {
            return true;
        }

        if (Math.Abs((start.y - end.y)) == 1 && start.x == end.x)
        {
            return true;
        }

        return false;
    }
    public Piece GetPiece(int x, int y)
    {
        if(x < 0 || x >= width || y < 0 || y >= height)
        {
            return null;
        }
        return Pieces[x, y];
    }
    private void FallDown()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if(Pieces[x,y] == null)
                {
                    for (int pos = y+1; pos < height; pos++)
                    {
                        if(Pieces[x,pos] != null)
                        {
                            Pieces[x, pos].Move(x, y);
                            Pieces[x, y] = Pieces[x, pos];
                            Pieces[x, pos] = null;
                            break;
                        }
                    }
                }
            }
        }
    }

    private void spawnPiece()
    {
        // if there is an empty space in top board
        for (int i = 0; i < width; i++)
        {
            if (GetPiece(i, height-1) == null)
            {
                // spawn a new piece
                var selectedPiece = avalaiblePieces[UnityEngine.Random.Range(0,avalaiblePieces.Length)];
                var o = Instantiate(selectedPiece, new Vector3(i, height-1, -5), Quaternion.identity);
                o.transform.parent = transform;
                Pieces[i, height-1] = o.GetComponent<Piece>();
                Pieces[i, height-1]?.Setup(i, height-1, this);
            }
        }
    }
}
