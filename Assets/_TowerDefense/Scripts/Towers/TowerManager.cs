using System.Collections.Generic;
using TowerDefense.Managers;
using UnityEngine;

namespace _TowerDefense.Towers
{
	public class TowerManager : MonoBehaviour
	{
		[SerializeField] private Transform _validTowerTilesParent;
		[SerializeField] private TowerSpawner _towerSpawner;
		[SerializeField] private List<GameObject> _towerBaseTypePrefabs = new();

		private Tower _selectedTowerType;
		private bool _canPlaceTowers;
		

		private void Awake()
		{
			GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
		}

	
		private void Start() 
		{
			for (int i = 0; i < _validTowerTilesParent.childCount; i++)
			{
				_validTowerTilesParent.GetChild(i).GetComponent<Tile>().OnTileMouseOver += HandleMouseTileOver;
				_validTowerTilesParent.GetChild(i).GetComponent<Tile>().OnTowerPlaceAttempted += HandleTowerPlaceAttempted;
			}
		}

	
		private void GameManagerOnGameStateChanged(GameState state)
		{
			_canPlaceTowers = state == GameState.TowerPlacement;
		}
	
		
		private void Update()
		{
			SelectTowerType();
		}

		private void SelectTowerType()
		{
			if (Input.GetKeyDown(KeyCode.Alpha1))
			{
				_selectedTowerType = _towerBaseTypePrefabs[0].GetComponent<Tower>();
				Debug.Log("Tower type Ballista chosen.");
			}
			else if (Input.GetKeyDown(KeyCode.Alpha2))
			{
				_selectedTowerType = _towerBaseTypePrefabs[1].GetComponent<Tower>();
				Debug.Log("Tower type Fire chosen.");
			}	
			else if (Input.GetKeyDown(KeyCode.Alpha3))
			{
				_selectedTowerType = _towerBaseTypePrefabs[2].GetComponent<Tower>();
				Debug.Log("Tower type Lightning chosen.");
			}
		}


		private void HandleMouseTileOver(string tileName)
		{
			//TODO spawn and move a transparent version of the selected tower type to the tile position
		}
	
	
		private void HandleTowerPlaceAttempted(Tile tile)
		{
			var towerCost = _selectedTowerType.towerData.towerCost;
			
			if (_canPlaceTowers)
			{
				if (_selectedTowerType == null)
				{
					Debug.Log("No tower has been selected.");
					return;
				}
			
				
				
				if (CurrencyManager.Instance.CanAffordTower(towerCost))
				{
					tile.IsTowerPlaceable = false;
					_towerSpawner.SpawnTower(_selectedTowerType, tile.towerParent);
					CurrencyManager.Instance.DetractFromBalance(towerCost);
				}
				else
				{
					Debug.Log("Not enough funds. Tower cost: " + towerCost + ". Current money: " + CurrencyManager.Instance.CurrentBalance);
				}
			}
		}
	}
}
