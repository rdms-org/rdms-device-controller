using RDMS.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace RDMS.ViewModels.Pages
{
    public class ScheduleViewModel : ViewModelBase
    {
        public XmlLanguage? LanguageResources { get; init; } = null;

        public ScheduleViewModel()
        {
            Title = "Schedule Page";
            Description = "The schedule viewer page";

            // Initialize language resources.
            LanguageResources = XmlLanguage.GetLanguage(LocalizationHelper.GetText("IDENTIFIER"));
        }
    }
}
