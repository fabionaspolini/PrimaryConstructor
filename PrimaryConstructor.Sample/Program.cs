﻿using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace PrimaryConstructor.Sample
{
	class Program
	{
		static void Main(string[] args)
		{
			var services = new ServiceCollection();
			services.AddSingleton<MyDependency>();
			services.AddSingleton<MyDependencyTwo>();
			services.AddSingleton<MyMainService>();
			services.AddLogging(builder => builder.AddConsole());
			var injector = services.BuildServiceProvider();
			var myService = injector.GetService<MyMainService>();

			Console.WriteLine(myService.Greeting());
		}
	}

    [PrimaryConstructor]
	public partial class MyMainService

		// fields/props in based class with [PrimaryConstructor] will be injected
        : MyServiceBaseTwo
    {
		// readonly field will be injected
	    private readonly MyDependencyTwo _myDependencyTwo;

		// readonly prop will be injected
        public MyDependency MyDependency { get; }

		// initialized field will not be injected
        private readonly string _template = "{0} {1}!";

		// fields/props with [IgnorePrimaryConstructor] will not be injected
        [IgnorePrimaryConstructor] 
        private readonly string _ignored;

		// fields/props with [IncludePrimaryConstructor] will be injected
		[IncludePrimaryConstructor]
        private MyDependency _included;

		public string Greeting()
		{
			return string.Format(_template,
				MyDependency.GetName(),
				_myDependencyTwo.GetName());
		}
	}

	[PrimaryConstructor]
	public partial class MyServiceBaseTwo : MyServiceBase
	{
	}

	[PrimaryConstructor]
	public partial class MyServiceBase
	{
		private readonly ILogger<MyServiceBase> _logger;
	}

    [PrimaryConstructor]
    public partial class MyDependency
	{
		public string GetName()
		{
			return "Hello";
		}
	}
    
	[PrimaryConstructor]
    public partial class MyDependencyTwo
    {
	    public string GetName()
	    {
		    return "World";
	    }
    }
}
