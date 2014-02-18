PusherMVC
=========

This is a test project to try out the pusher WebSocket in MVC 4.

###Installation

Create a new website in IIS (for example pushermvc.local). Set the app pool to _Integrated_, set the .NET framework version to 4.0 and change identity (in _Advanced settings_ to _NetworkService_).

####RavenDB
**For the first time only**: right-click to the RavenDB folder and and click _Convert to application_. Set a separate app pool with the with the same setting mentioned above. Navigate to http://_mysite_/ravendb. That should create all the other files you need to run RavenDB.It will run in embedded mode. This mode should be used for testing only.

