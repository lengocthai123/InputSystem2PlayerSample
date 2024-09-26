using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private Material[] _playerColors;

    /// <summary>
    ///  プレイヤー参加時の処理
    /// </summary>
    /// <param name="playerInput"></param>
    private void OnPlayerJoined(PlayerInput playerInput)
    {
        Debug.Log($"Player {playerInput.playerIndex} joined");
        InitializePlayer(playerInput);
    }

    /// <summary>
    ///  プレイヤー離脱時の処理
    /// </summary>
    /// <param name="playerInput"></param>
    private void OnPlayerLeft(PlayerInput playerInput)
    {
        Debug.Log($"Player {playerInput.playerIndex} left");
    }

    /// <summary>
    ///  プレイヤー初期化
    /// </summary>
    /// <param name="playerInput"></param>
    private void InitializePlayer(PlayerInput playerInput)
    {
        var playerIndex = playerInput.playerIndex;

        // プレイヤー名を設定
        playerInput.name = $"Player{playerIndex}";

        // プレイヤーの色を設定
        var playerColor = _playerColors[playerIndex];
        playerInput.GetComponent<Renderer>().material = playerColor;
    }
}