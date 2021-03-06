// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/PlayerController/Controlls/ThirdPersonPlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @ThirdPersonPlayerControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @ThirdPersonPlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""ThirdPersonPlayerControls"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""0e91b768-1146-4757-9b86-ca92c1a01f33"",
            ""actions"": [
                {
                    ""name"": ""PlayerMovement"",
                    ""type"": ""Value"",
                    ""id"": ""7bd44c8c-e618-4750-a30c-89c47e85b265"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Sprint"",
                    ""type"": ""PassThrough"",
                    ""id"": ""53c0fcac-9ad8-4c0f-9dbd-3e594c4d62c9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Walk"",
                    ""type"": ""PassThrough"",
                    ""id"": ""97cd51a0-54c4-4528-ac65-5c1b0cb92042"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Slide"",
                    ""type"": ""PassThrough"",
                    ""id"": ""c1d47e8e-08df-4be6-9668-85ebc39cbfd8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""fe4f281b-e2f7-4ae9-be09-27b7ea64ee4e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Look"",
                    ""type"": ""Value"",
                    ""id"": ""21db8fab-0295-40aa-b6b1-1721f6fabc5a"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Fire1"",
                    ""type"": ""PassThrough"",
                    ""id"": ""1e453b57-8305-4102-b105-abe842db5952"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Fire2"",
                    ""type"": ""PassThrough"",
                    ""id"": ""09292f6e-8d87-4a7b-9ea5-3b24e8e25927"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Testing"",
                    ""type"": ""PassThrough"",
                    ""id"": ""da3bd7ef-bd2e-4c97-be1e-df04ef3f141f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Ability1"",
                    ""type"": ""PassThrough"",
                    ""id"": ""2b5ca623-ced3-49c0-9c4f-0688ee06eedf"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Arm1"",
                    ""type"": ""PassThrough"",
                    ""id"": ""0b4da781-dce1-498e-8244-000b47372771"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Arm2"",
                    ""type"": ""PassThrough"",
                    ""id"": ""2737794f-cae7-499a-9a0b-dd61bc092d87"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ESC"",
                    ""type"": ""Button"",
                    ""id"": ""e91fc528-26c3-4851-ad91-d503dfe28fbe"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ArmMode"",
                    ""type"": ""Button"",
                    ""id"": ""33fbfd26-a5e4-43d3-908f-42cbd8a8b8f0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""4fb36d8e-33d4-4a7e-9cf4-f7b4f880adb3"",
                    ""path"": ""2DVector(mode=1)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayerMovement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""07beefc9-184c-4252-9fdc-03c9652d8dd6"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""PlayerMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""189559d3-5e76-4128-a0e2-cfe032446bfc"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""PlayerMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""5ac3723c-f936-489f-9108-3805ffe58fd7"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""PlayerMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""3aec5915-7dfd-4530-b917-7f0e356710fd"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""PlayerMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Arrows"",
                    ""id"": ""ce159110-4ff6-4a9d-b5c8-a5e5fec89fff"",
                    ""path"": ""2DVector(mode=1)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayerMovement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""39f983e4-8949-4a5d-a890-3b726287d62e"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""PlayerMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""8f9c74a5-6d8b-4495-b6c8-c096c8bd07d2"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""PlayerMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""cade9528-587b-4ee0-80ea-48be99654046"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""PlayerMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""6ada994c-7acc-4af6-a1a4-bf3b57f3c937"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""PlayerMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""bec4734a-6d8d-4bd0-a8c7-3aeb1a3a36de"",
                    ""path"": ""<XInputController>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""PlayerMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0469fef2-e374-408f-8802-d73b797cf744"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3e72dac6-572b-40d1-ae07-5f9fb726ede3"",
                    ""path"": ""<XInputController>/leftStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d651a22a-3d64-4583-a3a0-f8cb12267e42"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3e6136e4-2f97-4292-9251-28734e37c972"",
                    ""path"": ""<XInputController>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8253cb88-b72f-4ef4-97e6-5806a40021db"",
                    ""path"": ""<Pointer>/delta"",
                    ""interactions"": """",
                    ""processors"": ""InvertVector2(invertX=false),ScaleVector2(x=10,y=10)"",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""136195f6-c19f-49ac-8f75-d64514b1bbb0"",
                    ""path"": ""<XInputController>/rightStick"",
                    ""interactions"": """",
                    ""processors"": ""InvertVector2(invertX=false),ScaleVector2(x=150,y=150)"",
                    ""groups"": ""Controller"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""034c342d-d231-4afc-85bb-5757d71927d3"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""Slide"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""28bfe3bb-0de8-4e41-8a90-3bbd7d7ec948"",
                    ""path"": ""<XInputController>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Slide"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9843f56e-62b8-4c18-8c32-8ce73a19df9d"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""Fire1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""79b919ae-ada3-47ce-ae23-2cf646483452"",
                    ""path"": ""<XInputController>/rightTrigger"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Fire1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""317478ae-0362-484d-86eb-becf67a33639"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""Fire2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fda38254-055e-48cb-8d25-9628903f865d"",
                    ""path"": ""<XInputController>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Fire2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f3f7bf1a-7c43-458a-979d-5e964fc14f43"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""Testing"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cd4e6636-4d59-417b-9b19-2eb38a3a2588"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""Ability1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""325bf257-788d-4ba3-8ddb-cac7d30ccf63"",
                    ""path"": ""<XInputController>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Ability1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1b659542-4f67-4a0f-b635-b5b30390e24f"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""Arm1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3c459eda-1ec9-4906-a060-f621eb6539e1"",
                    ""path"": ""<XInputController>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Arm1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""76ccf4a1-a2b5-41bc-a4e0-d10051618d17"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""Arm2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b1beecee-793f-4fda-bb4b-b5c4c9c4cc40"",
                    ""path"": ""<XInputController>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Arm2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""10c1e96a-70cc-422f-aa7b-74dca8a8b76f"",
                    ""path"": ""<Keyboard>/leftCtrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""Walk"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ba8fb0f1-ffab-43a5-8595-ae76092b9c91"",
                    ""path"": ""<XInputController>/rightStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Walk"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""020ddfae-37a1-42bb-918f-4fa3a50e3c6e"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""ESC"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8631d461-8e96-4dc0-9827-f38a21a770e0"",
                    ""path"": ""<XInputController>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""ESC"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9f2c945f-4fe8-4305-abc7-09030016ad33"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""ArmMode"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fc392514-0b8f-4d60-b2da-d3c1f6cd4039"",
                    ""path"": ""<XInputController>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""ArmMode"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""KeyboardAndMouse"",
            ""bindingGroup"": ""KeyboardAndMouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": true,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": true,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Controller"",
            ""bindingGroup"": ""Controller"",
            ""devices"": [
                {
                    ""devicePath"": ""<XInputController>"",
                    ""isOptional"": true,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_PlayerMovement = m_Player.FindAction("PlayerMovement", throwIfNotFound: true);
        m_Player_Sprint = m_Player.FindAction("Sprint", throwIfNotFound: true);
        m_Player_Walk = m_Player.FindAction("Walk", throwIfNotFound: true);
        m_Player_Slide = m_Player.FindAction("Slide", throwIfNotFound: true);
        m_Player_Jump = m_Player.FindAction("Jump", throwIfNotFound: true);
        m_Player_Look = m_Player.FindAction("Look", throwIfNotFound: true);
        m_Player_Fire1 = m_Player.FindAction("Fire1", throwIfNotFound: true);
        m_Player_Fire2 = m_Player.FindAction("Fire2", throwIfNotFound: true);
        m_Player_Testing = m_Player.FindAction("Testing", throwIfNotFound: true);
        m_Player_Ability1 = m_Player.FindAction("Ability1", throwIfNotFound: true);
        m_Player_Arm1 = m_Player.FindAction("Arm1", throwIfNotFound: true);
        m_Player_Arm2 = m_Player.FindAction("Arm2", throwIfNotFound: true);
        m_Player_ESC = m_Player.FindAction("ESC", throwIfNotFound: true);
        m_Player_ArmMode = m_Player.FindAction("ArmMode", throwIfNotFound: true);
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

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_PlayerMovement;
    private readonly InputAction m_Player_Sprint;
    private readonly InputAction m_Player_Walk;
    private readonly InputAction m_Player_Slide;
    private readonly InputAction m_Player_Jump;
    private readonly InputAction m_Player_Look;
    private readonly InputAction m_Player_Fire1;
    private readonly InputAction m_Player_Fire2;
    private readonly InputAction m_Player_Testing;
    private readonly InputAction m_Player_Ability1;
    private readonly InputAction m_Player_Arm1;
    private readonly InputAction m_Player_Arm2;
    private readonly InputAction m_Player_ESC;
    private readonly InputAction m_Player_ArmMode;
    public struct PlayerActions
    {
        private @ThirdPersonPlayerControls m_Wrapper;
        public PlayerActions(@ThirdPersonPlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @PlayerMovement => m_Wrapper.m_Player_PlayerMovement;
        public InputAction @Sprint => m_Wrapper.m_Player_Sprint;
        public InputAction @Walk => m_Wrapper.m_Player_Walk;
        public InputAction @Slide => m_Wrapper.m_Player_Slide;
        public InputAction @Jump => m_Wrapper.m_Player_Jump;
        public InputAction @Look => m_Wrapper.m_Player_Look;
        public InputAction @Fire1 => m_Wrapper.m_Player_Fire1;
        public InputAction @Fire2 => m_Wrapper.m_Player_Fire2;
        public InputAction @Testing => m_Wrapper.m_Player_Testing;
        public InputAction @Ability1 => m_Wrapper.m_Player_Ability1;
        public InputAction @Arm1 => m_Wrapper.m_Player_Arm1;
        public InputAction @Arm2 => m_Wrapper.m_Player_Arm2;
        public InputAction @ESC => m_Wrapper.m_Player_ESC;
        public InputAction @ArmMode => m_Wrapper.m_Player_ArmMode;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @PlayerMovement.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPlayerMovement;
                @PlayerMovement.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPlayerMovement;
                @PlayerMovement.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPlayerMovement;
                @Sprint.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSprint;
                @Sprint.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSprint;
                @Sprint.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSprint;
                @Walk.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnWalk;
                @Walk.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnWalk;
                @Walk.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnWalk;
                @Slide.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSlide;
                @Slide.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSlide;
                @Slide.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSlide;
                @Jump.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Look.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLook;
                @Look.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLook;
                @Look.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLook;
                @Fire1.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFire1;
                @Fire1.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFire1;
                @Fire1.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFire1;
                @Fire2.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFire2;
                @Fire2.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFire2;
                @Fire2.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFire2;
                @Testing.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTesting;
                @Testing.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTesting;
                @Testing.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTesting;
                @Ability1.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAbility1;
                @Ability1.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAbility1;
                @Ability1.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAbility1;
                @Arm1.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnArm1;
                @Arm1.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnArm1;
                @Arm1.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnArm1;
                @Arm2.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnArm2;
                @Arm2.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnArm2;
                @Arm2.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnArm2;
                @ESC.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnESC;
                @ESC.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnESC;
                @ESC.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnESC;
                @ArmMode.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnArmMode;
                @ArmMode.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnArmMode;
                @ArmMode.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnArmMode;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @PlayerMovement.started += instance.OnPlayerMovement;
                @PlayerMovement.performed += instance.OnPlayerMovement;
                @PlayerMovement.canceled += instance.OnPlayerMovement;
                @Sprint.started += instance.OnSprint;
                @Sprint.performed += instance.OnSprint;
                @Sprint.canceled += instance.OnSprint;
                @Walk.started += instance.OnWalk;
                @Walk.performed += instance.OnWalk;
                @Walk.canceled += instance.OnWalk;
                @Slide.started += instance.OnSlide;
                @Slide.performed += instance.OnSlide;
                @Slide.canceled += instance.OnSlide;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Look.started += instance.OnLook;
                @Look.performed += instance.OnLook;
                @Look.canceled += instance.OnLook;
                @Fire1.started += instance.OnFire1;
                @Fire1.performed += instance.OnFire1;
                @Fire1.canceled += instance.OnFire1;
                @Fire2.started += instance.OnFire2;
                @Fire2.performed += instance.OnFire2;
                @Fire2.canceled += instance.OnFire2;
                @Testing.started += instance.OnTesting;
                @Testing.performed += instance.OnTesting;
                @Testing.canceled += instance.OnTesting;
                @Ability1.started += instance.OnAbility1;
                @Ability1.performed += instance.OnAbility1;
                @Ability1.canceled += instance.OnAbility1;
                @Arm1.started += instance.OnArm1;
                @Arm1.performed += instance.OnArm1;
                @Arm1.canceled += instance.OnArm1;
                @Arm2.started += instance.OnArm2;
                @Arm2.performed += instance.OnArm2;
                @Arm2.canceled += instance.OnArm2;
                @ESC.started += instance.OnESC;
                @ESC.performed += instance.OnESC;
                @ESC.canceled += instance.OnESC;
                @ArmMode.started += instance.OnArmMode;
                @ArmMode.performed += instance.OnArmMode;
                @ArmMode.canceled += instance.OnArmMode;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    private int m_KeyboardAndMouseSchemeIndex = -1;
    public InputControlScheme KeyboardAndMouseScheme
    {
        get
        {
            if (m_KeyboardAndMouseSchemeIndex == -1) m_KeyboardAndMouseSchemeIndex = asset.FindControlSchemeIndex("KeyboardAndMouse");
            return asset.controlSchemes[m_KeyboardAndMouseSchemeIndex];
        }
    }
    private int m_ControllerSchemeIndex = -1;
    public InputControlScheme ControllerScheme
    {
        get
        {
            if (m_ControllerSchemeIndex == -1) m_ControllerSchemeIndex = asset.FindControlSchemeIndex("Controller");
            return asset.controlSchemes[m_ControllerSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnPlayerMovement(InputAction.CallbackContext context);
        void OnSprint(InputAction.CallbackContext context);
        void OnWalk(InputAction.CallbackContext context);
        void OnSlide(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnLook(InputAction.CallbackContext context);
        void OnFire1(InputAction.CallbackContext context);
        void OnFire2(InputAction.CallbackContext context);
        void OnTesting(InputAction.CallbackContext context);
        void OnAbility1(InputAction.CallbackContext context);
        void OnArm1(InputAction.CallbackContext context);
        void OnArm2(InputAction.CallbackContext context);
        void OnESC(InputAction.CallbackContext context);
        void OnArmMode(InputAction.CallbackContext context);
    }
}
