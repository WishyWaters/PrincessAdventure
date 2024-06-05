using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

namespace PrincessAdventure
{
	public class GameManager : MonoBehaviour
	{
		[SerializeField] private CompanionManager _companionMgr;
		[SerializeField] private GameObject _princessPrefab;
		[SerializeField] private int _saveId;

		public static GameManager GameInstance;

		private LevelManager _levelMgr;
		private CharacterController _charCtrl;
		private PrincessCustomizerController _customizerCtrl;
		private GameObject _activeCompanion;
		private GameState _currentGameState = GameState.Undefined;
		private ActiveGame _gameDetails;
		private float _invincibleUntilTime = 0;
		private GameScenes _currentScene;

		
		private bool _controllingCompanion = false;
		private float _manaRegenTime;
		private float _playerNoticeTimer;
		private CinemachineVirtualCamera _virtualCamera;

		private bool _pause;

		private bool _showTips;

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
			_showTips = true;
			if (PlayerPrefs.HasKey("Tips"))
				_showTips = PlayerPrefs.GetInt("Tips") == 0 ? false : true;


			SetupGameFromStart();
		}

		private void Update()
        {
			if(GetCurrentGameState() == GameState.Playing)
				ManaRegen();
        }

        #endregion

        #region load functions

		public void StartGame(int saveId)
        {
			ChangeGameState(GameState.Loading);

			_saveId = saveId;

			//Load Game Details
			StartCoroutine(LoadGameThenLoadScene());
		}

        private void SetupGameFromStart()
        {
			ChangeGameState(GameState.Loading);

			GuiManager.GuiInstance.ClearOut();

			SetupLevel();

			if (_levelMgr.GetCurrentLevel() != GameScenes.MainMenu)
			{
				
				//Load Game Details
				LoadGameDetails();

				//Play Cutscene
				SpawnPlayerAtCheckpoint();
			}
		}

		private void SetupLevel()
		{
			//Setup this scene
			_virtualCamera = GameObject.FindGameObjectWithTag("VirtualCamera").GetComponentInChildren<CinemachineVirtualCamera>();
			_virtualCamera.enabled = false;
			_levelMgr = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<LevelManager>();
			_levelMgr.LoadLevelDetails(_saveId);
			_currentScene = _levelMgr.GetCurrentLevel();
			SoundManager.SoundInstance.ChangeMusic(_levelMgr.GetDefaultLevelMusic(), true);

			if (_levelMgr.GetCurrentLevel() == GameScenes.MainMenu)
			{
				ChangeGameState(GameState.Menu);
				GuiManager.GuiInstance.LoadMainMenuGui();
			}
			else if(_levelMgr.GetCurrentLevel() == GameScenes.CharCreator)
			{
				//ChangeGameState(GameState.Menu);
				GuiManager.GuiInstance.LoadCharCreator();
			}
		}

		private void SpawnPlayerAtCheckpoint(bool isNewgame = false)
        {
			ChangeGameState(GameState.Cutscene);
			FullRestoreNoEffects();

			GameObject checkPoint = _levelMgr.GetLevelCheckPoint();
			_virtualCamera.Follow = checkPoint.transform;
			_virtualCamera.enabled = true;

			PrincessSpawnController spawnCtrl = this.GetComponent<PrincessSpawnController>();

			if (isNewgame)
            {
				Vector3 spawnPosition = checkPoint.transform.position - new Vector3(0, 1, 0);

				spawnCtrl.CreatePrincessSpawn(spawnPosition);
            }
				
			else
				spawnCtrl.StartPrincessSpawn(checkPoint.transform.position);
			
		}

		public void LoadPlayer(Vector3 spawnPosition, Vector2 faceDirection, bool activatePrincess = true)
        {
			GameObject princess = Instantiate(_princessPrefab, spawnPosition, new Quaternion());
			_charCtrl = princess.GetComponent<CharacterController>();
			_customizerCtrl = princess.GetComponent<PrincessCustomizerController>();

			_customizerCtrl.SetStyle(_gameDetails.princessStyle);
			_customizerCtrl.SetEquipment(_gameDetails.equipment, _gameDetails.princessStyle);

			if(activatePrincess)
				ActivatePrincess(false, faceDirection);
		}


