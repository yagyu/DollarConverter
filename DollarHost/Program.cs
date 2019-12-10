using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using WcfDollarLibrary;


namespace DollarHost
{
    class Program
    {
        static void Main(string[] args)
        {
            Uri address = new Uri("http://localhost:8733/Dollar/");

            // Step 2: Create a ServiceHost instance.
            ServiceHost host = new ServiceHost(typeof(DollarConverter), address);

            try
            {
 
                host.AddServiceEndpoint(typeof(IDollar), new WSHttpBinding(), "CalculatorService");


                ServiceMetadataBehavior behavior = new ServiceMetadataBehavior();
                behavior.HttpGetEnabled = true;
                host.Description.Behaviors.Add(behavior);


                host.Open();
                Console.WriteLine("Listening...");

                Console.WriteLine("Press any key to stop.");

                Console.ReadKey();
                host.Close();
            }
            catch (CommunicationException ce)
            {

                Console.WriteLine("An exception occurred: {0}", ce.Message);
                host.Abort();
            }
        }
    }
}
