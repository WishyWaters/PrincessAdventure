using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace PrincessAdventure
{
	public class GameManager : MonoBehaviour
	{
		[SerializeField] private GuiManager _guiMgr;
		[SerializeField] private CompanionManager _companionMgr;
		[SerializeField] private CinemachineVirtualCamera _camera;
		[SerializeField] private GameObject _princessPrefab;


		public static GameManager GameInstance;

		private SceneManager _sceneMgr;
		private CharacterController _charCtrl;
		private GameState _currentState = GameState.Undefined;
		private ActiveGame _gameDetails;
		private float _invincibleUntilTime = 0;

		private bool _controllingCompanion = false;
		private float _manaRegenTime;

        #region Unity Functions
        private void Awake()
		{
			if (GameInstance == null)
			{
				DontDestroyOnLoad(this);
				GameInstance = this;
			}
			else if (GameInstance != this)
			{
				Destroy(gameObject);
			}
		}

        private void Start()
        {
			SetupGame();
		}

		private void Update()
        {
			if(_currentState == GameState.Playing)
				ManaRegen();
        }

        #endregion

        #region start up functions
        private void SetupGame()
        {
			_currentState = GameState.Loading;
			//Load Game Details
			LoadGameDetails();

			//Setup this scene
			_sceneMgr = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneManager>();
			GameObject checkPoint = _sceneMgr.GetSceneCheckPoint();
			_camera.Follow = checkPoint.transform;
			_camera.enabled = true;

			SoundManager.SoundInstance.ChangeMusic(_sceneMgr.GetSceneMusic(), true);


			//Call UI Setup
			LoadGameplayGui();

			//Play Cutscene
			_currentState = GameState.Cutscene;

			PrincessSpawnController spawnCtrl = this.GetComponent<PrincessSpawnController>();
			spawnCtrl.StartPrincessSpawn(checkPoint.transform.position);
		}

		public void LoadPlayer(Vector3 spawnPosition)
        {
			GameObject princess = Instantiate(_princessPrefab, spawnPosition, new Quaternion());
			_charCtrl = princess.GetComponent<CharacterController>();

			ActivatePrincess(false);
			_currentState = GameState.Playing;
		}

		private void LoadGameDetails()
		{
			_gameDetails = new ActiveGame(1);
			_gameDetails.currentHealth = _gameDetails.heartPoints;
			_gameDetails.maxManaPoints = _gameDetails.magicPoints * 50;
			_gameDetails.currentManaPoints = _gameDetails.maxManaPoints;

		}

		public void LoadDefeatGui()
        {
			_currentState = GameState.Menu;
			_guiMgr.LoadDefeatGui();
        }

		public void LoadStarShardGui()
		{
			_currentState = GameState.Menu;
			_guiMgr.LoadStarShardGui(_gameDetails.starShards);
		}

		private void LoadGameplayGui()
        {
			_guiMgr.LoadGameplayGui(_gameDetails);

		}

		public void ContinueGameAfterDeath()
        {
			SetupGame();
		}
		public void QuitGame()
        {
			#if UNITY_EDITOR
						// Application.Quit() does not work in the editor so
						// UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
						UnityEditor.EditorApplication.isPlaying = false;
			#else
					 Application.Quit();
			#endif

        }
        #endregion

        #region Gameplay Actions

		public GameState GetCurrentGameState()
        {
			return _currentState;
        }
		public void RouteInputs(PrincessInputActions inputs)
        {
			if (_currentState == GameState.Playing)
			{
				if (_controllingCompanion)
					_companionMgr.UpdateNextInputs(inputs);
				else
					_charCtrl.UpdateNextInputs(inputs);
			}
		}

		public void ActivateCompanion(Vector3 targetPosition)
        {
			//TODO: Get selected companion from GameDetails
			GameObject companion = _companionMgr.HandleCompanionActivation(1, targetPosition);
			ControlCompanion(companion);
		}

		public void ActivatePrincess(bool unsummon)
		{
			if (unsummon)
				_companionMgr.DestroyCurrentSummon();

            ControlPrincess();
		}

		private void ControlPrincess()
		{
			_controllingCompanion = false;
			_charCtrl.EnableController();
			_companionMgr.SetIgnoreInputs(true);
			_camera.Follow = _charCtrl.gameObject.transform;

		}

		private void ControlCompanion(GameObject companion)
		{
			_controllingCompanion = true;
			_charCtrl.DisableController();
			_companionMgr.SetIgnoreInputs(false);
			_camera.Follow = companion.transform;

		}

		public void DamageCompanion()
        {
			ActivatePrincess(true);
        }

		public void DamagePrincess(Vector3 hitFromPosition)
        {
			if(_controllingCompanion)
				ActivatePrincess(true);

			if (_invincibleUntilTime <= Time.time)
			{
				_invincibleUntilTime = Time.time + 1;

				//Update game details
				_gameDetails.currentHealth -= 1;

				_guiMgr.EmptyOneHeart();

				_charCtrl.HandleDamage(hitFromPosition);

				if (_gameDetails.currentHealth == 0)
					KillPrincess();
			}
        }

		public void CoinDamagePrincess(Vector3 hitFromPosition, int coinDmg)
		{
			if (_controllingCompanion)
				ActivatePrincess(true);

			if (_invincibleUntilTime <= Time.time)
			{
				_invincibleUntilTime = Time.time + 1;

				//Update game details
				if (_gameDetails.gold - coinDmg <= 0)
					coinDmg = _gameDetails.gold;

				_gameDetails.gold -= coinDmg;
				
				_guiMgr.UpdateGoldText(_gameDetails.gold);

				_charCtrl.HandleCoinDamage(hitFromPosition, Mathf.FloorToInt(coinDmg /2));

			}
		}

		public bool HasMana(int manaNeed)
        {
			if (_gameDetails.currentManaPoints >= manaNeed)
				return true;
			else
				return false;
        }

		private void ManaRegen()
        {
			//Add 1% mana every second
			if (_manaRegenTime > 1f)
			{
				_manaRegenTime = 0;
				int recoveryAmt = 2 + Mathf.FloorToInt(_gameDetails.maxManaPoints * .005f);

				ManaRecover(recoveryAmt);
			}
			else
				_manaRegenTime += Time.deltaTime;
		}

		public void ManaPotionUse()
        {
			//Add 75pts or 30%, which ever is higher mana on potion get
			//int recoveryAmt = Mathf.FloorToInt(_gameDetails.maxManaPoints * .20f);

			//if (recoveryAmt < 60)
			int recoveryAmt = 60;

			ManaRecover(recoveryAmt);

		}

		private void ManaRecover(int amount)
        {
			if (_gameDetails.currentManaPoints + amount > _gameDetails.maxManaPoints)
				_gameDetails.currentManaPoints = _gameDetails.maxManaPoints;
			else
				_gameDetails.currentManaPoints += amount;

			_guiMgr.UpdateMana(_gameDetails.currentManaPoints, _gameDetails.maxManaPoints);
		}

		public void ManaSpend(int amount)
		{
			if (_gameDetails.currentManaPoints - amount <= 0)
				_gameDetails.currentManaPoints = 0;
			else
				_gameDetails.currentManaPoints -= amount;

			_guiMgr.UpdateMana(_gameDetails.currentManaPoints, _gameDetails.maxManaPoints);
		}

		public void HealPrincess()
		{
			if (_gameDetails.currentHealth < _gameDetails.heartPoints)
			{
				//Update game details
				_gameDetails.currentHealth += 1;

				_guiMgr.HealOneHeart();
			}
		}

		public bool DoesPrincessNeedRegen()
        {
			if (_gameDetails.currentHealth == _gameDetails.heartPoints
				&& _gameDetails.currentManaPoints == _gameDetails.maxManaPoints)
				return false;

			return true;
        }

		private void KillPrincess()
        {
			_camera.Follow = null;
			_camera.enabled = false;
			_currentState = GameState.Cutscene;

			PrincessSpawnController spawnCtrl = this.GetComponent<PrincessSpawnController>();
			spawnCtrl.StartPrincessDeath(_charCtrl.gameObject.transform.position);

			Destroy(_charCtrl.gameObject);
			_charCtrl = null;

		}

		public void PickupItem(PickUps pickUpType)
        {
            switch (pickUpType)
            {
				case PickUps.RedPotion:

					HealPrincess();
					_charCtrl.HandlePotions(pickUpType);
					break;
				case PickUps.BluePotion:
					ManaPotionUse();
					_charCtrl.HandlePotions(pickUpType);
					break;
				case PickUps.GreenPotion:
					DamagePrincess(Vector3.zero);
					_charCtrl.HandlePotions(pickUpType);
					break;
				case PickUps.Coin:
					_gameDetails.gold += 1;
					_guiMgr.UpdateGoldText(_gameDetails.gold);
					break;
				case PickUps.SilverBar:
					_gameDetails.gold += 10;
					_guiMgr.UpdateGoldText(_gameDetails.gold);
					break;
				case PickUps.GoldBar:
					_gameDetails.gold += 25;
					_guiMgr.UpdateGoldText(_gameDetails.gold);
					break;
				case PickUps.Key:
					_gameDetails.keys += 1;
					_guiMgr.UpdateKeyText(_gameDetails.keys);
					break;
			}
        }

		public void PickupMajorTreasure(MajorTreasures treasure, int treasureId)
        {
			switch(treasure)
            {
				case MajorTreasures.StarShard:
					_gameDetails.starShards++;
					LoadStarShardGui();
					break;
				//case MajorTreasures.Friend:
				//	_gameDetails.friends.Add(treasureId);
				//	break;
				//case MajorTreasures.Hat:
				//	_gameDetails.hats.Add(treasureId);
				//	break;
				//case MajorTreasures.Necklace:
				//	_gameDetails.necklaces.Add(treasureId);
				//	break;
				//case MajorTreasures.Outfit:
				//	_gameDetails.outfits.Add(treasureId);
				//	break;
				//case MajorTreasures.Ring:
				//	_gameDetails.rings.Add(treasureId);
				//	break;
				//case MajorTreasures.Shoes:
				//	_gameDetails.shoes.Add(treasureId);
				//	break;
			}

			//Call Game save
        }

		public int GetCurrentHealth()
        {
			return _gameDetails.currentHealth;
        }

		public void UpdatePlayerGameScene()
        {
			_gameDetails.gameScene = _sceneMgr.GetCurrentScene();
        }

		public void UpdateCameraFollow(GameObject newFollow)
        {
			_camera.Follow = newFollow.transform;
        }

		public void ResumeGameFromMenu()
        {
			_currentState = GameState.Playing;
			LoadGameplayGui();
        }

        #endregion


    }


}
