using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Config;

namespace BetterTodoListServer
{
	[Authorize]
	public class BetterTodoItemController : TableController<BetterTodoItem>
	{

		protected override void Initialize(HttpControllerContext controllerContext)
		{
			base.Initialize(controllerContext);
			MobileServiceContext context = new MobileServiceContext();
			DomainManager = new EntityDomainManager<BetterTodoItem>(context, Request, enableSoftDelete: true);
		}


		// GET tables/BetterTodoItem
		public IQueryable<BetterTodoItem> GetAllBetterTodoItem()
		{
			return Query();
		}

		// GET tables/BetterTodoItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
		public SingleResult<BetterTodoItem> GetBetterTodoItem(string id)
		{
			return Lookup(id);
		}

		// PATCH tables/BetterTodoItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
		public Task<BetterTodoItem> PatchBetterTodoItem(string id, Delta<BetterTodoItem> patch)
		{
			return UpdateAsync(id, patch);
		}

		// POST tables/BetterTodoItem
		public async Task<IHttpActionResult> PostBetterTodoItem(BetterTodoItem item)
		{
			BetterTodoItem current = await InsertAsync(item);
			return CreatedAtRoute("Tables", new { id = current.Id }, current);
		}

		// DELETE tables/BetterTodoItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
		public Task DeleteBetterTodoItem(string id)
		{
			return DeleteAsync(id);
		}
	}
}
