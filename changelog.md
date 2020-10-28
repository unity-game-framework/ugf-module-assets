# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

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


