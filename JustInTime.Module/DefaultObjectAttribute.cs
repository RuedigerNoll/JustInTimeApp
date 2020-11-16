using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JustInTime.Module
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = true, AllowMultiple = false)]
    public class DefaultObjectAttribute : Attribute
    {
        public DefaultObjectAttribute(string targetPropertyName)
        {
            _targetPropertyName = targetPropertyName;
        }

        public DefaultObjectAttribute(string targetPropertyName, string parentBackRefPropertyName, string parentBackRefCollectionPropertyName)
        {
            _targetPropertyName = targetPropertyName;
            _parentBackRefPropertyName = parentBackRefPropertyName;
            _parentBackRefCollectionPropertyName = parentBackRefCollectionPropertyName;
        }

        // Fields...
        private string _parentBackRefCollectionPropertyName;
        private string _parentBackRefPropertyName;
        private string _targetPropertyName;

        public string TargetPropertyName
        {
            get { return _targetPropertyName; }
            set { _targetPropertyName = value; }
        }

        public string ParentBackRefPropertyName
        {
            get { return _parentBackRefPropertyName; }
            set { _parentBackRefPropertyName = value; }
        }

        public string ParentBackRefCollectionPropertyName
        {
            get { return _parentBackRefCollectionPropertyName; }
            set { _parentBackRefCollectionPropertyName = value; }
        }
    }
}
