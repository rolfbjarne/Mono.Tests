// MainClass.cs:
//
// Authors:
//	Rolf Bjarne Kvinge <rolf@xamarin.com>
//
// Copyright 2012 Xamarin Inc. All rights reserved
//

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

using NUnitLite.Runner;

using Mono.Unit;
using Mono.Unit.Framework;
using Mono.Unit.Framework.Runner;

namespace System_ServiceModel_Tests {
	class MainClass {
		static int Main (string [] args)
		{
			return ConsoleRunner.Run (args, typeof (MainClass).Assembly);
		}
	}
}

