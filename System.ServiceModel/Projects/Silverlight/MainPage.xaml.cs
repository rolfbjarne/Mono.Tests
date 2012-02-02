using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Threading;

using NUnit.Framework;
using NUnitLite;
using NUnitLite.Runner;

using Mono.Unit;
using Mono.Unit.Framework;
using Mono.Unit.Framework.Runner;

namespace System.ServiceModel.Tests {
	public partial class MainPage : UserControl, TestListener {
		AsyncTestSuite suite;

		public MainPage ()
		{
			InitializeComponent ();

			suite = (AsyncTestSuite) AsyncTestLoader.Load (GetType ().Assembly);
			suite.BeginInvokeHandler = BeginInvoke;
			BeginInvoke (() => suite.RunAsync (this), TimeSpan.FromMilliseconds (1));
		}

		void BeginInvoke (InvokeHandler action, TimeSpan interval)
		{
			var timer = new DispatcherTimer ();
			timer.Interval = interval;
			timer.Tick += (object sender, EventArgs args) => {
				timer.Stop ();
				action ();
			};
			timer.Start ();
		}

		void TestListener.TestStarted (ITest test)
		{
		}

		void TestListener.TestFinished (TestResult result)
		{
			AsyncTestCase atc = result.Test as AsyncTestCase;

			if (atc != null) {
				lstTests.Items.Add (string.Format ("{0}: {1}", atc.FullName, result.ResultState.ToString ()));
			}
		}
	}
}

