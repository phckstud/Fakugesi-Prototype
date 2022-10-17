using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

[RequireComponent(typeof(PlayerInput))]
	public class InputManager : Singleton<InputManager>
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public bool jump;
		public bool doubleJump; 
		public bool slide;
		public bool shoot;
		public bool onInteract;
		public bool onConversationEnter;
		public bool back;
		public bool onExit;

		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;


		public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnJump(InputValue value)
		{
			JumpInput(value.isPressed);
		}
		public void OnDoubleJump(InputValue value)
		{
			DoubleJumpInput(value.isPressed);
		}
		public void OnSlide(InputValue value)
		{
			SlideInput(value.isPressed);
		}
		
		public void OnShoot(InputValue value)
		{
			ShootInput(value.isPressed);
		}
		public void OnInteract(InputValue value)
		{
			InteractionInput(value.isPressed);
		}
		public void OnDialogueStart(InputValue value)
		{
			DialogueEnterInput(value.isPressed);
		}

		public void OnBack(InputValue value)
		{
			BackInput(value.isPressed);
		}

		public void OnExitGame(InputValue value)
		{
			ExitGameCondition(value.isPressed);
		}


		public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		} 

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		public void DoubleJumpInput(bool newDoubleJumpState)
		{
			doubleJump = newDoubleJumpState;
		}

		public void ShootInput(bool newShootState)
		{
			shoot = newShootState;
		}
		public void SlideInput(bool newSlideState)
		{
			slide = newSlideState;
		}

		public void DialogueEnterInput(bool newInteractState)
		{
			onConversationEnter = newInteractState;
		}

		public void InteractionInput(bool newInteractState)
		{
			onInteract = newInteractState;
		}

		public void BackInput(bool newBackInput)
		{
			back = newBackInput;
		}

		public void ExitGameCondition(bool newExitGameInput)
		{
			onExit = newExitGameInput;
		}


		/*public void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}*/
	}
	