		private bool LoadGameDetails()
		{
			GameDetails savedGame = SaveDataManager.LoadGameDetails(_saveId);
			bool isNewGame = false;

			if (savedGame == null)
            {
				_gameDetails = new ActiveGame();
				isNewGame = true;
			}
			else
				_gameDetails = new ActiveGame(savedGame);


			FullRestoreNoEffects();
			return isNewGame;
		}

		private void FullRestoreNoEffects()
        {
			_gameDetails.RecalculateMaxStats();
			_gameDetails.currentHealth = _gameDetails.maxHealth;
			_gameDetails.currentManaPoints = _gameDetails.maxManaPoints;
		}

		

		public int GetSaveId()
        {
			return _saveId;
        }

		public void MoveToSavedScene( FadeTypes fade)
		{
			ChangeGameState(GameState.Loading);

			StartCoroutine(LoadSceneWithCurrentGame(_gameDetails.gameScene, _gameDetails.loadLocationIndex, fade));

		}

		public void MoveToNewScene(GameScenes targetScene, int targetIndex, FadeTypes fade)
        {
			ChangeGameState(GameState.Loading);

			StartCoroutine(LoadSceneWithCurrentGame(targetScene, targetIndex, fade));

        }

		public void LoadMainMenu()
		{
			ChangeGameState(GameState.Loading);

			StartCoroutine(LoadMainMenuScene());

		}

		private IEnumerator LoadGameThenLoadScene()
        {
			float fadeTime = .6f;
			float currentTimer = 0f;

			//Fade out
			GuiManager.GuiInstance.FillToBlack(FadeTypes.Enter, .3f);
			while (currentTimer < fadeTime)
			{
				currentTimer += Time.unscaledDeltaTime;
				yield return null;
			}

			GuiManager.GuiInstance.DeactivateAllExceptFade();

			bool isNewgame = LoadGameDetails();

			if (isNewgame)
				_currentScene = GameScenes.CharCreator;
			else
				_currentScene = _gameDetails.gameScene;

			//load next scene
			AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(_currentScene.ToString());

			// Wait until the asynchronous scene fully loads
			while (!asyncLoad.isDone)
			{
				yield return null;
			}


			//Setup New scene
			SetupLevel();

			//Play Cutscene
			SpawnPlayerAtCheckpoint(isNewgame);

			//Call fade in
			currentTimer = 0;
			GuiManager.GuiInstance.FillToClear(FadeTypes.Enter, fadeTime);
			while (currentTimer < fadeTime)
			{
				currentTimer += Time.unscaledDeltaTime;
				yield return null;
			}

		}

		private IEnumerator LoadSceneWithCurrentGame(GameScenes targetScene, int targetIndex, FadeTypes fade)
		{
			float fadeTime = .6f;
			float currentTimer = 0f;

			//Fade out
			GuiManager.GuiInstance.FillToBlack(fade, .3f);
			while (currentTimer < fadeTime)
			{
				currentTimer += Time.unscaledDeltaTime;
				yield return null;
			}

			GuiManager.GuiInstance.DeactivateAllExceptFade();

			//save scene data
			SaveDataManager.SaveGameDetails(_saveId, _gameDetails);
			_levelMgr.SaveLevelDetails(_saveId);

			//load next scene
			AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(targetScene.ToString());

			// Wait until the asynchronous scene fully loads
			while (!asyncLoad.isDone)
			{
				yield return null;
			}


			//Setup New scene
			SetupLevel();

			Vector2 facing = new Vector2();
			switch(fade)
            {
				case FadeTypes.Left:
					facing = Vector2.left;
					break;
				case FadeTypes.Right:
					facing = Vector2.right;
					break;
				case FadeTypes.Up:
				case FadeTypes.Enter:
					facing = Vector2.up;
					break;
				case FadeTypes.Down:
				case FadeTypes.Exit:
				default:
					facing = Vector2.down;
					break;

			}

			LoadPlayer(_levelMgr.GetLevelEntryPoint(targetIndex).position, facing);

			//wait a frame after level load
			bool continueLoad = true;
			while (continueLoad)
			{
				continueLoad = false ;
				yield return null;
			}

			//Call fade in
			currentTimer = 0;
			GuiManager.GuiInstance.FillToClear(fade, fadeTime);
			while (currentTimer < fadeTime)
			{
				currentTimer += Time.unscaledDeltaTime;
				yield return null;
			}

			ResumeGameplay();
		}

