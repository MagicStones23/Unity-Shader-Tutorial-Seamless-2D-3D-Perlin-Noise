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



# Unity-Shader-Tutorial-Seamless-2D-3D-Perlin-Noise
