using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Collections;

namespace Infrastructure.ViewModel
{
    public abstract class ValidationBindable : Bindable, IValidationBindable
    {
        protected readonly Dictionary<string, List<ValidationResult>> _fieldErrors = new Dictionary<string, List<ValidationResult>>();

        public virtual bool HasErrors => this._fieldErrors.Count > 0;

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public IEnumerable GetErrors(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
                return this._fieldErrors.SelectMany(kvp => kvp.Value);

            if (this._fieldErrors.ContainsKey(propertyName))
                return this._fieldErrors.TryGetValue(propertyName, out var errors)
                    ? errors
                    : Enumerable.Empty<object>();

            return Enumerable.Empty<object>();
        }

        /// <summary> Raises an event on <see cref="INotifyDataErrorInfo.ErrorsChanged"/> to indicate that the validation errors have changed. </summary>
        /// <param name="propertyName">Property name of which the validation errors changed.</param>
        public void NotifyErrorsChanged([CallerMemberName] string propertyName = null)
        { 
            SetPropertyChangedOrChangingOrNotifyDataErrorChangedInUiContext(null, null, ErrorsChanged, propertyName);
        }

        /// <summary>
        /// Löscht alle Fehler, das UI wird dabei ebenfalls aktualisiert.
        /// Ausnahme: Typkonvertierungsfehler werden hier nicht bereinigt, sondern nur über das UI.
        /// </summary>
        public void ClearAllErrors()
        {
            var propNames = this._fieldErrors.Select(x => x.Key).ToList();

            this._fieldErrors.Clear();

            foreach (var propertyName in propNames)
            {
                this.NotifyPropertyChanging(nameof(this.HasErrors));
                this.NotifyErrorsChanged(propertyName);
                this.NotifyPropertyChanged(nameof(this.HasErrors));
            }
        }

        /// <summary>
        /// Löscht alle FieldValidation-Fehler (Typ: <see cref="ValidationAttribute"/>), das UI wird dabei ebenfalls aktualisiert.
        /// </summary>
        /// <param name="propertyNames">Falls angegeben, werden die entsprechenden Properties bereinigt, ansonsten werden alle FieldValidation bereinigt</param>
        public void ClearFieldValidationErrors(params string[] propertyNames)
        {
            if (propertyNames.Length == 0)
            {
                propertyNames = this._fieldErrors.Select(x => x.Key).ToArray();

                this._fieldErrors.Clear();
            }

            foreach (var propertyName in propertyNames)
            {
                this._fieldErrors.Remove(propertyName);

                this.NotifyPropertyChanging(nameof(this.HasErrors));
                this.NotifyErrorsChanged(propertyName);
                this.NotifyPropertyChanged(nameof(this.HasErrors));
            }
        }

        /// <summary> 
        /// FieldValidation-Fehler werden gesetzt und vorhandene Fehler der gleichen Property werden ersetzt.
        /// </summary>
        /// <param name="errors">Liste der Fehler (die Fehler können auch von unterschiedlichen Properties stammen)</param>
        public virtual void SetFieldValidationErrors(IEnumerable<ValidationResult> errors)
        {
            if (errors == null)
                return;

            foreach (var errorGroup in errors.GroupBy(x => x.MemberNames.First()))
            {
                string propertyName = errorGroup.Key;

                this.NotifyPropertyChanging(nameof(this.HasErrors));
                this._fieldErrors[propertyName] = errorGroup.ToList();
                this.NotifyErrorsChanged(propertyName);
                this.NotifyPropertyChanged(nameof(this.HasErrors));
            }
        }

        /// <summary> 
        /// FieldValidation-Fehler werden für die Property <paramref name="propertyName"/> gesetzt und vorhandene Fehler ersetzt.
        /// </summary>
        /// <param name="errors">Liste der Fehler</param>
        /// <param name="propertyName">Propertyname</param>
        public virtual void SetFieldValidationErrors(IEnumerable<string> errors, [CallerMemberName] string propertyName = null)
        {
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentNullException(nameof(propertyName));

            if (errors != null && errors.Any())
            {
                this.NotifyPropertyChanging(nameof(this.HasErrors));
                this._fieldErrors[propertyName] =
                        errors.Select(x => new ValidationResult(x, new List<string> { propertyName })).ToList();
                this.NotifyErrorsChanged(propertyName);
                this.NotifyPropertyChanged(nameof(this.HasErrors));
            }
            else if (this._fieldErrors.ContainsKey(propertyName))
            {
                this.NotifyPropertyChanging(nameof(this.HasErrors));
                this._fieldErrors.Remove(propertyName);
                this.NotifyErrorsChanged(propertyName);
                this.NotifyPropertyChanged(nameof(this.HasErrors));
            }
        }
    }
}
