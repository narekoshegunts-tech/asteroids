using System;
using MVVM;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Game.Scripts.UI.Binders
{
    public sealed class MonoViewBinder : MonoBehaviour
    {
        private enum BindingMode
        {
            FromInstance = 0,
            FromResolve = 1,
            FromResolveId = 2
        }

        [SerializeField]
        private BindingMode viewBinding;

        [SerializeField]
        private Object view;
        
#if UNITY_EDITOR
        [SerializeField]
        private MonoScript viewType;
#endif
        
        [SerializeField, HideInInspector]
        private string viewTypeFullName;

        [SerializeField]
        private string viewId;

        [Space(8)]
        [SerializeField]
        private BindingMode viewModelBinding;

        [SerializeField]
        private Object viewModel;

#if UNITY_EDITOR
        [SerializeField]
        private MonoScript viewModelType;
#endif

        [SerializeField, HideInInspector]
        private string viewModelTypeFullName;

        [SerializeField]
        private string viewModelId;
        
        private DiContainer _diContainer;

        private IBinder _binder;
        
        [Inject]
        private void Construct(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        private void Awake()
        {
            _binder = CreateBinder();
        }

        private void OnEnable()
        {
            _binder?.Bind();
        }

        private void OnDisable()
        {
            _binder?.Unbind();
        }

        private IBinder CreateBinder()
        {
            object viewObj = viewBinding switch
            {
                BindingMode.FromInstance => view,
                BindingMode.FromResolve => ResolveType(viewTypeFullName),
                BindingMode.FromResolveId => ResolveTypeWithId(viewTypeFullName, viewId),
                _ => throw new Exception($"Unknown view binding mode: {viewBinding}")
            };

            object modelObj = viewModelBinding switch
            {
                BindingMode.FromInstance => viewModel,
                BindingMode.FromResolve => ResolveType(viewModelTypeFullName),
                BindingMode.FromResolveId => ResolveTypeWithId(viewModelTypeFullName, viewModelId),
                _ => throw new Exception($"Unknown viewModel binding mode: {viewModelBinding}")
            };

            return BinderFactory.CreateComposite(viewObj, modelObj);
        }

        private object ResolveType(string fullName)
        {
            if (string.IsNullOrEmpty(fullName))
                throw new Exception("Type full name is empty");

            var type = Type.GetType(fullName);
            if (type == null)
                throw new Exception($"Cannot resolve type: {fullName}");

            return _diContainer.Resolve(type);
        }

        private object ResolveTypeWithId(string fullName, string id)
        {
            if (string.IsNullOrEmpty(fullName))
                throw new Exception("Type full name is empty");

            var type = Type.GetType(fullName);
            if (type == null)
                throw new Exception($"Cannot resolve type: {fullName}");

            return _diContainer.ResolveId(type, id);
        }
        
#if UNITY_EDITOR
        private void OnValidate()
        {
            if (viewType != null)
                viewTypeFullName = viewType.GetClass()?.AssemblyQualifiedName;

            if (viewModelType != null)
                viewModelTypeFullName = viewModelType.GetClass()?.AssemblyQualifiedName;
        }
#endif
    }
}