using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace PrincessAdventure
{
	public class GameManager : MonoBehaviour
	{
		[SerializeField] private GuiManager _guiMgr;
		[SerializeField] private CharacterController _charCtrl;
		[SerializeField] private CompanionManager _companionMgr;
		[SerializeField] private CinemachineVirtualCamera _camera;

		public static GameManager GameInstance;

		private GameState _currentState = GameState.Undefined;
		private ActiveGame _gameDetails;
		private float _invincibleUntilTime = 0;

		private bool _controllingCompanion = false;


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

			SetupGame();
		}

        #endregion

        #region start up functions
        private void SetupGame()
        {
			//TODO: Convert to async
			//TODO: Load given scene

			_currentState = GameState.Loading;
			//Load Game Details
			LoadGameDetails();

			//Call UI Setup
			LoadGui();

			//Unpause & Activate game

			_currentState = GameState.Playing;
		}

		private void LoadGameDetails()
		{
			_gameDetails = new ActiveGame(1);

			//Debug.Log(_gameDetails);
		}

		private void LoadGui()
        {
			_guiMgr.LoadGui(_gameDetails);

		}
        #endregion

        #region Gameplay Actions

		public void RouteInputs(PrincessInputActions inputs)
        {
			if(_controllingCompanion) 
				_companionMgr.UpdateNextInputs(inputs);
			else
				_charCtrl.UpdateNextInputs(inputs);
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

		public void HealPrincess()
		{
			if (_gameDetails.currentHealth < _gameDetails.maxHearts)
			{
				//Update game details
				_gameDetails.currentHealth += 1;

				_guiMgr.HealOneHeart();
			}
		}

		private void KillPrincess()
        {
			//TODO: Configure KillPrincess
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
					//TODO: Mana functions
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

		public int GetCurrentHealth()
        {
			return _gameDetails.currentHealth;
        }

        #endregion


    }


}
