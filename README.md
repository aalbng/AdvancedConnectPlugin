# AdvancedConnectPlugin 
AdvancedConnect is a plugin for [KeePass](http://keepass.info) password manager which gives you the possibility to provide different applications for direct connections.


## Requirements

- Microsoft Windows with .NET/[Mono](http://www.mono-project.com/download/) 3.5 or newer.
- Unix/Linux with [Mono](http://www.mono-project.com/download/) 3.5 or newer.
- [KeePass](http://keepass.info) version 2.59 or newer.


## Installation

- Download the [latest](https://github.com/aalbng/AdvancedConnectPlugin/releases/latest) release.
- Copy the AdvancedConnectPlugin.plgx in your KeePass directory and restart the application.

## Usage

- The plugin adds a new menu item named **AdvancedConnect** under **Tools** menu.
- Use the **Main** tab in the **Options** dialog to configure the custom fields from which the plugin gets the connection method and the connection options field (overrides default options). <br /><br />
On windows operation system the native remote desktop client have no option to provide the username and password via command-line. The built-in rdp support is a little workaround wich provides this functionality by storing the credentials securely through the Windows Credential Manager API (so the password never appears on a command line). You have to configure the keepass connection field (containing ip or hostname), a connection method (e.g. rdp) and you can set additional parameters (e.g. /w:1440 /h:900).<br />
- Use the **Applications** tab in the **Options** dialog to configure your connection applications. <br />
The **Path** and **Commandline Options** column is also supporting keepass placeholders and OS environment variables.
- To use a **portable configuration** you have to create an emtpy file named **AdvancedConnect.xml** next to **KeePass.exe**. <br />
(If a portable\admin configuration file is available, the default configuration *%appdata%\Keepass\AdvancedConnect.xml* will be ignored)

### Configuration file resolution

The plugin looks for its **AdvancedConnect.xml** configuration in the following order and uses the first one it finds:

1. **Program directory** – next to **KeePass.exe** (portable / admin configuration).
2. **Current working directory** – the directory KeePass was started from (see below).
3. **User profile** – *%appdata%\KeePass\AdvancedConnect.xml* (created automatically if none of the above exists).

#### Per-database configuration via the working directory

Loading from the current working directory enables a **per-database configuration** scenario: place an **AdvancedConnect.xml** next to a specific **.kdbx** file and start KeePass from that folder (for example by double-clicking the database). KeePass then inherits that folder as its working directory and the plugin loads the configuration located there.

A typical use case is support work with a **separate KeePass database per customer**, each accompanied by its own plugin configuration, so opening a customer's database loads a dedicated connection environment.

> **Security warning:** The working directory is determined by how KeePass was started and is **not necessarily a trusted location** (for example a download folder from which a database was opened). Because the configuration defines which programs are launched and receives your entry credentials (`{USERNAME}`/`{PASSWORD}`), loading it from an untrusted directory can run an attacker-chosen program with your credentials.<br />
> To make this transparent, the plugin shows a **warning message** whenever the configuration is loaded from the working directory instead of the program directory or *%appdata%*. Only continue if you trust the origin of that file.


## Example
- Configure main options
<p align="center"><img src="https://github.com/aalbng/AdvancedConnectPlugin/blob/master/Doc/AdvancedConnect_Options-Main.png"/></p>
- Add your applications
<p align="center"><img src="https://github.com/aalbng/AdvancedConnectPlugin/blob/master/Doc/AdvancedConnect_Options-Applications.png"/></p>
- Set custom fields in keepass entries
<p align="center"><img src="https://github.com/aalbng/AdvancedConnectPlugin/blob/master/Doc/Keepass_CustomFields.png"/></p>
- Start your applications directly from keepass
<p align="center"><img src="https://github.com/aalbng/AdvancedConnectPlugin/blob/master/Doc/Keepass_ContexMenu.png"/></p>

## Security

Please take note that launching applications via command-line can expose your password arguments in the taskmanager.

The configuration file (**AdvancedConnect.xml**) defines which programs are launched and how your entry credentials are passed to them. Only use configuration files from a trusted source. When the configuration is loaded from the current working directory (see [Configuration file resolution](#configuration-file-resolution)) the plugin shows a warning, because that location may not be trusted.

## Repository

The main repository is hosted on [GitHub](https://github.com/aalbng/AdvancedConnectPlugin).

## Changelog

See [CHANGELOG](https://github.com/aalbng/AdvancedConnectPlugin/blob/master/AdvancedConnectPlugin/CHANGELOG.txt) file for details.

## Download

You can get all binaries from [here](https://github.com/aalbng/AdvancedConnectPlugin/releases).

## License

The source code in this repository is released under the Apache License, Version 2.0 license. <br />
See the [LICENSE](https://github.com/aalbng/AdvancedConnectPlugin/blob/master/AdvancedConnectPlugin/LICENSE.txt) file for details.


____
The AdvancedConnect plugin is inspired by [QuickConnect](https://github.com/cristianst85/QuickConnectPlugin) 
