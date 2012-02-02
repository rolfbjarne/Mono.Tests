using System;
using System.Collections.Generic;
using System.Reflection;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.NUnit.UI;

using Mono.Unit.Framework;
using Mono.Unit.Framework.Runner;

namespace MonoTouch
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the 
	// User Interface of the application, as well as listening (and optionally responding) to 
	// application events from iOS.
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		// class-level declarations
		UIWindow window;
		TouchRunner runner;

		//
		// This method is invoked when the application has loaded and is ready to run. In this 
		// method you should instantiate the window, load the UI into it and then make the window
		// visible.
		//
		// You have 17 seconds to return from this method, or iOS will terminate your application.
		//
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			window = new UIWindow (UIScreen.MainScreen.Bounds);
		
			runner = new TouchRunner (window);
			runner.Add (AsyncTestLoader.Load (GetType ().Assembly));
			
			UIApplication.CheckForIllegalCrossThreadCalls = false;
			
			window.RootViewController = new UINavigationController (runner.GetViewController ());
			window.MakeKeyAndVisible ();
			
			return true;
		}
	}
}

