# Elmah.WebService


a an elmah target for logging to a remote elmah web service. 

this solution contains 3 projects. 

 - the elmah target class WebServiceErrorLog. 
 - an mvc website with the WebServiceErrorLog target class specified in the web.config. 
 - a webapi website with a controller that accepts elmah errors using POSTS to /api/elmah and then relogs them to a locally configured elmah target (eg, the elmah target specified in the `<errorLog>` tag
   in the web.config, or the memory target if there is none).
