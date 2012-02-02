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

namespace WCFTestService {
	class MainClass {
		static void Main ()
		{
			StartServices ();
		}

		static void StartServices ()
		{
			List<Thread> threads = new List<Thread> ();

			foreach (Type type in System.Reflection.Assembly.GetExecutingAssembly ().GetTypes ()) {
				Type contractType;
				Type serviceType = type;

				if (!serviceType.IsDefined (typeof (TestServiceAttribute), true))
					continue;

				if (serviceType.GetInterfaces ().Length != 1) {
					Console.WriteLine ("The type {0} implements {1} interfaces, it can only implement 1 interface to be a service", serviceType.FullName, serviceType.GetInterfaces ().Length);
					continue;
				}

				contractType = serviceType.GetInterfaces () [0];

				Thread t = new Thread ((v) => {
					RunService (serviceType, contractType, serviceType.Name);
				});
				t.Start ();
				threads.Add (t);
			}

			foreach (Thread t in threads)
				t.Join ();
		}
		
		static void RunService (Type serviceType, Type contractType, string relativeUri)
		{
			UriBuilder builder = new UriBuilder ();
			builder.Host = "127.0.0.1";
			builder.Port = 9999;
			builder.Scheme = Uri.UriSchemeHttp;
			var uri = new Uri (builder.Uri, new Uri ("/" + relativeUri.Trim ('/') + "/", UriKind.Relative));

			var binding = new BasicHttpBinding ();

			var host = new ServiceHost (serviceType, uri);

			ServiceMetadataBehavior smb = host.Description.Behaviors.Find<ServiceMetadataBehavior> ();
			// If not, add one
			smb = smb ?? new ServiceMetadataBehavior ();
			smb.HttpGetEnabled = true;
			smb.MetadataExporter.PolicyVersion = PolicyVersion.Policy15;
			host.Description.Behaviors.Add (smb);
			// Add MEX endpoint
			host.AddServiceEndpoint (ServiceMetadataBehavior.MexContractName, MetadataExchangeBindings.CreateMexHttpBinding (), "mex");

			host.AddServiceEndpoint (contractType, binding, uri);

			host.Open ();

			Console.WriteLine ("{0} is now running, press enter to stop.", serviceType.Name);
			Console.ReadLine ();
		}
	}


}

