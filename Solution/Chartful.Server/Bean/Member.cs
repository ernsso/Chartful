using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chartful.Server.Bean
{
    public class Member
    {
        #region properties

        /// <summary>
        /// Id du membre.
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Login du membre
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Salt du membre, permettant de renforcer la sécurité de son mot de passe.
        /// </summary>
        public string Salt { get; set; }

        /// <summary>
        /// Hash de la concaténation login  + salt + password.
        /// </summary>
        public string Hash { get; set; }

        /// <summary>
        /// Adresse email du membre.
        /// </summary>
        public string Email { get; set; }

        #endregion

        /// <summary>
        /// Initialise une nouvelle instance de la classe Member.
        /// </summary>
        /// <param name="id">Id du membre.</param>
        /// <param name="login">Login du membre.</param>
        /// <param name="salt">Salt du membre.</param>
        /// <param name="hash">Hash du membre.</param>
        /// <param name="email">Adresse email du membre.</param>
        public Member(int id, string login, string salt, string hash, string email)
        {
            this.Id = id;
            this.Login = login;
            this.Salt = salt;
            this.Hash = hash;
            this.Email = email;
        }

        /// <summary>
        /// Initialise une nouvelle instance de la classe Member.
        /// </summary>
        /// <param name="login">Login du membre.</param>
        /// <param name="salt">Mot de passe du membre.</param>
        /// <param name="email">Adresse email du membre.</param>
        public Member(string login, string password, string email)
        {
            this.Login = login;
            this.Salt = Tools.GenerateRandom(16);
            this.Hash = Tools.GetHash(this.Login + this.Salt + password);
            this.Email = email;
        }
    }
}