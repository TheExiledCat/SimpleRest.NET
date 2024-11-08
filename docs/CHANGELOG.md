# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/en/1.0.0/)
and this project adheres to [Semantic Versioning](http://semver.org/spec/v2.0.0.html).

<!-- insertion marker -->
## Unreleased

<small>[Compare with latest](https://github.com/TheExiledCat/SimpleRest.NET/compare/640ec0b3c63533222037f0fe5aa386f2ff0a0278...HEAD)</small>

### Added

- added templating structure ([f15fd19](https://github.com/TheExiledCat/SimpleRest.NET/commit/f15fd19cdf0c9e1f1d9eb59667672964a7c6a7d7) by TheExiledCat).
- added changelog ([448db93](https://github.com/TheExiledCat/SimpleRest.NET/commit/448db93709c1cc6dd371103d73358e0a50d9fca0) by TheExiledCat).
- added editorconfigs, updated readme, changed project from .net8 to .net >=6.0 and updated collection initializers to match ([0db8b98](https://github.com/TheExiledCat/SimpleRest.NET/commit/0db8b98c6e35b63d5eea7ab36056338e3ae43a8c) by TheExiledCat).
- added editorconfig rules ([4d7cfb1](https://github.com/TheExiledCat/SimpleRest.NET/commit/4d7cfb1c99dddb87cd9d38188401e28f4c2f28eb) by TheExiledCat).
- added basic 404 handler and fixed readme ([6d8fbe7](https://github.com/TheExiledCat/SimpleRest.NET/commit/6d8fbe7c983df085a989f09d3c1a8c39b087cb18) by armando-codecafe).
- Added source generator foc generic param getting, fixed type conversion for string types (added quotations to non convertable values by default), ran source generator for generic param getting, updated readme ([351a9dc](https://github.com/TheExiledCat/SimpleRest.NET/commit/351a9dca71b6e4f55c0ccc8e116528246e8b0749) by TheExiledCat).
- added xml docs to classes, new docs, and examples ([f5312e2](https://github.com/TheExiledCat/SimpleRest.NET/commit/f5312e2ff8e41ed18bec577d517a8ed9dcc91b23) by TheExiledCat).
- added default numeric parsing type, fixed middlewares being overwritten on every middleware stack check ([634671b](https://github.com/TheExiledCat/SimpleRest.NET/commit/634671bd260cc6a52b96a8b8371b5e758f69cfe6) by TheExiledCat).
- added blank target to badge ([afc1d9c](https://github.com/TheExiledCat/SimpleRest.NET/commit/afc1d9cb224541853ca5571f1825d6979cb9c05c) by armando-codecafe).
- added nuget badge ([9195b6a](https://github.com/TheExiledCat/SimpleRest.NET/commit/9195b6a16232315cae8bca8c4531112af79dd786) by armando-codecafe).
- Added UriTemplate parsing according to rfc6570 standard, DI for formatters, placed all classes in their correct interface with internal access ([a374ed1](https://github.com/TheExiledCat/SimpleRest.NET/commit/a374ed1fd758464c0d453bc2b2ea6b404df20939) by TheExiledCat).
- added new scripts to workflow ([191aae5](https://github.com/TheExiledCat/SimpleRest.NET/commit/191aae51f3d0497d5fdd1cf1f9e938fff6456946) by TheExiledCat).
- added new workflow script ([fa368cc](https://github.com/TheExiledCat/SimpleRest.NET/commit/fa368ccd80566c36b92a1a1ff56442c43c23a22b) by TheExiledCat).
- added jekyll ignore ([11e2f84](https://github.com/TheExiledCat/SimpleRest.NET/commit/11e2f845bbe8d9d825ab7cbd61d1a5c6768f924f) by TheExiledCat).
- added index.html forwarder to root of docs ([87b21ac](https://github.com/TheExiledCat/SimpleRest.NET/commit/87b21acb5a6cfe133d9cf2d842f93c7a9a4d8e80) by TheExiledCat).
- added new workflow ([513a36a](https://github.com/TheExiledCat/SimpleRest.NET/commit/513a36a9eedea26a6113d7035563dca5c6b31a34) by TheExiledCat).

### Fixed

- fixed rfc link ([78e1027](https://github.com/TheExiledCat/SimpleRest.NET/commit/78e10278df8ac334fe7afcbf55f63c3784212e1e) by TheExiledCat).
- fixed documentation link in footer ([8d98c04](https://github.com/TheExiledCat/SimpleRest.NET/commit/8d98c04ebb675d2f5ef8763575c6c73d8242c352) by armando-codecafe).
- fixed docs ([79945ed](https://github.com/TheExiledCat/SimpleRest.NET/commit/79945ed41bef7126b453e67dac3be93c0268c108) by armando-codecafe).
- fixed typos in readmes ([69288ee](https://github.com/TheExiledCat/SimpleRest.NET/commit/69288ee09518d07f05bd056fd179026b29302f99) by armando-codecafe).
- Fixed response being outside of namespace ([f06fe0e](https://github.com/TheExiledCat/SimpleRest.NET/commit/f06fe0e49aa90f5825abc7940783399bd5633f7f) by armando-codecafe).
- fixed installation name in readme ([74b9d92](https://github.com/TheExiledCat/SimpleRest.NET/commit/74b9d927fa21a0801d716bac9ba09d395d8fbfc5) by TheExiledCat).
- fixed code hightlighting in readme ([65a7624](https://github.com/TheExiledCat/SimpleRest.NET/commit/65a76249bd8efc3c20561ab97c06af7795cbfc7e) by TheExiledCat).
- fixed jekyll ([3fa8428](https://github.com/TheExiledCat/SimpleRest.NET/commit/3fa842857b400149cd49eb6990ee87a3386ee78a) by TheExiledCat).
- fixed empty routes ([160fd5a](https://github.com/TheExiledCat/SimpleRest.NET/commit/160fd5aea266f7838f31a2e200f6716522bac86a) by TheExiledCat).
- fixed instalatiion readme syntax error ([a030024](https://github.com/TheExiledCat/SimpleRest.NET/commit/a030024b9cd252435eca4712508d198f29023073) by TheExiledCat).

### Removed

- removed Program from namespace api ([2b3a39a](https://github.com/TheExiledCat/SimpleRest.NET/commit/2b3a39ad0e6215a1b957cb4e53fdc8539ee645d1) by TheExiledCat).

<!-- insertion marker -->
