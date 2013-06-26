using System;
using System.Collections.Generic;
using System.Text;
using Castle.Core.Resource;
using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;

namespace Somnus.Common
{
    public class CastleContext
    {
        private static IWindsorContainer container = null;

        private static object obj_lock = new object();

        public static IWindsorContainer Instence
        {
            get
            {
                if (container == null)
                {
                    lock (obj_lock)
                    {
                        if (container == null)
                        {
                            container = new WindsorContainer(new XmlInterpreter());
                        }
                    }
                }
                return container;
            }
        }

        public static IWindsorContainer Get()
        {
            return Instence;
        }

        public void ReloadWinsorContainer()
        {
            lock (obj_lock)
            {
                if (container != null)
                {
                    container.Dispose();
                }

                container = null;
            }
        }
    }
}
