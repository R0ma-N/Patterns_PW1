using System;
using System.Collections.Generic;

namespace Patterns_hw1
{
    #region 1.Провести рефакторинг кода из раздела «Повторяющаяся логика», применяя внедрение зависимостей к классу EntityBase.
    public interface IIdGenerator
    {
        long CalculateId();
    }

    public class DefaultIdGenerator : IIdGenerator
    {
        public long CalculateId()
        {
            long id = DateTime.Now.Ticks;
            return id;
        }
    }

    public abstract class EntityBase
    {
        public readonly IIdGenerator _idGenerator;
        public long Id { get; private set; }

        public EntityBase(IIdGenerator idGenerator)
        {
            _idGenerator = idGenerator;
            Id = _idGenerator.CalculateId();
        }
    }

    public class Store : EntityBase
    {
        public Store(IIdGenerator idGenerator) : base(idGenerator)
        {

        }
    }

    public class Customer : EntityBase
    {
        public string Description { get; set; }
        public Customer(IIdGenerator idGenerator) : base(idGenerator)
        {

        }
    }
    #endregion

    class Program
    {
        # region 2.Реализовать программу из раздела «Повторяющиеся фрагменты кода» с помощью делегата Func.
        private static Func<string, string, int, string> Format = ToFormat;

        private static void DummyFunc()
        {
            Format("Петя", "школьный друг", 30);
        }

        private static void DummyFuncAgain()
        {
            Format("Вася", "сосед", 54);
        }

        private static void DummyFuncMore()
        {
           Format("Николай", "сын", 4);
        }

        private static string ToFormat(string name, string description, int age)
        {
            string i = $"{name} - {description}, адрес: Москва, Россия, возраст {age}";
            Console.WriteLine(i);
            return i;
        }

        private static void MakeAction(Action action)
        {
            string methodName = action.Method.Name;
            Console.WriteLine("Начало работы метода {0}", methodName);
            action();
            Console.WriteLine("Окончание работы метода {0}", methodName);
        }

        private static List<Action> GetActionSteps()
        {
            return new List<Action>()
            {
                DummyFunc,
                DummyFuncAgain,
                DummyFuncMore
            };
        }
        #endregion
        static void Main(string[] args)
        {
            //1
            Store store = new Store(new DefaultIdGenerator());
            Customer customer = new Customer(new DefaultIdGenerator());
            Console.WriteLine(store.Id + "\n" + customer.Id);

            //2
            List<Action> actions = GetActionSteps();
            foreach (var action in actions)
            {
                MakeAction(action);
            }
        }
    }
}
