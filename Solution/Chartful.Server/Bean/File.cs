using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chartful.Server.Bean
{
    public class File
    {
        #region properties

        /// <summary>
        /// Id du fichier.
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Nom du fichier.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Tableau de bit représentant le contenu du fichier.
        /// </summary>
        public byte[] Content { get; set; }

        #endregion

        /// <summary>
        /// Constructeur de la classe File.
        /// </summary>
        /// <param name="id">Id du fichier.</param>
        /// <param name="name">Nom du fichier.</param>
        /// <param name="content">Contenu du fichier.</param>
        public File(int id, string name, byte[] content)
        {
            this.Id = id;
            this.Name = name;
            this.Content = content;
        }

        /// <summary>
        /// Constructeur de la classe File.
        /// </summary>
        /// <param name="name">Nom du fichier.</param>
        /// <param name="content">Contenu du fichier.</param>
        public File(string name, byte[] content)
        {
            this.Name = name;
            this.Content = content;
        }
    }
}