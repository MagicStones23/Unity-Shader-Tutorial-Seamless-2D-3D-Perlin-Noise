using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinNoise_Test_Evolution : MonoBehaviour {
    public float evolutionSpeed;
    public PerlinNoise perlinNoise;

    private void FixedUpdate() {
        float time = Time.realtimeSinceStartup;
        if (time % 3 < 1) {
            perlinNoise.evolution.x += Time.fixedDeltaTime * evolutionSpeed * 1.0f;
            perlinNoise.evolution.y += Time.fixedDeltaTime * evolutionSpeed * 0.75f;
            perlinNoise.evolution.z += Time.fixedDeltaTime * evolutionSpeed * 0.5f;
        }
        else if (time % 3 < 2) {
            perlinNoise.evolution.x += Time.fixedDeltaTime * evolutionSpeed * 0.75f;
            perlinNoise.evolution.y += Time.fixedDeltaTime * evolutionSpeed * 1.0f;
            perlinNoise.evolution.z += Time.fixedDeltaTime * evolutionSpeed * 0.5f;
        }
        else {
            perlinNoise.evolution.x += Time.fixedDeltaTime * evolutionSpeed * 1.0f;
            perlinNoise.evolution.y += Time.fixedDeltaTime * evolutionSpeed * 0.5f;
            perlinNoise.evolution.z += Time.fixedDeltaTime * evolutionSpeed * 0.75f;
        }

        perlinNoise.Generate();
    }
}