		private IEnumerator LoadMainMenuScene()
		{
			float fadeTime = .6f;
			float currentTimer = 0f;

			//Fade out
			GuiManager.GuiInstance.FillToBlack(FadeTypes.Exit, .3f);
			while (currentTimer < fadeTime)
			{
				currentTimer += Time.unscaledDeltaTime;
				yield return null;
			}

			GuiManager.GuiInstance.DeactivateAllExceptFade();

			//save scene data
			SaveDataManager.SaveGameDetails(_saveId, _gameDetails);
			_levelMgr.SaveLevelDetails(_saveId);

			//load next scene
			AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(GameScenes.MainMenu.ToString());

			// Wait until the asynchronous scene fully loads
			while (!asyncLoad.isDone)
			{
				yield return null;
			}


			//Setup New scene
			SetupLevel();

			//wait a frame after level load
			bool continueLoad = true;
			while (continueLoad)
			{
				continueLoad = false;
				yield return null;
			}

			//Call fade in
			currentTimer = 0;
			GuiManager.GuiInstance.FillToClear(FadeTypes.Exit, fadeTime);
			while (currentTimer < fadeTime)
			{
				currentTimer += Time.unscaledDeltaTime;
				yield return null;
			}

			ChangeGameState(GameState.Menu);
		}

		private IEnumerator ReloadCurrentSceneAtCheckpoint()
		{
			float fadeTime = .6f;
			float currentTimer = 0f;

			//Fade out
			GuiManager.GuiInstance.FillToBlack(FadeTypes.Enter, .3f);
			while (currentTimer < fadeTime)
			{
				currentTimer += Time.unscaledDeltaTime;
				yield return null;
			}

			//save scene data
			SaveDataManager.SaveGameDetails(_saveId, _gameDetails);
			_levelMgr.SaveLevelDetails(_saveId);

			//load next scene
			AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(_gameDetails.gameScene.ToString());

			// Wait until the asynchronous scene fully loads
			while (!asyncLoad.isDone)
			{
				yield return null;
			}

			//wait a frame after level load
			bool continueLoad = true;
			while (continueLoad)
			{
				continueLoad = false;
				yield return null;
			}

			//Setup New scene
			SetupLevel();

			//Play Cutscene
			SpawnPlayerAtCheckpoint();

			//Call fade in
			currentTimer = 0;
			GuiManager.GuiInstance.FillToClear(FadeTypes.Enter, fadeTime);
			while (currentTimer < fadeTime)
			{
				currentTimer += Time.unscaledDeltaTime;
				yield return null;
			}

		}


