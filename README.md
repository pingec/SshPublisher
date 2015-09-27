# SshPublisher
A small utility which helps me deploy files to a web server quickly with using just SSH(SCP).

This is a cmd utility for uploading files via SSH using the SCP protocol with the main purpose to be used as a Windows Explorer context menu shortcut to make publishing files to the web quicker.

# Instructions
0. Create App.config from App.config.sample
		cp SshPublisher/App.config.sample SshPublisher/App.config
		
0. Modify App.config according to your needs (an example is also provided below)

0. Build the solution

0. Copy the resulting \bin\Release or \bin\Debug directory to a permanent location (I recommend C:\Program Files (x86)\SshPublisher)
		cp -R SshPublisher/bin/Release "/c/Program Files (x86)/SshPublisher"
	
0. Now you can upload files by calling the utility with file-to-be-uploaded as parameter
		"c:\Program Files (x86)\SshPublisher\SshPublisher.exe" c:\Users\pingec\Desktop\photo.jpeg

## Making it useful
The real purpose of this utility is so that I can quickly call it from the Windows Explorer context menu in order to publish files to the web via SSH.
To enable this, simply run 
	"\SshPublisher\utils\add explorer context menu shortcut for all files.reg" 
to make the appropriate changes to the registry.

![right click shortcut demo](https://raw.github.com/pingec/SshPublisher/master/demo/demo.png)

## App.config example
	<?xml version="1.0" encoding="utf-8" ?>
	<configuration>
	<startup>
		<supportedRuntime version="v4.0" sku=".NETFramework,Version=v4.5.2" />
	</startup>
	<appSettings>
		<add key="RemoteHost" value="example.server.com" />
		<add key="Username" value="YourUserName" />
		<add key="PrivateKeyPath" value="C:\Path\To\Your\Private\Key\MyPrivateKey.pem" />
		<add key="PrivateKeyPassphrase" value="MyPrivateKeyPassword" />
		<add key="RemoteHostDestinationPath" value="/var/www/public_stuff" />
		<add key="ClipboardUrlPrefix" value="http://example.server.com/public" />
	</appSettings>
	</configuration>

*ClipboardUrlPrefix* is optional. If it is set, the clipboard will be manipulated. A string comprised of ClipboardUrlPrefix + uploaded file will be added to clipboard.
For example with above config settings, when uploading a "myphoto.jpeg" file, "http://example.server.com/public/myphoto.jpeg" will be added to the clipboard.