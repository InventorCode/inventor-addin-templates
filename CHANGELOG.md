# InventorCode.AddinTemplates Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [0.4.0](https://www.nuget.org/packages/InventorCode.AddinTemplates/)

### Added

- `inv-pluginhost` template
- `inv-plugin` template

## [0.3.0](https://www.nuget.org/packages/InventorCode.AddinTemplates/)

### Added

- CHANGELOG.md
- WPF window addin template `inv-wpf`
- Inventor (2021) set as Debug executable for all templates.

### Fixed

- nifty-addin-sdk build event copies the Resources folder into the user's Autodesk Addins directory during Debug builds.
- nifty-addin-sdk was added to the inventor-addin-templates VS solution.

### Removed

## [0.2.0](https://www.nuget.org/packages/InventorCode.AddinTemplates/)

### Added

- Initial Release
- Added templates:
  - `inv-empty`
  - `inv-nifty`
- Created nuget package [InventorCode.AddinTemplates](https://www.nuget.org/packages/InventorCode.AddinTemplates/)