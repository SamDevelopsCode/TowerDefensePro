using System;
using TowerDefense.Tower;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TowerDefense.Managers
{
	public class GameManager : MonoBehaviour
	{
		public static GameManager Instance;

		public GameState State;

		public static event Action<GameState> GameStateChanged;
	
	
		private void Awake()
		{
			Instance = this;
		}

		
		private void Start()
		{
			UpdateGameState(GameState.TowerPlacement);
		}
	

		public void UpdateGameState(GameState newState)
		{
			State = newState;
			
			switch (State)
			{
				case GameState.TowerPlacement:
					break;
				case GameState.EnemyWave:
					break;
				case GameState.Victory:
					LoadEndOfLevelScene();
					break;
				case GameState.Lose:
					LoadEndOfLevelScene();
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
			}
		
			GameStateChanged?.Invoke(newState);
		}

		
		private void QuitToMainMenu()
		{
			SceneManager.LoadScene(0);
		}
	
	
		private void LoadEndOfLevelScene()
		{
			SceneManager.LoadScene(2);
		}
	}

	
	public enum GameState
	{
		TowerPlacement,
		EnemyWave,
		Victory,
		Lose
	}
}