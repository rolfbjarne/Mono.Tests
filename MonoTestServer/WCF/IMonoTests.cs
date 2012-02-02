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
	[ServiceContract (Namespace = "http://www.mono-project.com")]
	public interface IMonoTests {
		[OperationContract]
		[FaultContract (typeof (MonoFault), Action = "http://www.mono-project.com/MonoFault")]
		void ThrowMonoFault (string message);
	}
	
	[DataContract]
	public class MonoFault {
		[DataMember]
		public string Message { get; set; }

		public MonoFault (string message)
		{
			this.Message = message;
		}
	}
}

