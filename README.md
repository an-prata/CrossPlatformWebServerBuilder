# CrossPlatformWebServerBuilder
[![Build](https://github.com/an-prata/CrossPlatformWebServerBuilder/actions/workflows/dotnet.yml/badge.svg)](https://github.com/an-prata/CrossPlatformWebServerBuilder/actions/workflows/dotnet.yml)

A cross platform version of my previous WinForms app (https://github.com/an-prata/WebServerBuilder) for quickly making node.js scripts to host a website.

The bottom has two text boxes, the top is the path to a local file you want to host, the bottom is the URL it should be hosted on, not including the address i.e. /my/epic/url. You would then connect to this URL with http://myip:myport/my/epic/url. To start making a script hit the `Add Requirements` button, then use the text boxes and `Add Get Statement` button to add files to host to the script. To finish making your script click the `Add Listen` button, you may want to edit the port number, the default is 80, which allows you while to connect without giving a port (http://myip/my/epic/url).
