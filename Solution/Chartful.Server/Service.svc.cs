using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.PeerToPeer;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Chartful.Server.Bean;
using Chartful.Server.DAO;

namespace Chartful.Server
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "Service1" dans le code, le fichier svc et le fichier de configuration.
    // REMARQUE : pour lancer le client test WCF afin de tester ce service, sélectionnez Service1.svc ou Service1.svc.cs dans l'Explorateur de solutions et démarrez le débogage.
    public class Service : IService
    {
        #region IService Member

        public bool SignUp(string username, string password, string email)
        {
            //Console.WriteLine("SignUp(\"{0}\", \"{0}\")");
            bool success = false;
            MemberDAO memberDao = MemberDAO.Instance;
            Member member = memberDao.select(username);
            if (member == null)
            {
                memberDao.create(username, password, email);
                success = true;
            }
            return success;
        }

        public bool SignIn(string username, string password)
        {
            //Console.WriteLine("LogIn(\"{0}\", \"{0}\")");
            MemberDAO memberDao = MemberDAO.Instance;
            Member member = memberDao.select(username);
            return (member != null);
        }

        public bool SignOut()
        {
            //Console.WriteLine("LogOut()");
            return true;
        }

        public List<string> GetFileNameList()
        {
            //Console.WriteLine("GetFileNameList()");
            FileDAO f = FileDAO.Instance;
            List<string> fileNameList = new List<string>();
            foreach (File file in f.select())
                fileNameList.Add(file.Name);
            return fileNameList;
        }

        public List<PeerName> GetColloboratorPeerName()
        {
            //Console.WriteLine("GetColloboratorPeerName()");
            return null;
        }

        #endregion
    }
}
