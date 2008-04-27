using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

using ChatterBot.Goals;

namespace ChatterBot
{
	class ChatEngine
	{
		public Log Log = new Log();
		public OptionList OptionList = new OptionList();
		public DateTime LastActed;

		public ConversationState State;

		List<Type> GoalTypes = new List<Type>();
		List<Goal> Goals = new List<Goal>();

		Goal current;
		Random random = new Random();

		public ChatEngine()
		{
			Initalize();
		}

		public void Initalize()
		{
			RegisterGoals(Assembly.GetExecutingAssembly());
		}

		void RegisterGoals(Assembly assembly)
		{
			foreach (Type t in assembly.GetTypes())
			{
				if (t.IsSubclassOf(typeof(Goal)))
				{
					GoalTypes.Add(t);
				}
			}
		}

		void CreateGoal(Type type)
		{
			ConstructorInfo constructor = type.GetConstructor(new Type[] { });
			if (constructor != null)
			{
				Goal goal = (Goal)constructor.Invoke(new object[] { });
				Goals.Add(goal);
			}
		}

		void UpdateGoal()
		{
			if (current != null && current.IsCompleted)
			{
				Goals.Remove(current);
				current = null;
			}

			if (current == null)
			{
				if (Goals.Count > 0)
				{
					current = Goals[random.Next(Goals.Count)];
					Console.WriteLine("Current Goal: " + current.GetType().Name);
				}
				else
					CreateGoal(GoalTypes[random.Next(GoalTypes.Count)]);
			}
		}

		public string Act()
		{
			UpdateGoal();

			string result = null;
			if (current != null)
			{
				result = current.Act(null);

				// if nothing has changed in log then do nothing

				// if question then try to answer

				// if statement was made then acknowlege or ask a question

				// if given order then acknowledge and try to execute (add to goals?)

				// if greet and not already greeted then greet

				// if name unknown ask name

				// ask about something interesting

				// tell a story

				// do nothing

				//if (Log.LastReceived.IsQuestion)
				//{
				//    result = "I.know not();";
				//}
				//else
				//{
				//    result = "I.agree();";
				//}

				if (result != null)
					Log.Add(new LogEntry("Self", result));
			}

			LastActed = DateTime.Now;
			return result;
		}

		public void Sense(string message, string source)
		{
			Log.Add(new LogEntry(source, message));
		}
	}
}