		public void ContinueGame()
        {
			ChangeGameState(GameState.Loading);
			GuiManager.GuiInstance.DeactivateAllGui();

			StartCoroutine(ReloadCurrentSceneAtCheckpoint());

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

		#region Customization functions


		public PrincessStyle GetPrincessStyle()
		{
			return _gameDetails.princessStyle;
		}

		public void SetBodyColor(PrincessSkinColor newBodyColor)
		{
			//update player
			_customizerCtrl.SetBodyColor(newBodyColor);

			_gameDetails.princessStyle.bodyColor = (int)newBodyColor;
		}

		public void SetHairStyle(PrincessHairStyles hairStyle)
		{
			//update player
			_customizerCtrl.SetHairStyle(hairStyle);

			_gameDetails.princessStyle.hairStyle = (int)hairStyle;

			SetPrincessEquipment(_gameDetails.equipment);

		}

		public void SetHairColor(PrincessHairColor newHairColor)
		{
			//update player
			_customizerCtrl.SetHairColor(newHairColor);

			_gameDetails.princessStyle.hairColor = (int)newHairColor;

		}

		public void SetEyeShape(PrincessEyeShapes eyes)
		{
			//update player
			_customizerCtrl.SetEyeShape(eyes);

			_gameDetails.princessStyle.eyeStyle = (int)eyes;

		}

		public void SetEyeColor(PrincessEyeColor eyeColor)
		{
			//update player
			_customizerCtrl.SetEyeColor(eyeColor);

			_gameDetails.princessStyle.eyeColor = (int)eyeColor;

		}

		public void SetGlassesStyle(PrincessGlassesStyle glassesStyle)
		{
			//update player
			_customizerCtrl.SetGlassesShape(glassesStyle);

			_gameDetails.princessStyle.glassesStyle = (int)glassesStyle;

		}

		public void SetGlassesColor(PrincessGlassesColor color)
		{
			//update player
			_customizerCtrl.SetGlassesColor(color);

			_gameDetails.princessStyle.glassesColor = (int)color;

		}

		public PrincessEquipment GetPrincessEquipment()
        {
			return _gameDetails.equipment;
        }

		public void SetPrincessEquipment(PrincessEquipment newEquipment)
        {
			_gameDetails.equipment = newEquipment;

			//Update Stats
			_gameDetails.RecalculateMaxStats();
			GuiManager.GuiInstance.UpdateGameplayGui(_gameDetails);

			//Call Princess customizer to update character
			_customizerCtrl.SetEquipment(newEquipment, _gameDetails.princessStyle);
		}

		public void SavePrincessCustomization()
        {

        }

		#endregion

		#region GUI functions
		public void UpdateShowTips(bool isOn)
        {
			_showTips = isOn;
		}

		public bool GetShowTips()
		{
			return _showTips;
		}

		public void LoadDefeatGui()
		{
			ChangeGameState(GameState.Defeated);
			GuiManager.GuiInstance.LoadDefeatGui();
		}

		public void LoadStarShardGui()
		{
			ChangeGameState(GameState.Menu);
			GuiManager.GuiInstance.LoadStarShardGui(_gameDetails.starShards);
		}

		public void LoadUniqueItemGui(PickUps item)
		{
			ChangeGameState(GameState.Menu);
			GuiManager.GuiInstance.LoadUniqueItemGui(item);
		}

		public void LoadUniqueItemGui(MajorTreasures treasureType, int treasureId)
		{
			ChangeGameState(GameState.Menu);
			GuiManager.GuiInstance.LoadUniqueItemGui(treasureType, treasureId);
		}

		private void LoadGameplayGui()
		{
			GuiManager.GuiInstance.LoadGameplayGui(_gameDetails);

		}

		public void LoadPowerUpGui()
		{
			ChangeGameState(GameState.Menu);
			GuiManager.GuiInstance.LoadPowerUpGui();
		}

		public void StartTimerGui(int time)
		{
			GuiManager.GuiInstance.StartTimerGui(time);
		}

		public void UpdateTimerText(int time)
		{
			GuiManager.GuiInstance.UpdateTimerText(time);
		}

		public void EndTimerGui()
		{
			GuiManager.GuiInstance.EndTimerGui();
		}

		public void LoadMessageGui(MessageModels.Message msg, string title)
        {
			ChangeGameState(GameState.Menu);

			GuiManager.GuiInstance.LoadMessageGui(msg, title);
        }

		public string GetLevelMessage(int msgId)
        {
			return _levelMgr.GetMessageText(msgId);
		}

		public bool HasItem(int itemType, int itemId)
        {
			bool has = false;
			switch(itemType)
            {
				case 0:
					return _gameDetails.equipment.friends.Contains(itemId);
				case 1:
					return _gameDetails.equipment.hats.Contains(itemId);
				case 2:
					return _gameDetails.equipment.necklaces.Contains(itemId);
				case 3:
					return _gameDetails.equipment.outfits.Contains(itemId);
				case 4:
					return _gameDetails.equipment.rings.Contains(itemId);
				case 5:
					return _gameDetails.equipment.shoes.Contains(itemId);
				case 6:
					return _gameDetails.hasMagic;
				case 7:
					return _gameDetails.hasFireball;
				case 8:
					return _gameDetails.hasFade;
				case 9:
					return _gameDetails.hasBomb;
				case 10:
					return _gameDetails.hasVision;
				case 11:
					return _gameDetails.hasSummon;
			}
			
			return has;
        }

		#endregion

		#region Gameplay Actions

		private void ChangeGameState(GameState newState)
        {
			switch(newState)
            {
				case GameState.Menu:
				case GameState.Defeated:
					Time.timeScale = 0;
					break;
				default:
					Time.timeScale = 1;
					break;
            }

			_currentGameState = newState;

		}

		public GameState GetCurrentGameState()
        {
			return _currentGameState;
        }

		public void RouteInputs(PrincessInputActions inputs)
        {
			
			if (GetCurrentGameState() == GameState.Playing  && (_currentScene != GameScenes.MainMenu && _currentScene != GameScenes.CharCreator))
			{

				if (_controllingCompanion)
					_companionMgr.UpdateNextInputs(inputs);
				else
				{
					inputs.InputDropBomb = inputs.InputDropBomb && _gameDetails.hasBomb;
					inputs.InputFade = inputs.InputFade && _gameDetails.hasFade;
					inputs.InputHoldBomb = inputs.InputHoldBomb && _gameDetails.hasBomb;
					inputs.InputMagicCast = inputs.InputMagicCast && _gameDetails.hasMagic;
					inputs.InputSummonComplete = inputs.InputSummonComplete && _gameDetails.hasSummon && _gameDetails.equipment.selectedFriend > 0;
					inputs.InputSummoning = inputs.InputSummoning && _gameDetails.hasSummon && _gameDetails.equipment.selectedFriend > 0; ;
					inputs.InputThrowFireball = inputs.InputThrowFireball && _gameDetails.hasFireball;

					_charCtrl.UpdateNextInputs(inputs);
				}
				
			}
		}

		public void RoutePauseInput(bool techPause, bool gamePause)
        {
			if (_currentScene == GameScenes.MainMenu)
				return;

			if (_currentScene == GameScenes.CharCreator)
				return;

			if (_currentGameState == GameState.Defeated)
				return;


			if (_currentGameState == GameState.Menu && _pause)
			{
				if (techPause || gamePause)
				{
					SoundManager.SoundInstance.PlayUiEventBig();

					_virtualCamera.m_Lens.OrthographicSize = 5f;
					ResumeGameplay();
				}
			}
			else
			{
				if (techPause)
				{
					SoundManager.SoundInstance.PlayUiEventBig();

					_pause = true;
					ChangeGameState(GameState.Menu);
					GuiManager.GuiInstance.LoadTechnicalPause();
				}
				else if (gamePause)
				{
					SoundManager.SoundInstance.PlayUiEventBig();

					_virtualCamera.m_Lens.OrthographicSize = 4f;
					_pause = true;
					ChangeGameState(GameState.Menu);
					if(_gameDetails.starShards >= 5)
						GuiManager.GuiInstance.LoadPowerUpGui();
					else
						GuiManager.GuiInstance.LoadGamePause();
				}
			}
			

		}

		public void ActivateCompanion(Vector3 targetPosition)
        {

			GameObject companion = _companionMgr.HandleCompanionActivation(_gameDetails.equipment.selectedFriend, targetPosition);
			ControlCompanion(companion);
		}

		public void ActivatePrincess(bool unsummon, Vector2 faceDirection = new Vector2())
		{
			bool doWakeUpAnim = false;
			_virtualCamera.enabled = true;
			_virtualCamera.Follow = _charCtrl.gameObject.transform;
			_virtualCamera.OnTargetObjectWarped(_charCtrl.gameObject.transform, _charCtrl.gameObject.transform.position - _virtualCamera.gameObject.transform.position);

			if (unsummon)
			{
				doWakeUpAnim = true;
				//_virtualCamera.OnTargetObjectWarped(_charCtrl.gameObject.transform, _charCtrl.gameObject.transform.position - _activeCompanion.transform.position);
				_companionMgr.DestroyCurrentSummon();
			}
			
			ControlPrincess(faceDirection, doWakeUpAnim);
		}

		private void ControlPrincess(Vector2 faceDirection, bool doWakeUp)
		{
			_activeCompanion = null;
			_controllingCompanion = false;
			_charCtrl.EnableController(faceDirection, doWakeUp);
			_companionMgr.SetIgnoreInputs(true);

		}

		private void ControlCompanion(GameObject companion)
		{
			_activeCompanion = companion;
			_controllingCompanion = true;
			_charCtrl.DisableController();
			_companionMgr.SetIgnoreInputs(false);

			_virtualCamera.Follow = companion.transform;

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

				GuiManager.GuiInstance.EmptyOneHeart();

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

				GuiManager.GuiInstance.UpdateGoldText(_gameDetails.gold);

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

			GuiManager.GuiInstance.UpdateMana(_gameDetails.currentManaPoints, _gameDetails.maxManaPoints);
		}

		public void ManaSpend(int amount)
		{
			if (_gameDetails.currentManaPoints - amount <= 0)
				_gameDetails.currentManaPoints = 0;
			else
				_gameDetails.currentManaPoints -= amount;

			GuiManager.GuiInstance.UpdateMana(_gameDetails.currentManaPoints, _gameDetails.maxManaPoints);
		}

		public void HealPrincess()
		{
			if (_gameDetails.currentHealth < _gameDetails.maxHealth)
			{
				//Update game details
				_gameDetails.currentHealth += 1;

				GuiManager.GuiInstance.HealOneHeart();
			}
		}

		public bool DoesPrincessNeedRegen()
        {
			if (_gameDetails.currentHealth == _gameDetails.maxHealth
				&& _gameDetails.currentManaPoints == _gameDetails.maxManaPoints)
				return false;

			return true;
        }

		private void KillPrincess()
        {
			_virtualCamera.Follow = null;
			_virtualCamera.enabled = false;
			ChangeGameState(GameState.Cutscene);

			PrincessSpawnController spawnCtrl = this.GetComponent<PrincessSpawnController>();
			spawnCtrl.StartPrincessDeath(_charCtrl.gameObject.transform.position);

			Destroy(_charCtrl.gameObject);
			_charCtrl = null;

		}

		public bool HasKey()
        {
			if (_gameDetails.keys > 0)
				return true;

			return false;
        }

		public void UseKey()
        {
			if (_gameDetails.keys > 0)
            {
				_gameDetails.keys -= 1;

			}

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
					GuiManager.GuiInstance.UpdateGoldText(_gameDetails.gold);
					break;
				case PickUps.SilverBar:
					_gameDetails.gold += 10;
					GuiManager.GuiInstance.UpdateGoldText(_gameDetails.gold);
					break;
				case PickUps.GoldBar:
					_gameDetails.gold += 25;
					GuiManager.GuiInstance.UpdateGoldText(_gameDetails.gold);
					break;
				case PickUps.Key:
					_gameDetails.keys += 1;
					GuiManager.GuiInstance.UpdateKeyText(_gameDetails.keys);
					break;
				case PickUps.StarShard:
					_gameDetails.starShards++;
					LoadStarShardGui();
					break;
				case PickUps.Staff:
					_gameDetails.hasMagic = true;
					LoadUniqueItemGui(PickUps.Staff);
					break;
				case PickUps.Candle:
					_gameDetails.hasFireball = true;
					LoadUniqueItemGui(PickUps.Candle);
					break;
				case PickUps.Skull:
					_gameDetails.hasFade = true;
					LoadUniqueItemGui(PickUps.Skull);
					break;
				case PickUps.Book:
					_gameDetails.hasBomb = true;
					LoadUniqueItemGui(PickUps.Book);
					break;
				case PickUps.Soup:
					_gameDetails.hasSummon = true;
					LoadUniqueItemGui(PickUps.Soup);
					break;
				case PickUps.Gemstone:
					_gameDetails.hasVision = true;
					LoadUniqueItemGui(PickUps.Gemstone);
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
				case MajorTreasures.Friend:
					_gameDetails.equipment.friends.Add(treasureId);
					LoadUniqueItemGui(treasure, treasureId);
					break;
				case MajorTreasures.Hat:
                    _gameDetails.equipment.hats.Add(treasureId);
					LoadUniqueItemGui(treasure, treasureId);
					break;
                case MajorTreasures.Necklace:
                    _gameDetails.equipment.necklaces.Add(treasureId);
					LoadUniqueItemGui(treasure, treasureId);
					break;
                case MajorTreasures.Outfit:
                    _gameDetails.equipment.outfits.Add(treasureId);
                    break;
                case MajorTreasures.Ring:
                    _gameDetails.equipment.rings.Add(treasureId);
					LoadUniqueItemGui(treasure, treasureId);
					break;
                case MajorTreasures.Shoes:
                    _gameDetails.equipment.shoes.Add(treasureId);
					LoadUniqueItemGui(treasure, treasureId);
					break;
            }

			//Call Game save
        }

