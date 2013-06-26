using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SomnusLogistic.Model
{
    public class Permission
    {
        /// <summary>
        /// auto_increment
        /// </summary>		
        private int _permissionid;
        public int PermissionID
        {
            get { return _permissionid; }
            set { _permissionid = value; }
        }
        /// <summary>
        /// ParentID
        /// </summary>		
        private int _parentid;
        public int ParentID
        {
            get { return _parentid; }
            set { _parentid = value; }
        }
        /// <summary>
        /// PermissionName
        /// </summary>		
        private string _permissionname;
        public string PermissionName
        {
            get { return _permissionname; }
            set { _permissionname = value; }
        }
        /// <summary>
        /// PermissionController
        /// </summary>		
        private string _permissioncontroller;
        public string PermissionController
        {
            get { return _permissioncontroller; }
            set { _permissioncontroller = value; }
        }
        /// <summary>
        /// PermissionAction
        /// </summary>		
        private string _permissionaction;
        public string PermissionAction
        {
            get { return _permissionaction; }
            set { _permissionaction = value; }
        }
        /// <summary>
        /// Icon
        /// </summary>		
        private string _icon;
        public string Icon
        {
            get { return _icon; }
            set { _icon = value; }
        }
        /// <summary>
        /// IsVisible
        /// </summary>		
        private bool _isvisible;
        public bool IsVisible
        {
            get { return _isvisible; }
            set { _isvisible = value; }
        }
        /// <summary>
        /// IsButton
        /// </summary>		
        private bool _isbutton;
        public bool IsButton
        {
            get { return _isbutton; }
            set { _isbutton = value; }
        }
        /// <summary>
        /// Sort
        /// </summary>		
        private int _sort;
        public int Sort
        {
            get { return _sort; }
            set { _sort = value; }
        }
        /// <summary>
        /// Description
        /// </summary>		
        private string _description;
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        /// <summary>
        /// IsDelete
        /// </summary>
        private bool _isDelete;
        public bool IsDelete
        {
            get { return _isDelete; }
            set { _isDelete = value; }
        }

        /// <summary>
        /// CreateDate
        /// </summary>		
        private DateTime _createdate;
        public DateTime CreateDate
        {
            get { return _createdate; }
            set { _createdate = value; }
        }

    }
}
