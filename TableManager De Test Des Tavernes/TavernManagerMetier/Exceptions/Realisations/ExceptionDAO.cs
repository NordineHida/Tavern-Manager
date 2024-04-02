
namespace TavernManagerMetier.Exceptions.Realisations
{
    /// <summary>
    /// Exception générique de problème du DAO des tavernes
    /// </summary>
    public abstract class ExceptionDAO : ExceptionTavernManager
    {
        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        /// <param name="message">Message de l'exception</param>
        protected ExceptionDAO(string message) : base("Erreur de sauvegarde/chargement", message)
        {
        }
    }
}
