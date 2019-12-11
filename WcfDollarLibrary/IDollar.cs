using System;
using System.ServiceModel;

namespace WcfDollarLibrary
{
    // UWAGA: możesz użyć polecenia „Zmień nazwę” w menu „Refaktoryzuj”, aby zmienić nazwę interfejsu „IService1” w kodzie i pliku konfiguracji.
    [ServiceContract]
    public interface IDollar
    {
        [OperationContract]
        [FaultContract(typeof(ArgumentOutOfRangeException))]
        [FaultContract(typeof(ArgumentNullException))]
        [FaultContract(typeof(FormatException))]
        string Convert(string value);

    }

}
