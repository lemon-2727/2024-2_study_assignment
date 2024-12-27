using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // 프리팹들
    public GameObject TilePrefab;
    public GameObject[] PiecePrefabs;   // King, Queen, Bishop, Knight, Rook, Pawn 순
    public GameObject EffectPrefab;

    // 오브젝트의 parent들
    private Transform TileParent;
    private Transform PieceParent;
    private Transform EffectParent;
    
    private MovementManager movementManager;
    private UIManager uiManager;
    
    public int CurrentTurn = 1; // 현재 턴 1 - 백, 2 - 흑
    public Tile[,] Tiles = new Tile[Utils.FieldWidth, Utils.FieldHeight];   // Tile들
    public Piece[,] Pieces = new Piece[Utils.FieldWidth, Utils.FieldHeight];    // Piece들

    void Awake()
    {
        TileParent = GameObject.Find("TileParent").transform;
        PieceParent = GameObject.Find("PieceParent").transform;
        EffectParent = GameObject.Find("EffectParent").transform;
        
        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        movementManager = gameObject.AddComponent<MovementManager>();
        movementManager.Initialize(this, EffectPrefab, EffectParent);
        
        InitializeBoard();
    }

    void InitializeBoard()
    {
        // 8x8로 타일들을 배치
        // TilePrefab을 TileParent의 자식으로 생성하고, 배치함
        // Tiles를 채움
        // --- TODO ---
        for (int x = 0; x <Utils.FieldWidth ; x++){
            for (int y = 0; y < Utils.FieldHeight; y++){
                GameObject tileObj = Instantiate(TilePrefab, TileParent);
                tileObj.transform.position = new Vector3(x, y, -1);
                Tile tile = tileObj.GetComponent<Tile>();
                tile.Set((x, y));
                Tiles[x, y] = tile;
            }
        }
        
        // ------

        PlacePieces(1);
        PlacePieces(-1);
    }

    void PlacePieces(int direction)
    {
        // PlacePiece를 사용하여 Piece들을 적절한 모양으로 배치
        // --- TODO ---
        int startRow = direction == 1 ? 0 : Utils.FieldHeight - 1;
        int pawnRow = direction == 1 ? 1 : Utils.FieldHeight - 2;
         // Rook
        PlacePiece(5, (0, startRow), direction);
        // Knight
        PlacePiece(4, (1, startRow), direction);
        // Bishop 
        PlacePiece(3, (2, startRow), direction); 
        // Queen
        PlacePiece(2, (3, startRow), direction); 
        // King
        PlacePiece(1, (4, startRow), direction); 
        // Bishop
        PlacePiece(3, (5, startRow), direction); 
        // Knight
        PlacePiece(4, (6, startRow), direction); 
        // Rook
        PlacePiece(5, (7, startRow), direction); 

        // Pawn
        for (int x = 0; x < Utils.FieldWidth; x++)
        {
            PlacePiece(6, (x, pawnRow), direction); 
        }

        
        // ------
    }

    Piece PlacePiece(int pieceType, (int, int) pos, int direction)
    {
        // Piece를 배치 후, initialize
        // PiecePrefabs의 원소를 사용하여 배치, PieceParent의 자식으로 생성
        // Pieces를 채움
        // 배치한 Piece를 리턴
        // --- TODO ---
        GameObject pieceObj = Instantiate(PiecePrefabs[pieceType - 1], PieceParent);
        pieceObj.transform.position = new Vector3(pos.Item1, pos.Item2, -1);
        Piece piece = pieceObj.GetComponent<Piece>();
        piece.initialize(pos, direction);
        Pieces[pos.Item1, pos.Item2] = piece;
        return piece;
  
        
        // ------
    }

    public bool IsValidMove(Piece piece, (int, int) targetPos)
    {
        return movementManager.IsValidMove(piece, targetPos);
    }

    public void ShowPossibleMoves(Piece piece)
    {
        movementManager.ShowPossibleMoves(piece);
    }

    public void ClearEffects()
    {
        movementManager.ClearEffects();
    }


    public void Move(Piece piece, (int, int) targetPos)
    {
        if (!IsValidMove(piece, targetPos)) return;
        
        // 해당 위치에 다른 Piece가 있다면 삭제
        // Piece를 이동시킴
        // --- TODO ---
        int targetX = targetPos.Item1;
        int targetY = targetPos.Item2;

        if (Pieces[targetX, targetY] != null){
            Destroy(Pieces[targetX, targetY].gameObject);
        }

        piece.MoveTo(targetPos);
        Pieces[targetX, targetY] = piece;
        ChangeTurn();
                
                // ------
    }

    void ChangeTurn()
    {
        // 턴을 변경하고, UI에 표시
        // --- TODO ---
        CurrentTurn = CurrentTurn == 1 ? 2 : 1;
        uiManager.ShowMessage("Turn has changed");
        
        // ------
    }
}
