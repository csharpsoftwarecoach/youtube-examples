using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Infrastructure.ViewModel
{
    /// <summary>
    /// Verwaltung und Benachrichtigung des Clients über Fehler, die aufgetreten sind.
    /// 
    /// Es wird zwischen zwei Typen unterschieden:
    /// - FieldValidation: Das sind Feldvalidierungen, die Validierungsregeln werden via DataAnnotation<see cref="ValidationAttribute"/>
    ///                    über die Property angelegt. Entweder werden die Standard-Annotation wie z.B. Required verwendet
    ///                    oder eigene.
    /// - EntityValidation: Das sind Entitätsvalidierungen, diese Regeln können komplexer sein und auch DB-Services verwenden.
    ///                     Diese Validierungen sollten bevorzugt werden, denn somit sind die Regeln außerhalb der Entitäten.
    /// </summary>
    public interface IValidationBindable : INotifyDataErrorInfo
    {
        /// <summary> 
        /// FieldValidation-Fehler werden gesetzt und vorhandene Fehler der gleichen Property werden ersetzt.
        /// </summary>
        /// <param name="errors">Liste der Fehler (die Fehler können auch von unterschiedlichen Properties stammen)</param>
        void SetFieldValidationErrors(IEnumerable<ValidationResult> errors);
        /// <summary> 
        /// FieldValidation-Fehler werden für die Property <paramref name="propertyName"/> gesetzt und vorhandene Fehler ersetzt.
        /// </summary>
        /// <param name="errors">Liste der Fehler</param>
        /// <param name="propertyName">Propertyname</param>
        void SetFieldValidationErrors(IEnumerable<string> errors, string propertyName);

        /// <summary>
        /// Löscht alle Fehler, das UI wird dabei ebenfalls aktualisiert.
        /// </summary>
        void ClearAllErrors();
        /// <summary>
        /// Löscht alle FieldValidation-Fehler (Typ: <see cref="ValidationAttribute"/>), das UI wird dabei ebenfalls aktualisiert.
        /// </summary>
        /// <param name="propertyNames">Falls angegeben, werden die entsprechenden Properties bereinigt, ansonsten werden alle FieldValidation bereinigt</param>
        void ClearFieldValidationErrors(params string[] propertyNames);


        /// <summary> Raises an event on <see cref="INotifyDataErrorInfo.ErrorsChanged"/> to indicate that the validation errors have changed. </summary>
        /// <param name="propertyName">Property name of which the validation errors changed.</param>
        void NotifyErrorsChanged([CallerMemberName] string propertyName = null);
    }
}
