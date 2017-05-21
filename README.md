# Elmah.WebService


a an elmah target for logging to a remote elmah web service. 

this solution contains 3 projects. 

 - the elmah target class WebServiceErrorLog. 
 - an mvc website with the WebServiceErrorLog target class specified in the web.config. 
 - a webapi website with a controller that accepts elmah errors using POSTS to /api/elmah and then relogs them to a locally configured elmah target (eg, the elmah target specified in the `<errorLog>` tag
   in the web.config, or the memory target if there is none).

configuration. 

3 possible ways in web.config:

1. ConfigObjectName="ElmahLogConfigObject"
 
this retrieves a IConfigurationProvider from the asp.net cache using the key "ElmahLogConfigObject". this IConfigurationProvider is used to configure the Elmah.WebService.Client object
    
2. ConfigurationProviderType="Elmah.WebService.Client.ConfigurationProvider"

this creates a new instance of Elmah.WebService.Client.ConfigurationProvider which implements IConfigurationProvider to configure the Elmah.WebService.Client object. 
    
3.  WebServiceUrl="http://localhost/api/elmah" WebServiceUrlTimeout="5" WebServiceUseCompression="true" applicationName="Website Application Name"

these are raw options in web.config used to configure ElmahWSClient class. 

    
Important: version 1.2 of elmah does not support logging asynchronously. so, if your elmah webservice is slow or down, it will block the exception handling/display on your client website until it either completes or times out.
If you get pauses during exception logging this is probably why. 
