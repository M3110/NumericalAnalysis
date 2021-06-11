# IC Soft Tissue App
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/en/1.0.0/)
and this project adheres to [Semantic Versioning](http://semver.org/spec/v2.0.0.html).

## UNRELEASED
### Added
 - Folders Static and Dynamic in RunAnalysis.
 - Controller for DynamicAnalysis operations.
 - Operation RunHalfCarDynamicAnalysis.
 - Numerical methods Newmark and Newmark-Beta.
 - Method Sum and Subtract in ArrayExtension.
 - Property GravityAcceleration in Constants.
 - Class BasePaths that contains the application base paths used in operations.
### Changed
 - Renamed operation and contracts from RunAnalysis to RunStaticAnalysis .
 - Renamed AnalysisController to StaticAnalysisController.
 - Moved operation RunStaticAnalysis to folder 'RunAnalysis/Static'.
 - OperationBase to use invariant culture.

## [1.0.0] - 2021-04-20
### Added
 - First version of the program.