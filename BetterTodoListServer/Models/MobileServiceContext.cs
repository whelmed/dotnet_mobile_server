using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using Microsoft.Azure.Mobile.Server.Tables;

namespace BetterTodoListServer
{
	public class MobileServiceContext : DbContext
	{
		private const string connectionStringName = "Name=MS_TableConnectionString";

		public MobileServiceContext() : base(connectionStringName)
		{

		}

		public DbSet<BetterTodoItem> TodoItems { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Conventions.Add(
				new AttributeToColumnAnnotationConvention<TableColumnAttribute, string>(
					"ServiceColumnTable", (property, attributes) => attributes.Single().ColumnType.ToString()));
		}
	}
}
