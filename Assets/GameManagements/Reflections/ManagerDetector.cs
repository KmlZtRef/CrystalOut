using System;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace GameManagements.Reflections
{
	public static class ManagerDetector
	{
		public static Type[] GetManagers()
		{
			try
			{
				Debug.Log("Finding Manager classes. Only classes in same assembly will be detected.");
				Assembly assembly = Assembly.GetExecutingAssembly();
				Type managerType = typeof(Manager);
				return assembly.GetTypes()
					.Where(t => managerType.IsAssignableFrom(t) && t.IsClass && !t.IsAbstract)
					.ToArray();
			}
			catch (ReflectionTypeLoadException e)
			{
				Debug.LogError($"Reflection Error. Detail : {e.Message}");
				return null;
			}
			catch (Exception e)
			{
				Debug.LogError(e.Message);
				return null;
			}
			finally
			{
				Debug.Log("Complete.");
			}
		}
	}
}