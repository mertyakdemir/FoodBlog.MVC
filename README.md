# FoodBlog.MVC

Please change the following fields for the project to work local environment.

FoodBlog.WebApp -> Web.config

    <add key="MailHost" value="smtp.gmail.com"/>
	  <add key="MailPort" value="587"/>
	  <add key="MailUser" value="test@gmail.com"/>
	  <add key="MailPass" value="testpassword"/>
	  <add key="SiteRootUri" value="https://localhost:44344"/>

	<connectionStrings>
		<add name="DatabaseContext" providerName="System.Data.SqlClient"
			connectionString="Server=TestServer;Database=TestDB; Integrated Security=SSPI;"/>
	</connectionStrings>
