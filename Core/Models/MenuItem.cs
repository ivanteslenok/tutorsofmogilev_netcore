namespace Core.Models
{
    public class MenuItem
    {
        public bool IsActive;
        public string Name;
        public string CssClassName;
        public string Controller;
        public string Action;

        public MenuItem(
            string name, 
            string cssClassName, 
            bool isActive, 
            string controller, 
            string action
            )
        {
            Name = name;
            CssClassName = cssClassName;
            IsActive = isActive;
            Controller = controller;
            Action = action;
        }
    }
}