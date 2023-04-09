﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PerlinNoise))]
public class PerlinNoiseEditor : Editor {
    private PerlinNoise instance;

    private void OnEnable() {
        instance = target as PerlinNoise;
    }

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        GUILayout.Space(30);
        if (GUILayout.Button("Generate", GUILayout.Height(30))) {
            instance.Generate();
        }

        GUILayout.Space(30);
        if (GUILayout.Button("SaveToDisk", GUILayout.Height(30))) {
            instance.SaveToDisk();
        }
    }
}