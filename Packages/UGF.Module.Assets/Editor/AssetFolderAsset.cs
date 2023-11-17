using System;
using UGF.EditorTools.Runtime.IMGUI.Attributes;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UGF.Module.Assets.Editor
{
    public abstract class AssetFolderAsset : ScriptableObject
    {
        [ObjectPicker("t:folder")]
        [SerializeField] private Object m_folder;

        public Object Folder { get { return m_folder; } set { m_folder = value; } }

        public bool IsValid()
        {
            if (m_folder != null)
            {
                string path = AssetDatabase.GetAssetPath(m_folder);

                return AssetDatabase.IsValidFolder(path) && OnIsValid();
            }

            return false;
        }

        public Type GetAssetType()
        {
            return OnGetAssetType();
        }

        public void Update()
        {
            if (!IsValid()) throw new InvalidOperationException("Asset folder is invalid.");

            OnUpdate();
        }

        public string GetFolderPath()
        {
            if (!IsValid()) throw new InvalidOperationException("Asset folder is invalid.");

            return AssetDatabase.GetAssetPath(m_folder);
        }

        public string[] FindAssetsAsGuids()
        {
            string path = GetFolderPath();

            return AssetFolderEditorUtility.FindAssetsAsGuids(path, GetAssetType());
        }

        protected virtual bool OnIsValid()
        {
            return true;
        }

        protected virtual Type OnGetAssetType()
        {
            return typeof(Object);
        }

        protected abstract void OnUpdate();
    }
}
