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

### Enable RemapTo01, FbmIteration = 8

![使用说明-2D FBM](https://user-images.githubusercontent.com/129722386/231012581-ec6b5a2a-3f79-49e2-8efb-fe8cd3b50b90.png)

### Evolution, need assistance of C# script

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

### Default 3D

![使用说明-默认3D](https://user-images.githubusercontent.com/129722386/231012847-e7e0a222-6134-45ef-9661-350bf3f6696e.png)

### Disable/Enable Tilable

![使用说明-3D 不连续](https://user-images.githubusercontent.com/129722386/231012917-c96f0809-ff07-490f-97c2-59d8bede43bb.png)

![使用说明-3D 六方连续](https://user-images.githubusercontent.com/129722386/231012930-2c62d90c-5636-4ca5-8427-e7270c259d19.png)

### Enable RemapTo01, FbmIteration = 8

![使用说明-3D FBM](https://user-images.githubusercontent.com/129722386/231013009-8b54b675-6efc-40f4-89f8-dd5373f28c9c.png)



<br/><br/>

# Unity-Shader-Tutorial-Seamless-2D-3D-Perlin-Noise

# Algorithm

### Let's take a look at a 256x256 perlin noise

![教程0](https://user-images.githubusercontent.com/129722386/231013417-e5d6fb01-1418-44cc-9aee-6b1f274006e4.png)

### split it into 16 blocks and 25 vertices

![教程1](https://user-images.githubusercontent.com/129722386/231013471-b6552a4a-7ac8-411d-a05b-e995c795ea1e.png)

### assign a random vector to each vertex

![教程2](https://user-images.githubusercontent.com/129722386/231013518-ff62fba1-eb66-4193-9391-5f5f6bb04c91.png)

### All pixels are calculated in the same way, so let's take pixel P for example

### Find out P's block

![教程3](https://user-images.githubusercontent.com/129722386/231013684-1e1ed922-f4f9-4467-98f8-7460e06c582f.png)

### Texture is 256x256, 16 blocks in total, 64x64 for each block

### Find out UV coordinates of P

```csharp
float2 uv;
uv.x = (P.x - A.x) / (64 - 1);
uv.y = (P.y - A.y) / (64 - 1);
```
