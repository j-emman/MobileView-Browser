
# MobileView-Browser

A lightweight Windows Forms application designed to provide a mobile browsing experience by default, utilizing the WebView2 control.

---

## Features
- **Mobile-first Browsing**: Defaults all websites to mobile view.
- **Lightweight**: Minimalistic design for fast and efficient browsing.
- **Customizable**: Extendable for additional features such as incognito mode, [extension support](##extension-support), tab management, and more.

---

## Platform Support

Currently, this application supports **Windows** only.

| Platform | Technologies          |
|----------|-----------------------|
| Windows  | .NET Framework/.NET Core, WebView2 |

---

## Library Dependencies

### Windows
- **Microsoft.Web.WebView2**
  - Used for embedding the Edge WebView2 control into the application.
  - Installable via [NuGet](https://www.nuget.org/packages/Microsoft.Web.WebView2).

---

## Build Requirements

### Windows
- **Microsoft Visual Studio Community 2022**
  - A free, powerful IDE for building .NET applications.
  - [Download Visual Studio Community 2022](https://visualstudio.microsoft.com/vs/community/)

- **WebView2 Runtime**
  - Required for WebView2 functionality.
  - [Download WebView2 Runtime](https://developer.microsoft.com/en-us/microsoft-edge/webview2/#download-section)

---

## Setup Instructions

### 1. Clone the Repository
```bash
$ git clone https://github.com/yourusername/MobileView-Browser.git
$ cd MobileView-Browser
```

### 2. Install Dependencies
Ensure you have the WebView2 runtime installed and the necessary NuGet package.
```bash
# Install dependencies via NuGet
$ nuget install Microsoft.Web.WebView2
```

### 3. Build the Application
1. Open the solution file (`MobileView-Browser.sln`) in Visual Studio.
2. Restore NuGet packages.
3. Build the solution (`Ctrl+Shift+B`).

### 4. Run the Application
Press `F5` in Visual Studio to debug and run the app.

---
## Extension Support

---

## Contributing

Contributions are welcome! Follow these steps to contribute:

1. Fork the repository.
2. Create a new branch for your feature or bug fix.
3. Commit your changes and push to your branch.
4. Submit a pull request.

---

## License
This project is licensed under the MIT License. See the `LICENSE` file for details.

---

## Support

If you encounter any issues or have questions, please [open an issue](https://github.com/yourusername/MobileView-Browser/issues).

---

## Acknowledgments
- **Microsoft Edge WebView2** for providing a modern browser control.
- **Visual Studio** for its powerful development environment.

Happy Browsing!
