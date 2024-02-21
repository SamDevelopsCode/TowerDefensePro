using System;
using _TowerDefense.Towers;
using TowerDefense.Tower;
using UnityEngine;

public class Tile : MonoBehaviour
{
	[SerializeField] public Transform towerParent;
	[SerializeField] private bool _canPlaceTower;

	public bool CanPlaceTower
	{
		get => _canPlaceTower;
		set => _canPlaceTower = value;
	}

	public event Action<string> OnTileMouseOver;
	public event Action<Tile> TowerPlaceAttempted;
	public event Action<TowerData, Tile> TowerSelected;
	
	private bool _mouseHasEnteredTile;


	private void Start()
	{
		_canPlaceTower = true;
	}

	
	private void OnMouseOver()
	{
		if (!_mouseHasEnteredTile)
		{
			OnTileMouseOver?.Invoke(name);
			_mouseHasEnteredTile = true;
		}		
		
		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			if (_canPlaceTower)
			{
				TowerPlaceAttempted?.Invoke(this); 
			}
			else
			{
				TowerData towerData = towerParent.GetChild(0).GetComponent<Tower>().towerData;
				TowerSelected?.Invoke(towerData, this);
				CoreGameUI.Instance.OnTowerTypeSelected(towerData);
				CoreGameUI.Instance.UpdateDropDownTargetingBehaviourValue(towerParent.GetChild(0).gameObject);
			}
		}
	}
	
	
	private void OnMouseExit()
	{
		_mouseHasEnteredTile = false;
	}
}
