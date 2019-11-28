using FluentValidation;
using FluentValidation.Results;
using LanceUpContactList.Notifications;
using LanceUpContactList.Notifications.Interfaces;
using LanceUpContactList.Models;

namespace LanceUpContactList.Services
{
	public abstract class BaseService
	{
		private readonly INotifier _notifier;

		protected BaseService(INotifier notifier)
		{
			_notifier = notifier;
		}

		protected void Notify(ValidationResult validationResult)
		{
			foreach (var error in validationResult.Errors)
			{
				Notify(error.ErrorMessage);
			}
		}

		protected void Notify(string message)
		{
			_notifier.Handle(new Notification(message));
		}

		protected bool PerformValidation<TV, TE>(TV validation, TE entity) where TV : AbstractValidator<TE> where TE : Entity
		{
			var validator = validation.Validate(entity);

			if (validator.IsValid) return true;

			Notify(validator);

			return false;
		}
	}
}
