// FaultContractInfo.cs:
//
// Authors:
//	Rolf Bjarne Kvinge <rolf@xamarin.com>
//
// Copyright 2012 Xamarin Inc. All rights reserved
//

using System;
using System.ComponentModel;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading;

using Mono.Unit.Framework;
using NUnit.Framework;

using MonoTestServer.WCF;

namespace System_ServiceModel_Dispatcher {
	[TestFixture]
	public class FaultContractInfo : TestFixtureBase {
		const string EndPoint = "http://192.168.1.2:8081/WCF/MonoTests.svc";

		[Test]
		[Asynchronous]
		public void FaultContractInfoTest ()
		{
			AsyncCompletedEventArgs acea = null;
			MonoTestsClient client;

			client = new MonoTestsClient (new BasicHttpBinding (), new EndpointAddress (EndPoint));
			client.ThrowMonoFaultCompleted += (sender, args) =>
			{
				acea = args;
			};
			client.ThrowMonoFaultAsync ("msg");

			EnqueueConditional (() => acea != null);
			Enqueue (() =>
			{
				Assert.NotNull (acea.Error, "#error");
				Assert.That (acea.Error.Message == "msg", "#msg");
				Assert.That (acea.Error.GetType () == typeof (FaultException<MonoFault>), "#type");
			});
			EnqueueTestComplete ();
		}
	}
}

