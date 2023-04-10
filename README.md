# How to use

This project was created in Unity2022.2 

Click Generate to create noise texture

Click SaveToDisk to save noise texture to disk

![使用说明-主界面](https://user-images.githubusercontent.com/129722386/231011626-e3d9f84c-a45e-4c9c-abc5-406d04c7875d.png)

# Main parameter

  SaveToDiskPath

  Resolution

  Frequency

  Is3D

  IsTilable: Should noise texture be seamless，make sure Resolution / Frequency is integer, for instance 256 / 4，256 / 8

  FbmIteration

  RemapTo01: Remap noise value to [0,1]

  Invert: Invert noise value

  Evolution

Tip: 3D noise texture takes up a lot of memory and GPU resources, so it's best to create 3D noise at a resolution of no more than 256 (unless your graphic card is really good)

# Template

### Default 2D

![使用说明-默认2D](https://user-images.githubusercontent.com/129722386/231012371-306ddf43-ccf8-4714-82bd-03cc22618a9f.png)

### Disable/Enable Tilable

![使用说明-2D 不连续](https://user-images.githubusercontent.com/129722386/231012430-f7c1b2cc-1f43-4e7c-a939-19ca2fac9f1b.png)
 
![使用说明-2D 四方连续](https://user-images.githubusercontent.com/129722386/231012485-7e3c7d0b-eb2f-4877-af10-c4773fc14e49.png)

Enable RemapTo01, FbmIteration = 8

![使用说明-2D FBM](https://user-images.githubusercontent.com/129722386/231012581-ec6b5a2a-3f79-49e2-8efb-fe8cd3b50b90.png)

Evolution, need assistance of C# script

```csharp
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
```

https://user-images.githubusercontent.com/129722386/231012716-a0de0721-5db4-4993-88a4-916499f48082.mp4

# Unity-Shader-Tutorial-Seamless-2D-3D-Perlin-Noise
