# BundleDebugger

Asp.net Web Application (.Net Framework) helps you find repetitive scripts in bundles that you use in your projects

## Installation

Add to web.config

```bash
<add key="BundleDebugger:Enabled" value="true" />
```

## Usage

```bash
Script Render
@BundleDebug.RenderScript("~/bundles/jquery")
Style Render
@BundleDebug.RenderCss("~/Content/css")

Repeated scripts will appear at the bottom of the page with a red mark.
```


## License
[MIT](https://choosealicense.com/licenses/mit/)
