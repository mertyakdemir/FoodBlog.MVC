# http://www.foodblogmvc.somee.com/

Please change the following fields for the project to work local environment.

FoodBlog.WebApp -> Web.config

	  <add key="MailUser" value="test@gmail.com"/>
	  <add key="MailPass" value="TestPassword"/>
	  <add key="SiteRootUri" value="TestURI"/>

	  <connectionStrings>
           <add name="DatabaseContext" connectionString="Server=TestServerName\SQLEXPRESS;Database=TestDB; 
	        Integrated Security=SSPI;" providerName="System.Data.SqlClient" />
      </connectionStrings>
      