		public int GetCurrentHealth()
        {
			return _gameDetails.currentHealth;
        }

		public void UpdatePlayerGameScene()
        {
			_gameDetails.gameScene = _levelMgr.GetCurrentLevel();
        }

		public void UpdateCameraFollow(GameObject newFollow)
        {
			_virtualCamera.Follow = newFollow.transform;
        }

		public void UpdateCameraFollowToPlayer()
		{
			if(_controllingCompanion)
				_virtualCamera.Follow = _activeCompanion.transform;
			else
				_virtualCamera.Follow = _charCtrl.gameObject.transform;

		}

		public void PowerUpPrincess(PowerUpOptions powerUp)
        {
			if (_gameDetails.starShards >= 5)
			{
				_gameDetails.starShards -= 5;

				switch (powerUp)
				{
					case PowerUpOptions.Heart:
						_gameDetails.heartPoints++;
						_gameDetails.RecalculateMaxStats();
						_gameDetails.currentHealth = _gameDetails.maxHealth;
						break;
					case PowerUpOptions.Magic:
						_gameDetails.magicPoints++;
						_gameDetails.RecalculateMaxStats();
						_gameDetails.currentManaPoints = _gameDetails.maxManaPoints;
						break;
				}
			}


			ResumeGameplay();

		}

