using System.Collections.Generic;

namespace LanceUpContactList.Notifications.Interfaces
{
	public interface INotifier
	{
		bool HasNotification();
		List<Notification> GetNotifications();
		void Handle(Notification notification);
	}
}
