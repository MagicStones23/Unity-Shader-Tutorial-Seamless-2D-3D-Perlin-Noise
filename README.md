![文章封面](https://user-images.githubusercontent.com/129722386/231019769-f78e1b2f-a8c4-491b-8e8a-586ff3e819e5.png)

* [How to use](#how-to-use)
* [Main parameter](#main-parameter)
* [Template](#template)
* [Algorithm](#algorithm)
* [Continuity](#continuity)
* [FBM](#fbm)
* [My Social Media](#my-social-media)

# How to use

This project was created in Unity2021.3 

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

Tip: 3D noise texture takes up a lot of memory and GPU resources, resolution should no more than 256 (unless your graphic card is really good)

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

### Split it into 16 blocks and 25 vertices

![教程1](https://user-images.githubusercontent.com/129722386/231013471-b6552a4a-7ac8-411d-a05b-e995c795ea1e.png)

### Assign a random vector to each vertex

![教程2](https://user-images.githubusercontent.com/129722386/231013518-ff62fba1-eb66-4193-9391-5f5f6bb04c91.png)

### All pixels are calculated in the same way, let's take pixel P for example

### Find out P's block

![教程3](https://user-images.githubusercontent.com/129722386/231013684-1e1ed922-f4f9-4467-98f8-7460e06c582f.png)

### Texture is 256x256, 16 blocks in total, 64x64 for each block

### Find out uv coordinates of P

```csharp
float2 uv;
uv.x = (P.x - A.x) / (64 - 1);
uv.y = (P.y - A.y) / (64 - 1);
```
### a, b, c, d is random vector of Vertex A, B, C, D(green vectors in picture above)

### Calculate 4 dot products

```csharp
float2 AP = P - A;
float2 BP = P - B;
float2 CP = P - C;
float2 DP = P - D;

AP /= 64;
BP /= 64;
CP /= 64;
DP /= 64;

float dotA = dot(AP , a);
float dotB = dot(BP , b);
float dotC = dot(CP , c);
float dotD = dot(DP , d);
```

### Use uv coordinates to interpolate them

```csharp
float PerlinNoiseLerp(float l, float r, float t) {
    t = ((6 * t - 15) * t + 10) * t * t * t;
    return lerp(l, r, t);
}

float temp0 = PerlinNoiseLerp(dotA, dotD, uv.x);
float temp1 = PerlinNoiseLerp(dotB, dotC, uv.x);
float noiseValue = PerlinNoiseLerp(temp0, temp1, uv.y);
noiseValue = (noiseValue + 1.0) / 2.0;
```
### It's all done, we have perlin noise value for P!

# Continuity

### Take P1 and P2 for example

![教程4](https://user-images.githubusercontent.com/129722386/231015654-e59d2483-a286-4153-996f-4e5a5efddc65.png)

### UV of P1 is (0, 0.5), uv of P2 is (1, 0.5), noise value of P1 is determined by random vectors of A and B, and noise value of P2 is determined by random vectors of H and G, so as long as thier random vectors are equal, it can connect seamlessly

![教程5](https://user-images.githubusercontent.com/129722386/231016877-24e25268-9247-48b4-be30-93421be9af3a.png)

# FBM

### PerlinNoiseA, frequency = 4

![教程6](https://user-images.githubusercontent.com/129722386/231017200-5ddb29b8-c766-4bd9-846e-1e10013c2f14.png)

### PerlinNoiseB, frequency = 8

![教程7](https://user-images.githubusercontent.com/129722386/231017208-b02ef518-d006-4d3b-8ce1-72b2ccc10ac5.png)

### PerlinNoiseC, frequency = 16

![教程8](https://user-images.githubusercontent.com/129722386/231017213-60d911e1-64e9-4719-9a00-bbaecdba1612.png)

### FBM = PerlinNoiseA + PerlinNoiseB * 0.5 + PerlinNoiseC * 0.25 ans so on, endup something like this

![教程9](https://user-images.githubusercontent.com/129722386/231017530-f96a6d95-d23a-41ed-a08f-0e0da4675298.png)

### Congratulations, you've learned basic perlin noise!

# My Social Media

### Twitter : https://twitter.com/MagicStone23

### YouTube : https://www.youtube.com/channel/UCBUXiYqkFy0g6V0mVH1kESw

### zhihu : https://www.zhihu.com/people/shui-guai-76-84

### Bilibili : https://space.bilibili.com/423191063?spm_id_from=333.1007.0.0
