using System;
using System.Collections.Generic;

namespace GameManagements
{
	// God Object가 되는 것을 막기 위해 매니저 클래스에서만 이 클래스에 의존하기
	public static class MessageBus
	{
		private static Dictionary<Type, Delegate> _handlers = new();

		public static void Subscribe<T>(Action<T> handler)
		{
			_handlers.TryGetValue(typeof(T), out var existing);
			_handlers[typeof(T)] = (Action<T>)existing + handler;
		}

		public static void Unsubscribe<T>(Action<T> handler)
		{
			if (_handlers.TryGetValue(typeof(T), out var existing))
				_handlers[typeof(T)] = (Action<T>)existing - handler;
		}

		public static void Publish<T>(T message)
		{
			if (_handlers.TryGetValue(typeof(T), out var existing))
				((Action<T>)existing)?.Invoke(message);
		}
	}
}