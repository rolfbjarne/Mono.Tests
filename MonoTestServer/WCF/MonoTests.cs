using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Security;
using System.Threading;
using System.Xml;

namespace MonoTestServer.WCF {
	class MonoTests : IMonoTests {
		public void ThrowMonoFault (string message)
		{
			throw new FaultException<MonoFault> (new MonoFault (message), new FaultReason (message));
		}
	}
}

