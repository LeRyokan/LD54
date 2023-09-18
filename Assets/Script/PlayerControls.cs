//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.6.3
//     from Assets/Script/PlayerControls.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerControls: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Ground"",
            ""id"": ""8c155aaa-2ec5-4c7b-b34a-b258145f73fd"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""ef4e7cb9-30a6-4dc0-8b5f-6f21a46ff011"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""3fa08627-4262-4e51-aeb7-0bd5c59d439a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""92bb137c-36fa-4073-8b37-09d95f14ccf1"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""04cc320f-c4b5-472b-aeab-2e42b5619e11"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gameplay"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""fdfad721-4150-4fac-96c7-d8efc7882273"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gameplay"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""73eab8b9-006b-44fc-a13f-b452ab2659ae"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gameplay"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""9a4d442d-56cf-4cb2-b9b5-466448456644"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gameplay"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""9ae94b34-ad32-44b0-afe9-562b8d57c58e"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""8bfa8e1c-24c9-4221-b922-e5cbf0c873b9"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gameplay"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""f3738c1c-2471-4999-81ff-0c920555eee0"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gameplay"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""36fe2772-c449-4543-b015-36c009b2d1ab"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gameplay"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""95aa9d14-19c1-438d-92b1-ccff7b2db74c"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gameplay"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""7c3f6836-1666-40c6-9c8b-c86036cdd07d"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gameplay"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cd6ad517-8430-4f0a-a75b-484caa1ae2ca"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gameplay"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Car"",
            ""id"": ""84daf7e3-112d-4fe7-a833-04319da5834a"",
            ""actions"": [
                {
                    ""name"": ""New action"",
                    ""type"": ""Button"",
                    ""id"": ""f7ff42dc-895b-45ca-bd1d-3842e158229e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""5022bdc6-5849-4f26-a4f4-5cfed2477b14"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gameplay"",
                    ""action"": ""New action"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Gameplay"",
            ""bindingGroup"": ""Gameplay"",
            ""devices"": []
        }
    ]
}");
        // Ground
        m_Ground = asset.FindActionMap("Ground", throwIfNotFound: true);
        m_Ground_Move = m_Ground.FindAction("Move", throwIfNotFound: true);
        m_Ground_Jump = m_Ground.FindAction("Jump", throwIfNotFound: true);
        // Car
        m_Car = asset.FindActionMap("Car", throwIfNotFound: true);
        m_Car_Newaction = m_Car.FindAction("New action", throwIfNotFound: true);
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

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Ground
    private readonly InputActionMap m_Ground;
    private List<IGroundActions> m_GroundActionsCallbackInterfaces = new List<IGroundActions>();
    private readonly InputAction m_Ground_Move;
    private readonly InputAction m_Ground_Jump;
    public struct GroundActions
    {
        private @PlayerControls m_Wrapper;
        public GroundActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Ground_Move;
        public InputAction @Jump => m_Wrapper.m_Ground_Jump;
        public InputActionMap Get() { return m_Wrapper.m_Ground; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GroundActions set) { return set.Get(); }
        public void AddCallbacks(IGroundActions instance)
        {
            if (instance == null || m_Wrapper.m_GroundActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_GroundActionsCallbackInterfaces.Add(instance);
            @Move.started += instance.OnMove;
            @Move.performed += instance.OnMove;
            @Move.canceled += instance.OnMove;
            @Jump.started += instance.OnJump;
            @Jump.performed += instance.OnJump;
            @Jump.canceled += instance.OnJump;
        }

        private void UnregisterCallbacks(IGroundActions instance)
        {
            @Move.started -= instance.OnMove;
            @Move.performed -= instance.OnMove;
            @Move.canceled -= instance.OnMove;
            @Jump.started -= instance.OnJump;
            @Jump.performed -= instance.OnJump;
            @Jump.canceled -= instance.OnJump;
        }

        public void RemoveCallbacks(IGroundActions instance)
        {
            if (m_Wrapper.m_GroundActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IGroundActions instance)
        {
            foreach (var item in m_Wrapper.m_GroundActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_GroundActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public GroundActions @Ground => new GroundActions(this);

    // Car
    private readonly InputActionMap m_Car;
    private List<ICarActions> m_CarActionsCallbackInterfaces = new List<ICarActions>();
    private readonly InputAction m_Car_Newaction;
    public struct CarActions
    {
        private @PlayerControls m_Wrapper;
        public CarActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Newaction => m_Wrapper.m_Car_Newaction;
        public InputActionMap Get() { return m_Wrapper.m_Car; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CarActions set) { return set.Get(); }
        public void AddCallbacks(ICarActions instance)
        {
            if (instance == null || m_Wrapper.m_CarActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_CarActionsCallbackInterfaces.Add(instance);
            @Newaction.started += instance.OnNewaction;
            @Newaction.performed += instance.OnNewaction;
            @Newaction.canceled += instance.OnNewaction;
        }

        private void UnregisterCallbacks(ICarActions instance)
        {
            @Newaction.started -= instance.OnNewaction;
            @Newaction.performed -= instance.OnNewaction;
            @Newaction.canceled -= instance.OnNewaction;
        }

        public void RemoveCallbacks(ICarActions instance)
        {
            if (m_Wrapper.m_CarActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(ICarActions instance)
        {
            foreach (var item in m_Wrapper.m_CarActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_CarActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public CarActions @Car => new CarActions(this);
    private int m_GameplaySchemeIndex = -1;
    public InputControlScheme GameplayScheme
    {
        get
        {
            if (m_GameplaySchemeIndex == -1) m_GameplaySchemeIndex = asset.FindControlSchemeIndex("Gameplay");
            return asset.controlSchemes[m_GameplaySchemeIndex];
        }
    }
    public interface IGroundActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
    }
    public interface ICarActions
    {
        void OnNewaction(InputAction.CallbackContext context);
    }
}
