

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBehaviour : MonoBehaviour
{
    public Player AiPlayer;
    MiniMax miniMax;
    void Start()
    {
        miniMax = new MiniMax();
        AiPlayer = GameManager.instance.otherPlayer;
    }
    void Update()
    {
        if (AiPlayer==GameManager.instance.currentPlayer)
        {
            Move move = miniMax.GetMove();
            _DoAIMove(move);
        }
    }
    void _DoAIMove(Move move)
    {
        GameManager.instance.SwapPieces(move);
    }
}
