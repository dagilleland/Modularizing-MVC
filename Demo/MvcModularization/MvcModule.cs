using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace MvcModularization
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class MvcModule : IRouteHome
    {
        public string DefaultController { get; private set; }
        public string DefaultAction { get; private set; }
        public string DefaultArea { get; private set; }
        public string DefaultLinkText { get; private set; }
        public string DefaultLinkUrl { get; private set; }
        public ICollection<string> Namespaces { get; private set; } = new List<string>();
        public MvcModule(string defaultController, string defaultAction, string defaultArea, string defaultLinkText, string defaultLinkUrl)
        {
            if (string.IsNullOrEmpty(defaultController))
                throw new ArgumentException($"{nameof(defaultController)} is null or empty.", nameof(defaultController));
            if (string.IsNullOrEmpty(defaultAction))
                throw new ArgumentException($"{nameof(defaultAction)} is null or empty.", nameof(defaultAction));
            if (string.IsNullOrEmpty(defaultArea))
                throw new ArgumentException($"{nameof(defaultArea)} is null or empty.", nameof(defaultArea));
            if (string.IsNullOrEmpty(defaultLinkText))
                throw new ArgumentException($"{nameof(defaultLinkText)} is null or empty.", nameof(defaultLinkText));
            if (string.IsNullOrEmpty(defaultLinkUrl))
                throw new ArgumentException($"{nameof(defaultLinkUrl)} is null or empty.", nameof(defaultLinkUrl));

            DefaultController = defaultController;
            DefaultAction = defaultAction;
            DefaultArea = defaultArea;
            DefaultLinkText = defaultLinkText;
            DefaultLinkUrl = defaultLinkUrl;
        }

        internal MvcModule Clone() { return this.MemberwiseClone() as MvcModule; }
        internal void AddNamespaces(ICollection<string> namespaces)
        {
            if (namespaces == null) throw new ArgumentNullException(nameof(namespaces));
            foreach (var item in namespaces)
                Namespaces.Add(item);
        }
        public void FindActions(Assembly[] assemblies)
        {
            foreach (var ns in Namespaces)
            {
                string searchArea = ns;
                if (searchArea.EndsWith(".*")) searchArea = searchArea.Substring(0, searchArea.Length - 2);
                // FIND the controller class!!
                foreach (var assm in assemblies)
                {
                    try
                    {
                        var foundControllers = assm
                            .DefinedTypes
                            //.GetTypes()
                            .Where(x => x.IsClass && x.IsSubclassOf(typeof(Controller)) &&
                            x.Namespace.StartsWith(searchArea));
                        //x.FullName == $"{searchArea}.{registration.DefaultController}Controller");
                        foreach (var controller in foundControllers)
                        {
                            AvailableControllers.Add(controller.FullName, new AvailableActions());
                            var methods = controller.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance);
                            foreach (var action in methods)
                            {
                                string actionNameKey = 
                                    $"{action.Name}({string.Join(", ", action.GetParameters().Select(x => ParameterDescription(x)))})";
                                AvailableControllers[controller.FullName].Add(actionNameKey, action);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // TODO: Handle Exception!
                    }
                }
            }

        }
        private string ParameterDescription(ParameterInfo info)
        {
            string text = info.Name;
            Type type = info.ParameterType;
            if(type.ContainsGenericParameters)
            {
                text = type.Name + " of " + string.Join(", ", type.GenericTypeArguments.Select(x => x.Name)) + text;
            }
            else if(type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                text = type.GenericTypeArguments[0].Name + "? " + text;
            }
            else
            {
                text = info.ParameterType.Name + " " + text;
            }
            return text;
        }
        public AvailableControllers AvailableControllers { get; private set; } = new AvailableControllers();
    }
}
