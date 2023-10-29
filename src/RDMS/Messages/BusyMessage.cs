using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDMS.Messages
{
    /// <summary>
    /// Provides a message format to exchange Busy Indicator information between view models.
    /// </summary>
    public class BusyMessage : ValueChangedMessage<bool>
    {
        /// <summary>
        /// Creates a new <see cref="BusyMessage"/> instance.
        /// </summary>
        /// <param name="value">Gets or sets whether to show the busy indicator.</param>
        public BusyMessage(bool value) : base(value) { }
    }
}
