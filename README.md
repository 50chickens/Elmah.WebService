# Elmah.WebService


a an elmah target for logging to a remote elmah web service. 

this solution contains 3 projects. 

 - the elmah target class WebServiceErrorLog. 
 - an mvc website with the WebServiceErrorLog target class specified in the web.config. 
 - a webapi website with a controller that accepts elmah errors using POSTS to /api/elmah and then relogs them to a locally configured elmah target (eg, the elmah target specified in the `<errorLog>` tag
   in the web.config, or the memory target if there is none).

configuration. 

added ConfigurationProviderType option in web.config. it specifies a type which implements the IConfigurationProvider interface and has the GetWebServiceUrl, GetWebServiceUrlTimeout, GetWebServiceUseCompression methods to allow for those options to be passed to the ElmahWSClient in the constructor.


Important: version 1.2 of elmah does not support logging asynchronously. so, if your elmah webservice is slow or down, it will block the exception handling/display on your client website until it either completes or times out.
If you get pauses during exception logging this is probably why. 
