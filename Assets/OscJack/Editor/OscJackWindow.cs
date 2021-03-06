//
// OSC Jack - OSC Input Plugin for Unity
//
// Copyright (C) 2015, 2016 Keijiro Takahashi
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//
using UnityEngine;
using UnityEditor;
using System.Runtime.InteropServices;

namespace OscJack
{
    // OSC message monitor window
    class OscJackWindow : EditorWindow
    {
        #region Custom Editor Window Code

        [MenuItem("Window/OSC Jack")]
        public static void ShowWindow()
        {
            EditorWindow.GetWindow<OscJackWindow>("OSC Jack");
        }

        void OnGUI()
        {
            EditorGUILayout.BeginVertical();

            foreach (var item in OscMaster.MasterDirectory)
            {
                var data = item.Value;
                var text = "";

                for (var i = 0; i < data.Length - 1; i++)
                    text += data[i] + ", ";

                if (data.Length > 0)
                    text += data[data.Length - 1];

                EditorGUILayout.LabelField(item.Key, text);
            }

            EditorGUILayout.EndVertical();
        }

        #endregion

        #region Update And Repaint

        const int _updateInterval = 20;
        int _countToUpdate;
        int _lastMessageCount;

        void Update()
        {
            if (--_countToUpdate > 0) return;
            _countToUpdate = _updateInterval;

            var mcount = OscMaster.MasterDirectory.TotalMessageCount;
            if (mcount != _lastMessageCount) {
                Repaint();
                _lastMessageCount = mcount;
            }
        }

        #endregion
    }
}
