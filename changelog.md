# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [5.1.0](https://github.com/unity-game-framework/ugf-module-assets/releases/tag/5.1.0) - 2023-05-03  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-module-assets/milestone/19?closed=1)  
    

### Added

- Add asset group collection ([#65](https://github.com/unity-game-framework/ugf-module-assets/issues/65))  
    - Update dependencies: `com.ugf.application` to `8.5.0` version.
    - Add `AssetGroupCollectionListAsset` class as implementation of `AssetGroupAsset` class used to store collection of the `AssetGroupAsset` assets.
    - Change `AssetModuleDescription` class to be read-only.

## [5.0.0](https://github.com/unity-game-framework/ugf-module-assets/releases/tag/5.0.0) - 2023-01-04  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-module-assets/milestone/18?closed=1)  
    

### Changed

- Update project ([#62](https://github.com/unity-game-framework/ugf-module-assets/issues/62))  
    - Update dependencies: `com.ugf.application` to `8.4.0` and `com.ugf.editortools` to `2.15.0` version.
    - Update package _Unity_ version to `2022.2`.
    - Add `AssetTrack.GetAsset<T>()` method to get access with casting to specific type.

## [5.0.0-preview.2](https://github.com/unity-game-framework/ugf-module-assets/releases/tag/5.0.0-preview.2) - 2022-10-30  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-module-assets/milestone/17?closed=1)  
    

### Added

- Add asset database loader ([#59](https://github.com/unity-game-framework/ugf-module-assets/issues/59))  
    - Update dependencies: `com.ugf.editortools` to `2.13.0` version.
    - Add `AssetDatabaseAssetLoader` class as loader which can be used as loader from editor _AssetDatabase_ directly. (Editor Only)
    - Change `AssetModuleAssetEditor` class to support asset replacements.

## [5.0.0-preview.1](https://github.com/unity-game-framework/ugf-module-assets/releases/tag/5.0.0-preview.1) - 2022-10-25  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-module-assets/milestone/16?closed=1)  
    

### Fixed

- Fix preload assets list display ([#57](https://github.com/unity-game-framework/ugf-module-assets/issues/57))  
    - Update dependencies: `com.ugf.application` to `8.3.1` and `com.ugf.editortools` to `2.12.0` versions.
    - Fix `AssetModuleAsset` inspector drawing for _Preload_ and _PreloadAsync_ reorderable lists.

## [5.0.0-preview](https://github.com/unity-game-framework/ugf-module-assets/releases/tag/5.0.0-preview) - 2022-07-14  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-module-assets/milestone/15?closed=1)  
    

### Changed

- Change string ids to data ([#55](https://github.com/unity-game-framework/ugf-module-assets/issues/55))  
    - Update dependencies: `com.ugf.application` to `8.3.0` and `com.ugf.editortools` to `2.8.1` versions.
    - Update package _Unity_ version to `2022.1`.
    - Change usage of ids as `GlobalId` structure instead of `string`.

## [4.0.0](https://github.com/unity-game-framework/ugf-module-assets/releases/tag/4.0.0) - 2021-12-23  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-module-assets/milestone/14?closed=1)  
    

### Added

- Add direct asset loader ([#54](https://github.com/unity-game-framework/ugf-module-assets/pull/54))  
    - Update package _Unity_ version to `2021.2`.
    - Update dependencies: `com.ugf.application` to `8.0.0` version.
    - Add `ReferencedAssetLoader` and related classes to support loading of asset by direct asset reference.
    - Add `ReferencedAssetGroupAsset` class to define collection of assets loaded directly by asset reference.
    - Change `ResourcesLoader` class and related classes name to `ResourcesAssetLoader` scheme.
    - Remove deprecated code.

## [4.0.0-preview.4](https://github.com/unity-game-framework/ugf-module-assets/releases/tag/4.0.0-preview.4) - 2021-10-06  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-module-assets/milestone/13?closed=1)  
    

### Added

- Add refresh all groups in project settings ([#52](https://github.com/unity-game-framework/ugf-module-assets/pull/52))  
    - Add `Refresh All` button in project settings to refresh all groups in the project.
    - Deprecate `ResourcesAssetEditorUtility.UpdateAllAssetGroups()` method, use `UpdateAssetGroupAll()` method instead.

## [4.0.0-preview.3](https://github.com/unity-game-framework/ugf-module-assets/releases/tag/4.0.0-preview.3) - 2021-06-11  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-module-assets/milestone/12?closed=1)  
    

### Added

- Add asset info default load and unload parameters ([#50](https://github.com/unity-game-framework/ugf-module-assets/pull/50))  
    - Add `IAssetLoadParameters` and `IAssetUnloadParameters` properties for `IAssetLoader` interface and class implementations.
    - Add `Load`, `LoadAsync`, `Unload` and `UnloadAsync` methods without parameters argument for `IAssetLoader` interface and class implementations which use default load and unload parameters from loader.
    - Add implementation of default load and unload parameters and methods overloads as virtual methods for `AssetLoaderBase` class.
    - Add `TryGetDefaultLoadParametersByAsset`, `GetDefaultLoadParametersByAsset`, `TryGetDefaultUnloadParametersByAsset` and `GetDefaultUnloadParametersByAsset` extension methods for `IAssetModule` interface to get default load and unload parameters of loader by the specified asset id.
    - Add `Load`, `LoadAsync`, `Unload` and `UnloadAsync` extension methods for `IAssetModule` interface without load and unload parameters as argument which use default parameters of loader.
    - Change `AssetModule` to load and unload assets on initialization and uninitialization using default parameters from loaders.
    - Change `Load<T>` and `LoadAsync<T>` extension methods for `IAssetModule` to make load parameters optional, default parameters from loader will be used instead.
    - Change `UnloadForce` and `UnloadForceAsync` extension methods for `IAssetModule` to make unload parameters optional, default parameters from loader will be used instead.

## [4.0.0-preview.2](https://github.com/unity-game-framework/ugf-module-assets/releases/tag/4.0.0-preview.2) - 2021-05-25  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-module-assets/milestone/11?closed=1)  
    

### Changed

- Change project settings root name ([#48](https://github.com/unity-game-framework/ugf-module-assets/pull/48))  
    - Update dependencies: `com.ugf.application` to `8.0.0-preview.7` version.
    - Change project settings root name to `Unity Game Framework`.

## [4.0.0-preview.1](https://github.com/unity-game-framework/ugf-module-assets/releases/tag/4.0.0-preview.1) - 2021-02-09  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-module-assets/milestone/10?closed=1)  
    

### Changed

- Update project registry ([#45](https://github.com/unity-game-framework/ugf-module-assets/pull/45))  
    - Update package publish registry.
    - Update dependencies: `com.ugf.application` to `8.0.0-preview.4` version.
- Update to Unity 2021.1 ([#44](https://github.com/unity-game-framework/ugf-module-assets/pull/44))  
- Remove groups and loaders provider types ([#41](https://github.com/unity-game-framework/ugf-module-assets/pull/41))  
    - Add `ResourcesAssetEditorSettings` settings with `UpdateAllGroupsOnBuild` property to control update of all asset groups before player build.
    - Add `ResourcesAssetEditorUtility` class to work with `ResourcesAssetGroupAsset` assets.
    - Add `IAssetLoadParameters` and `IAssetUnloadParameters` interfaces and implementations to replace solution with structures.
    - Add `AssetModuleExtensions` with `GetLoaderByAsset`, `TryGetLoaderByAsset` methods and etc.
    - Change `IAssetInfo` and implementations to define `LoaderId` property.
    - Change name of `IAssetsModule` and related classes to `IAssetModule`.
    - Change `IAssetModule`, `IAssetLoader` and implementations to work with asset load and unload parameter interfaces.
    - Change `AssetModuleAsset` to work with reworked `AssetGroupAsset` groups.
    - Change `IAssetTracker` to require the same asset to be specified when untrack usage.
    - Change `ResourcesLoader` and `ResourcesAssetGroupAsset` to work with reworked parameters and groups.
    - Change name of abstract `AssetLoaderAssetBase` class to `AssetLoaderAsset`.
    - Remove `IAssetGroupProvider` and implementations, change to store loader id for each asset info.
    - Remove `IAssetGroup` and replated classes, replaced by abstract `AssetGroupAsset` class.
    - Remove `AssetLoadMode` and `AssetUnloadMode` enums and change to track all loaded assets by default.
    - Remove `AssetLoaderProvider` and replaced by generic providers solution.

## [4.0.0-preview](https://github.com/unity-game-framework/ugf-module-assets/releases/tag/4.0.0-preview) - 2021-01-25  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-module-assets/milestone/9?closed=1)  
    

### Changed

- Update providers and application package ([#39](https://github.com/unity-game-framework/ugf-module-assets/pull/39))  
    - Update dependencies: `com.ugf.application` to `8.0.0-preview.3` version.
    - Add `AssetLoaderProvider` and `AssetGroupProvider` providers.
    - Change `IAssetsModule` to work with changed providers.
    - Change `IAssetLoader` methods to take `IContext` as one of arguments.
    - Change `IAssetTracker` and `AssetTracker` to implement `IProvider<string, AssetTrack>` interface.
    - Remove `AssetsProvider` and replaced by other providers.
    - Remove `IAssetProvider` argument from `IAssetLoader` methods.

## [3.1.0](https://github.com/unity-game-framework/ugf-module-assets/releases/tag/3.1.0) - 2021-01-16  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-module-assets/milestone/8?closed=1)  
    

### Changed

- Update application dependency ([#36](https://github.com/unity-game-framework/ugf-module-assets/pull/36))  
    - Update dependencies: `com.ugf.application` to `7.1.0` version.
    - Deprecate `AssetsModuleDescription` constructor with `registerType` argument, use properties initialization instead.

## [3.0.0](https://github.com/unity-game-framework/ugf-module-assets/releases/tag/3.0.0) - 2020-12-05  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-module-assets/milestone/7?closed=1)  
    

### Changed

- Update to support latest application package ([#33](https://github.com/unity-game-framework/ugf-module-assets/pull/33))  
    - Update to use `UGF.Builder` and `UGF.Description` packages from the latest version of `UGF.Application` package.
    - Change dependencies: `com.ugf.application` to `6.0.0` and `com.ugf.logs` to `4.1.0`.
    - Change all assets to use and implement builders features.
    - Change `AssetGroupAsset` to be abstract from assets storage, and no longer require generic argument.
    - Change name of the root of create asset menu, from `UGF` to `Unity Game Framework`.
    - Remove `IAssetGroupAssetEntry` interface and `AssetGroupAssetEntry` abstract class, implementations moved to specific classes.
    - Remove `ResourcesAssetGroupEntry`, implementation moved to `ResourcesAssetGroupAsset` as `Entry` nested structure.

## [2.0.1](https://github.com/unity-game-framework/ugf-module-assets/releases/tag/2.0.1) - 2020-11-07  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-module-assets/milestone/6?closed=1)  
    

### Changed

- Update UGF.Application package ([#29](https://github.com/unity-game-framework/ugf-module-assets/pull/29))  
    - Update `UGF.Application` package to `5.2.0` version.
    - Change `IAssetsModule` to implement `IApplicationModuleDescribed<IAssetsModuleDescription>` interface.
    - Add `AssetsModule` default constructor.

## [2.0.0](https://github.com/unity-game-framework/ugf-module-assets/releases/tag/2.0.0) - 2020-11-07  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-module-assets/milestone/5?closed=1)  
    

### Added

- Add ability to unload asset with track only ([#26](https://github.com/unity-game-framework/ugf-module-assets/pull/26))  
    - Add `AssetUnloadMode` with `TrackOnly` option.
    - Add `AssetUnloadParameters.DefaultTrackOnly` default parameters for unload with track only.
    - Change `AssetUnloadParameters` to use `AssetUnloadMode` instead of `AssetLoadMode`.
    - Change `AssetsModule` to work with `AssetUnloadMode` when unloads asset.
- Add AssetsModule unload unused ([#25](https://github.com/unity-game-framework/ugf-module-assets/pull/25))  
    - Add `UnloadUnused` and `UnloadUnusedAsync` extension methods for `IAssetsModule` to unload tracked assets with zero counter.
- Add generic asset loader ([#22](https://github.com/unity-game-framework/ugf-module-assets/pull/22))  
    - Add `AssetGroupAsset<T>.OnCreateGroup` and `OnPopulateGroup` methods to override `IAssetGroup` creation.
    - Add `AssetLoader<TGroup, TInfo>` loader class with fetching of `IAssetGroup` and `IAssetInfo` by default.
    - Change `ResourcesLoader` to inherit from `AssetLoader<TGroup, TInfo>` class.

### Changed

- Add parameters for asset load and unload handlers ([#21](https://github.com/unity-game-framework/ugf-module-assets/pull/21))  
    - Change `AssetLoadHandler`, `AssetLoadedHandler`, `AssetUnloadHandler` and `AssetUnloadedHandler` signature to have `AssetLoadParameters` and `AssetUnloadParameters` arguments.
    - Change `AssetsModule` to pass `AssetLoadParameters` and `AssetUnloadParameters` for all events.

## [1.0.0](https://github.com/unity-game-framework/ugf-module-assets/releases/tag/1.0.0) - 2020-10-28  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-module-assets/milestone/4?closed=1)  
    

### Changed

- Rework module with updated UGF.Application ([#12](https://github.com/unity-game-framework/ugf-module-assets/pull/12))  
    - Add `AssetGroup` and `AssetGroupAsset` to define group of assets which use specific loader by id.
    - Add `IAssetLoader` and related classes to implement specific loader of assets.
    - Add `IAssetProvider` and default implementation to manage loaders and asset groups.
    - Add `IAssetTracker` and default implementation to manager and track assets usage.
    - Add `ResourcesLoader` and related classes as assets loading implementation from `Resources`.
    - Rework `AssetsModule` to use new assets loaders, tracker and provider with additional parameters.
    - Rework `AssetsModule` and related classes to support updated `UGF.Application` package.
    - Remove `AssetsResourcesModule` class.
- Update to Unity 2020.2 ([#10](https://github.com/unity-game-framework/ugf-module-assets/pull/10))

## [0.3.0-preview](https://github.com/unity-game-framework/ugf-module-assets/releases/tag/0.3.0-preview) - 2019-12-09  

- [Commits](https://github.com/unity-game-framework/ugf-module-assets/compare/0.2.0-preview...0.3.0-preview)
- [Milestone](https://github.com/unity-game-framework/ugf-module-assets/milestone/3?closed=1)

### Added
- Package dependencies:
    - `com.ugf.application`: `3.0.0-preview`.

### Changed
- Update `UGF.Application` package.

### Removed
- Package dependencies:
    - `com.ugf.module`: `0.2.0-preview`.

## [0.2.0-preview](https://github.com/unity-game-framework/ugf-module-assets/releases/tag/0.2.0-preview) - 2019-11-19  

- [Commits](https://github.com/unity-game-framework/ugf-module-assets/compare/0.1.0-preview...0.2.0-preview)
- [Milestone](https://github.com/unity-game-framework/ugf-module-assets/milestone/2?closed=1)

### Added
- Package dependencies:
    - `com.ugf.module`: from `0.2.0-preview`.

### Changed
- Rework module to use async / await.

### Removed
- Package dependencies:
    - `com.ugf.module.unity`: from `0.1.0-preview`.

## [0.1.0-preview](https://github.com/unity-game-framework/ugf-module-assets/releases/tag/0.1.0-preview) - 2019-10-08  

- [Commits](https://github.com/unity-game-framework/ugf-module-assets/compare/d0480fe...0.1.0-preview)
- [Milestone](https://github.com/unity-game-framework/ugf-module-assets/milestone/1?closed=1)

### Added
- This is a initial release.


