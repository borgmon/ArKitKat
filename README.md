# ArKitKat [bad joke](https://img.shields.io/badge/why%20everybody%20likes%20badge-because%20it's%20cool-brightgreen.svg)

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

### VerticalPlaneDetection
> A implement of ArKitKat. Generate vertical plane based on horizontal plane.
> (Since Arkit not support vertical plane detection yet)

please add UnityARUtility.cs `mf.gameObject.tag = "HoriPlane";` to UpdatePlaneWithAnchorTransform method.


## License

The MIT License(MIT)
Copyright(c) 2017 Borgmon.me
