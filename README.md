# ArKitKat 

[![bad joke](https://img.shields.io/badge/why%20everybody%20likes%20badge-because%20it's%20cool-brightgreen.svg)](http://borgmon.me)

An Unity Library for interacting UnityARKitPlugin.

## Usage
1. First thing first. Drag ArKitKat folder into your assets folder.
2. ArKitKat is using UnityARKitPlugin, so make sure you have it imported.

```C#
using UnityEngine.XR.iOS;
using ArKitKat;
```

## Wiki

### ArPlaneTools
> Get ArPlaneAnchor info easier

|return|method|parameters|explain
|---|---|---|---|
|Vector3|GetRealPosition|ARPlaneAnchor|Return a globle position of the plane where it really is|
|Vector3|GetPosition|ARPlaneAnchor|Return a globle position of the plane where it spawned|
|Quaternion|GetRotation|ARPlaneAnchor|Return rotation of the plane

### ArDebug
> Build-in debug tool which shows you ArPlaneAnchor in scenes

|return|method|parameters|explain
|---|---|---|---|
|void|VirtualAnchor|string color,ARPlaneAnchor|spawn a point in scene |
|void|VirtualAnchor|string color,Vector3|spawn a point in scene |
|void|VirtualAnchor|string color,Transform|spawn a point in scene |
|void|VirtualAnchor|string color,Matrix4x4|spawn a point in scene |

### VerticalPlaneGenetation
> An implement of ArKitKat. It generates vertical plane based on horizontal plane.
> (Since Arkit not support vertical plane detection yet)

please add UnityARUtility.cs `mf.gameObject.tag = "HoriPlane";` to UpdatePlaneWithAnchorTransform method.

### FastDetection
> This script can detect a plane faster than ever! It uses raw Arkit point to generate plane.

PRO:
- Detect a plane in a sec!
- Using Calculus III!

CON:
- Only detect one plane
- If more than one plane exist, it will be broken

If you are looking for something that give you a plane before the stable plane spawn, this is the one. 

### BestFitPlaneDouble/Float
> Dependency for FastDetection. Found the rust version at [this blog](http://www.ilikebigbits.com/blog/2017/9/24/fitting-a-plane-to-noisy-points-in-3d). I ported to c# version for this project.

You have 2 versions to choice. Double(64)precision and float(32)precision.
Unity does not provide Vector3 x double operation, so if you are using double version,
**Please download [this lib](https://github.com/sldsmkd/vector3d) and copy to your asset**

## License

The MIT License(MIT)
Copyright(c) 2017 Borgmon.me
