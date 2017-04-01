using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Web.Http;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Authentication;
using Microsoft.Azure.Mobile.Server.Config;
using Owin;

namespace BetterTodoListServer
{
	public class Startup
	{
		public Startup()
		{
		}

		public void Configuration(IAppBuilder app)
		{

			HttpConfiguration conf = new HttpConfiguration();

			new MobileAppConfiguration()
				.MapApiControllers()
				.ApplyTo(conf);

			// Use Entity Framework Code First to create database tables based on your DbContext
			Database.SetInitializer(new MobileServiceInitializer());


			MobileAppSettingsDictionary settings = conf.GetMobileAppSettingsProvider().GetMobileAppSettings();

			if (string.IsNullOrEmpty(settings.HostName))
			{
				app.UseAppServiceAuthentication(new AppServiceAuthenticationOptions
				{
					// This middleware is intended to be used locally for debugging. By default, HostName will
					// only have a value when running in an App Service application.
					SigningKey = ConfigurationManager.AppSettings["SigningKey"],
					ValidAudiences = new[] { ConfigurationManager.AppSettings["ValidAudience"] },
					ValidIssuers = new[] { ConfigurationManager.AppSettings["ValidIssuer"] },
					TokenHandler = conf.GetAppServiceTokenHandler()
				});
			
			}


		}
	}


	public class MobileServiceInitializer : CreateDatabaseIfNotExists<MobileServiceContext>
	{
		protected override void Seed(MobileServiceContext context)
		{
			List<BetterTodoItem> todoItems = new List<BetterTodoItem>
			{
				new BetterTodoItem { Id = Guid.NewGuid().ToString(), Text = "First item", Completed = false },
				new BetterTodoItem { Id = Guid.NewGuid().ToString(), Text = "Second item", Completed = false }
			};

			foreach (BetterTodoItem todoItem in todoItems)
			{
				context.Set<BetterTodoItem>().Add(todoItem);
			}

			base.Seed(context);
		}
	}
}
