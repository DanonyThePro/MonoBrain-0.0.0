using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;


namespace MonoBrain.GameEngineScripts
{
    public interface IMonoBrain
    {
        void Start();

        void Update();
    }

    public class FileLoader
    {
        public static List<Action> start_Methods = new List<Action>();
        public static List<Action> update_Methods = new List<Action>();

        public void File_Loader(params Assembly[] assemblies)
        {
            var FileTypes = new List<Type>();

            foreach (Assembly assembly in assemblies)
            {
                FileTypes.AddRange(assembly.GetTypes()
                    .Where(t => typeof(IMonoBrain).IsAssignableFrom(t) && !t.IsAbstract && !t.IsInterface));
            }

            List<IMonoBrain> Files = new List<IMonoBrain>();

            foreach (var filetype in FileTypes)
            {
                var instance = (IMonoBrain)Activator.CreateInstance(filetype);
            
                start_Methods.Add(instance.Start);

                update_Methods.Add(instance.Update);
            }
        }
    }
}
