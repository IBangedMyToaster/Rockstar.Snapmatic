<p align="center">
 <img alt="GitHub " src="https://img.shields.io/github/last-commit/IBangedMyToaster/Rockstar.Snapmatic">
 <img alt="GitHub " src="https://img.shields.io/github/repo-size/IBangedMyToaster/Rockstar.Snapmatic"> 
 <img alt="GitHub " src="https://img.shields.io/github/license/IBangedMyToaster/Rockstar.Snapmatic">
 <img alt="GitHub " src="https://img.shields.io/github/issues-raw/IBangedMyToaster/Rockstar.Snapmatic">
 <img alt="GitHub " src="https://img.shields.io/github/issues-closed-raw/IBangedMyToaster/Rockstar.Snapmatic">
 <img alt="GitHub" src="https://img.shields.io/nuget/v/Rockstar.Snapmatic.svg">
</p>

---

A .NET library for reading, decoding, and extracting metadata from GTA V Snapmatic files, including image data and in-game location information.

Using this library in your project is simple:

1. Install the [NuGet package](https://www.nuget.org/packages/Rockstar.Snapmatic).
    
2. Add the namespace:
```
    using Rockstar.Snapmatic;
```
       
3. Load a Snapmatic file with:
```
    Snap snap = Snapmatic.Load(filePath);
```

This returns a `Snap` object containing the image data (as a byte array) along with metadata such as the in-game location.

> ⚠️ **Disclaimer**: This project is **not affiliated with or endorsed by Rockstar Games.**