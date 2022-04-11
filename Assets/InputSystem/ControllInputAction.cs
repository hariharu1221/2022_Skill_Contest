// GENERATED AUTOMATICALLY FROM 'Assets/InputSystem/ControllInputAction.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @ControllInputAction : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @ControllInputAction()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""ControllInputAction"",
    ""maps"": [
        {
            ""name"": ""InputAction_Player"",
            ""id"": ""93158a85-1d10-4079-a6ef-679c2b38effd"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""49764a05-77a2-46a1-9435-add82763ea20"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": ""StickDeadzone"",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LowFlying"",
                    ""type"": ""Button"",
                    ""id"": ""b1ad6779-8187-493d-b3ad-5c111c0fa047"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Boosting"",
                    ""type"": ""Button"",
                    ""id"": ""db8e4614-9ebc-459e-b548-427787514f62"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Rotation"",
                    ""type"": ""Button"",
                    ""id"": ""0787decc-d798-4dd9-9c72-96a90052706d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""a8fef154-617d-4b39-9970-a0ad167982bf"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LowFlying"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""a4903794-e441-4bb7-a3d5-cf60a8a93578"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""aead116d-1b2c-4637-9e3e-f1ca6a613aac"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""62dc655c-e286-4ea9-a97c-b0607206b0c8"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""99810320-e484-4c3a-957d-fa2d6e7c7273"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""de1d7123-c043-4019-a0c0-b5fa1d95bb4d"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Arrow"",
                    ""id"": ""791100e4-ef3b-4746-b87f-a092c8a04ebf"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""85e5c7d4-8205-4755-ac7c-4b18e63b9be3"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""8e858417-7a3b-47cb-8dc8-6f825f24519f"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""bda3683c-3da3-4601-9624-770143cb5219"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""ebbdc38d-9bcc-436f-a810-47c92ad95023"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""d48c4d6b-fa00-4e07-aaa6-fda85742f8af"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Boosting"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6ee3d2dd-8a7a-42af-8028-772ed229426a"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // InputAction_Player
        m_InputAction_Player = asset.FindActionMap("InputAction_Player", throwIfNotFound: true);
        m_InputAction_Player_Movement = m_InputAction_Player.FindAction("Movement", throwIfNotFound: true);
        m_InputAction_Player_LowFlying = m_InputAction_Player.FindAction("LowFlying", throwIfNotFound: true);
        m_InputAction_Player_Boosting = m_InputAction_Player.FindAction("Boosting", throwIfNotFound: true);
        m_InputAction_Player_Rotation = m_InputAction_Player.FindAction("Rotation", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // InputAction_Player
    private readonly InputActionMap m_InputAction_Player;
    private IInputAction_PlayerActions m_InputAction_PlayerActionsCallbackInterface;
    private readonly InputAction m_InputAction_Player_Movement;
    private readonly InputAction m_InputAction_Player_LowFlying;
    private readonly InputAction m_InputAction_Player_Boosting;
    private readonly InputAction m_InputAction_Player_Rotation;
    public struct InputAction_PlayerActions
    {
        private @ControllInputAction m_Wrapper;
        public InputAction_PlayerActions(@ControllInputAction wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_InputAction_Player_Movement;
        public InputAction @LowFlying => m_Wrapper.m_InputAction_Player_LowFlying;
        public InputAction @Boosting => m_Wrapper.m_InputAction_Player_Boosting;
        public InputAction @Rotation => m_Wrapper.m_InputAction_Player_Rotation;
        public InputActionMap Get() { return m_Wrapper.m_InputAction_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(InputAction_PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IInputAction_PlayerActions instance)
        {
            if (m_Wrapper.m_InputAction_PlayerActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_InputAction_PlayerActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_InputAction_PlayerActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_InputAction_PlayerActionsCallbackInterface.OnMovement;
                @LowFlying.started -= m_Wrapper.m_InputAction_PlayerActionsCallbackInterface.OnLowFlying;
                @LowFlying.performed -= m_Wrapper.m_InputAction_PlayerActionsCallbackInterface.OnLowFlying;
                @LowFlying.canceled -= m_Wrapper.m_InputAction_PlayerActionsCallbackInterface.OnLowFlying;
                @Boosting.started -= m_Wrapper.m_InputAction_PlayerActionsCallbackInterface.OnBoosting;
                @Boosting.performed -= m_Wrapper.m_InputAction_PlayerActionsCallbackInterface.OnBoosting;
                @Boosting.canceled -= m_Wrapper.m_InputAction_PlayerActionsCallbackInterface.OnBoosting;
                @Rotation.started -= m_Wrapper.m_InputAction_PlayerActionsCallbackInterface.OnRotation;
                @Rotation.performed -= m_Wrapper.m_InputAction_PlayerActionsCallbackInterface.OnRotation;
                @Rotation.canceled -= m_Wrapper.m_InputAction_PlayerActionsCallbackInterface.OnRotation;
            }
            m_Wrapper.m_InputAction_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @LowFlying.started += instance.OnLowFlying;
                @LowFlying.performed += instance.OnLowFlying;
                @LowFlying.canceled += instance.OnLowFlying;
                @Boosting.started += instance.OnBoosting;
                @Boosting.performed += instance.OnBoosting;
                @Boosting.canceled += instance.OnBoosting;
                @Rotation.started += instance.OnRotation;
                @Rotation.performed += instance.OnRotation;
                @Rotation.canceled += instance.OnRotation;
            }
        }
    }
    public InputAction_PlayerActions @InputAction_Player => new InputAction_PlayerActions(this);
    public interface IInputAction_PlayerActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnLowFlying(InputAction.CallbackContext context);
        void OnBoosting(InputAction.CallbackContext context);
        void OnRotation(InputAction.CallbackContext context);
    }
}
