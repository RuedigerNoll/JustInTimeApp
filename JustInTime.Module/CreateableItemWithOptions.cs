using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Persistent.Base;

namespace JustInTime.Module
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = false, AllowMultiple = false)]
    public class CreatableItemWithOptionsAttribute : CreatableItemAttribute
    {
        /// <summary>
        /// Initializes a new instance of the CreatableItemWithOptionsAttribute class.
        /// </summary>
        public CreatableItemWithOptionsAttribute(bool isCreatableItem, CreatableItemOptions option)
            : base(isCreatableItem)
        {
            Option = option;
        }

        private CreatableItemOptions _option;
        public CreatableItemOptions Option
        {
            get { return _option; }
            set { _option = value; }
        }
    }

    public enum CreatableItemOptions
    {
        Never,
        VisibleInEmbeddedListViews
    }
}
