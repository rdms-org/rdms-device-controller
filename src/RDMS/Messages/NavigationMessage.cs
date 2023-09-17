using CommunityToolkit.Mvvm.Messaging.Messages;

namespace RDMS.Messages
{
    /// <summary>
    /// Provides a message format to exchange navigation information between view models.
    /// </summary>
    public class NavigationMessage : ValueChangedMessage<string>
    {
        /// <summary>
        /// Creates a new <see cref="NavigationMessage"/> instance.
        /// </summary>
        /// <param name="value">The navigation source</param>
        public NavigationMessage(string value) : base(value) { }
    }
}