		public void ResumeGameplay()
        {
			if(_currentScene == GameScenes.CharCreator)
            {
				ActivateCharCreator();
            }
			else
            {
				_pause = false;

				LoadGameplayGui();
				ChangeGameState(GameState.Playing);

            }


		}

		public void ActivateCharCreator()
		{
			//_pause = true;

			_virtualCamera.m_Lens.OrthographicSize = 3f;
			
			//_virtualCamera.transform.position.y += 1;
			ChangeGameState(GameState.Playing);

		}

		public int GetPlayerNoticeModifier()
        {
			if (_playerNoticeTimer > Time.time)
				return 2;

			return 0;
        }

		public void PlayerIsNoticeable()
        {
			_playerNoticeTimer = Time.time + 3f;

		}

		public void TeleportPlayerWithinScene(Vector3 target, FadeTypes fade)
        {

			ChangeGameState(GameState.Menu);

			StartCoroutine(TeleportPlayerWithinSceneWithFillGui(target, fade));


        }

		#endregion

		#region Coroutines
		private IEnumerator TeleportPlayerWithinSceneWithFillGui(Vector3 target, FadeTypes fade)
        {
			float fadeTime = .3f;
			float currentTimer = 0f;

			//Call fade out
			GuiManager.GuiInstance.FillToBlack(fade, fadeTime);

			while(currentTimer < fadeTime)
            {
				currentTimer += Time.unscaledDeltaTime;
				yield return null;
            }

			//perform warp
			if (_controllingCompanion)
			{
				_virtualCamera.OnTargetObjectWarped(_activeCompanion.transform, target - _activeCompanion.transform.position);
				_activeCompanion.transform.position = target;
			}
			else
			{
				_virtualCamera.OnTargetObjectWarped(_charCtrl.gameObject.transform, target - _charCtrl.gameObject.transform.position);
				_charCtrl.gameObject.transform.position = target;

			}

			//Call fade in
			GuiManager.GuiInstance.FillToClear(fade, fadeTime);
			currentTimer = 0f;
			while (currentTimer < fadeTime)
			{
				currentTimer += Time.unscaledDeltaTime;
				yield return null;
			}

			ChangeGameState(GameState.Playing);
		}

		#endregion


	}


}
