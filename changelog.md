# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [2.0.0] - 2023.12.03

### Added
- Generic versions of `Generate()` and `GenerateStream()` are now available if you're targeting .NET 7 (issue [#5](https://github.com/JasonBock/Collatz/issues/5))
- Added an icon for the NuGet package (issue [#4](https://github.com/JasonBock/Collatz/issues/4))

### Changed
- Non-generic versions of `Generate()` and `GenerateStream()` are now marked as `[Obsolete]` without error, and will eventually be removed once .NET 6 is out of support.