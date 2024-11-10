using Framework.Pooling;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Framework.Editors
{

    [InitializeOnLoad]
    public static class PoolingInspectorGUI
    {

        [Serializable]
        private class PoolingJsonData
        {
            public bool toggle;
            public string key;
            public int poolCount;
        }

        private static AssetImporter _currentImporter;
        private static PoolingJsonData _currentData;

        static PoolingInspectorGUI()
        {
            Editor.finishedDefaultHeaderGUI += OnGUI;
            EditorApplication.projectChanged += HandleChanged;
        }

        private static void HandleChanged()
        {
            var data = Resources.Load<PoolingData>("PoolingData");
            RemoveDestory(data);
            EditorUtility.SetDirty(data);
        }

        private static void OnGUI(Editor editor)
        {

            if (editor.targets.Length <= 0 || editor.targets[0].GetType().Name != "PrefabImporter")
            {

                return;
            }

            var path = AssetDatabase.GetAssetPath(editor.targets[0]);
            var importer = AssetImporter.GetAtPath(path);

            if(_currentImporter != importer)
            {
                _currentImporter = importer;
                _currentData
                    = JsonUtility.FromJson<PoolingJsonData>(importer.userData) ?? new PoolingJsonData();
            }

            DrawGUI();

        }

        private static void DrawGUI()
        {

            EditorGUILayout.BeginHorizontal();
            var rect = EditorGUILayout.GetControlRect(false);
            _currentData.toggle = GUI.Toggle(rect, _currentData.toggle, GUIContent.none);
            rect.position += new Vector2(14, 0);
            GUI.Label(rect, "Pooling");
            EditorGUILayout.EndHorizontal();


            if (_currentData.toggle)
            {

                var oldK = _currentData.key;
                var oldCnt = _currentData.poolCount;

                _currentData.key = EditorGUILayout.TextField("PoolingKey", _currentData.key);
                _currentData.poolCount = EditorGUILayout.IntField("PoolingCount", _currentData.poolCount);

                if(_currentData.key != oldK || _currentData.poolCount != oldCnt)
                    SaveData();

            }


        }

        private static void SaveData()
        {

            if (_currentImporter == null)
                return;

            _currentImporter.userData = JsonUtility.ToJson(_currentData);

            var data = Resources.Load<PoolingData>("PoolingData");

            if (data == null)
                return;

            var asset = AssetDatabase.LoadAssetAtPath<GameObject>(_currentImporter.assetPath);
            var obj = data.poolDatas.Find(x => x.originObject == asset);

            if (_currentData.toggle)
            {
                if (obj == null && data.poolDatas.Find(x => x.key == _currentData.key) == null)
                {
                    data.poolDatas.Add(new PoolingData.PoolData
                    {
                        key = _currentData.key,
                        originObject = asset,
                        poolCount = _currentData.poolCount
                    });
                }
                else if (obj != null && (obj.key != _currentData.key || obj.poolCount != _currentData.poolCount))
                {
                    if (data.poolDatas.Find(x => x.key == _currentData.key) == null)
                    {
                        obj.key = _currentData.key;
                    }

                    obj.poolCount = _currentData.poolCount;
                }
            }
            else
            {
                if(obj != null)
                {
                    data.poolDatas.Remove(obj);
                }
            }

            EditorUtility.SetDirty(data);

            _currentImporter = null;
            _currentData = null;

        }

        private static void RemoveDestory(PoolingData data)
        {
            var removes = data.poolDatas.FindAll(x => x.originObject == null);

            foreach (var remove in removes)
            {
                data.poolDatas.Remove(remove);
            }

        }
    }

}
