using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Net.PeerToPeer;

namespace Chartful.Server
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom d'interface "IService1" à la fois dans le code et le fichier de configuration.
    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        bool SignUp(string username, string password, string email);

        [OperationContract]
        bool SignIn(string username, string password);

        [OperationContract]
        bool SignOut();

        [OperationContract]
        List<string> GetFileNameList();

        [OperationContract]
        List<PeerName> GetColloboratorPeerName();
    }
}
