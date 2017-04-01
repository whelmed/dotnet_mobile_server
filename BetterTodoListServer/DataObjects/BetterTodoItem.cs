using System;
using Microsoft.Azure.Mobile.Server;

namespace BetterTodoListServer
{
	public class BetterTodoItem : EntityData
	{
		public string Text { get; set; }
		public bool Completed { get; set; }
	}
}
