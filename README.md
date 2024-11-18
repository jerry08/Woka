<h1 align="center">
    Woka
</h1>

<p align="center">
   <a href="https://discord.gg/U7XweVubJN"><img src="https://img.shields.io/badge/Discord-7289DA?style=for-the-badge&logo=discord&logoColor=white"></a>
   <a href="https://nuget.org/packages/Woka"><img src="https://img.shields.io/nuget/dt/Woka.svg?label=Downloads&color=%233DDC84&logo=nuget&logoColor=%23fff&style=for-the-badge"></a>
</p>

**Woka** is a library that provides workarounds for issues/bugs related to [.NET MAUI](https://github.com/dotnet/maui).
This project uses mostly handlers to provide workarounds. As I experience more bugs,
I will add more effective and efficient workarounds.

### 🌟STAR THIS REPOSITORY TO SUPPORT THE DEVELOPER AND ENCOURAGE THE DEVELOPMENT OF THE PROJECT!


## Install

- 📦 [NuGet](https://nuget.org/packages/Woka): `dotnet add package Woka` (**main package**)

## Usage

Add the `.ConfigureWorkarounds()` in your `MauiProgram.cs` as shown below:

```C#
using Woka;
```

```C#
builder
    .UseMauiApp<App>()
    .UseMauiCommunityToolkit()
    .ConfigureWorkarounds();
```

### Workaround coverage:
Fixes known issues/bugs:
 - [#5983](https://github.com/dotnet/maui/issues/5983) - (Removed) ✔️ Keyboard does not Pop Up when Entry View's Focus is set to True Programmatically
 - [#8787](https://github.com/dotnet/maui/issues/8787) (Removed) ✔️ - Image Source to null doesn't work if the Source had a different value before
 - [#8926](https://github.com/dotnet/maui/issues/8926) - Pull to refresh on RefreshView does not hide the refresh indicator
 - [#6092](https://github.com/dotnet/maui/issues/6092) - The prompts displayed by DisplayAlert and other controls do not follow the theme set
 - [#6030](https://github.com/dotnet/maui/issues/6030) - (Removed) ✔️ Label MaxLines doesn't work
 - [#12219](https://github.com/dotnet/maui/issues/12219) (Removed) ✔️ - [Android] CollectionView: VirtualView cannot be null here, when clearing and adding items on second navigation
 - [#4116](https://github.com/dotnet/maui/issues/4116) - [Windows] CollectionView ItemsUpdatingScrollMode property not working
 - [#8387](https://github.com/dotnet/maui/issues/8387) - [Windows] Notify changes in CollectionView Layouts
 - [#15143](https://github.com/dotnet/maui/issues/15143) - Account for padding when expanding * rows/columns to new sizes
 - [#15018](https://github.com/dotnet/maui/issues/15018) - [iOS/Catalyst] Grid issue with calculating * values on iOS/MacCatalyst
 - [#14557](https://github.com/dotnet/maui/issues/14557) - CollectionView Header & Footer not showing
 - [#6404](https://github.com/dotnet/maui/issues/6404) - (Windows) RC1 - RefreshView does not appear when swiping down with mouse
 - [#7315](https://github.com/dotnet/maui/issues/7315) - iOS - CollectionView inside of RefreshView does not size correctly
