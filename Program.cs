using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace Test
{
    internal class Animal : IComparable<Animal>
    {
        internal string? Name { get; set; }
        internal int Age { get; set; }

        public int CompareTo(Animal? other) => other.Age > Age ? -1 : 1;

        internal void Print() => Console.WriteLine($"{Name,-10}{Age}");
    }
    internal class AnimalNameComparer : IComparer<Animal>
    {
        public int Compare(Animal? x, Animal? y)
        {
            return string.CompareOrdinal(x.Name, y.Name);
        }
    }

    internal class AnimalList : IEnumerable<Animal>
    {
        private List<Animal> _list = new();

        internal void Add(Animal a) => _list.Add(a);
        public IEnumerator<Animal> GetEnumerator() => new AnimalEnumerator(_list);

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
    internal class AnimalEnumerator : IEnumerator<Animal>
    {
        private List<Animal> _list;
        private int _index = -1;
        internal AnimalEnumerator(List<Animal> l) { _list = l; }
        public Animal Current => _list[_index];

        object IEnumerator.Current => Current;

        public void Dispose() { }

        public bool MoveNext()
        {
            _index++;
            return _index < _list.Count;
        }

        public void Reset() => _index = -1;
    }
    internal class RandomNumbersCollection : IEnumerable<int>
    {
        private readonly Random _random = new();

        public IEnumerator<int> GetEnumerator()
        {
            while (true) yield return _random.Next(1, 101);
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    internal interface ILogger
    {
        void Log(string? message);
        void LogInfo(string? message) => Log("[INFO] " + (message ?? "Внимание: пустое сообщение"));
        void LogWarning(string? message) => Log("[WARN] " + (message ?? "Внимание: пустое сообщение"));
        void LogError(string? message) => Log("[ERROR] " + (message ?? "Внимание: пустое сообщение"));
    }
    internal interface ITimestampedLogger : ILogger
    {
        void LogWithTimestamp(string? message) => Log($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} {message ?? "Внимание: пустое сообщение"}");
    }
    internal class ConsoleLogger : ILogger
    {
        public void Log(string? message)
        {
            Console.WriteLine(message);
        }
        void ILogger.LogInfo(string? message)
        {
            Log("CONSOLE INFO: " + message);
        }
    }
    internal class FileLogger : ILogger
    {
        private readonly List<string?> _fileContent = [];

        public void Log(string? message)
        {
            _fileContent.Add(message);
            Console.WriteLine($"[записано в файл] {message}");
        }
    }
    internal class TimestampedFileLogger : ITimestampedLogger
    {
        private readonly List<string?> _fileContent = [];
        public void Log(string? message)
        {
            _fileContent.Add(message);
            Console.WriteLine($"[записано в файл] {message}");
        }
    }

    internal interface ICache<TKey, TValue>
    {
        void Set(TKey key, TValue value);
        TValue Get(TKey key);
        void Remove(TKey key);
    }
    internal class FifoCache<TKey, TValue> : ICache<TKey, TValue> where TKey : notnull
    {
        private Dictionary<TKey, TValue> _cache = [];
        private Queue<TKey> _queue = new();
        private readonly int _capacity;

        public FifoCache(int capacity)
        {
            if (capacity <= 0) throw new ArgumentOutOfRangeException(nameof(capacity), "Емкость должна быть больше 0");
            _capacity = capacity;
        }
        public TValue Get(TKey key)
        {
            if (_cache.TryGetValue(key, out var value)) return value;
            throw new KeyNotFoundException($"Ключ '{key}' отсутствует в кэше");
        }

        public void Remove(TKey key)
        {
            if (!_cache.Remove(key)) return;
            _queue = new Queue<TKey>(_queue.Where(k => !EqualityComparer<TKey>.Default.Equals(k, key)));
        }

        public void Set(TKey key, TValue value)
        {
            if (_cache.ContainsKey(key))
            {
                _cache[key] = value;
                return;
            }
            _cache[key] = value;
            _queue.Enqueue(key);
            if (_cache.Count > _capacity)
            {
                TKey oldest = _queue.Dequeue();
                _cache.Remove(oldest);
            }
        }
    }

    internal class LruCache<TKey, TValue> : ICache<TKey, TValue> where TKey : notnull
    {
        private readonly Dictionary<TKey, LinkedListNode<KeyValuePair<TKey, TValue>>> _cache = [];
        private readonly LinkedList<KeyValuePair<TKey, TValue>> _list = new();
        private readonly int _capacity;
        public LruCache(int capacity)
        {
            if (capacity <= 0) throw new ArgumentOutOfRangeException(nameof(capacity), "Емкость должна быть больше 0");
            _capacity = capacity;
        }

        public TValue Get(TKey key)
        {
            if (!_cache.TryGetValue(key, out var node)) throw new KeyNotFoundException($"Ключ '{key}' отсутствует в кэше");
            MoveToFront(node);
            return node.Value.Value;
        }

        public void Set(TKey key, TValue value)
        {
            if (_cache.TryGetValue(key, out var existing))
            {
                existing.Value = new KeyValuePair<TKey, TValue>(key, value);
                MoveToFront(existing);
                return;
            }

            var node = _list.AddFirst(new KeyValuePair<TKey, TValue>(key, value));
            _cache[key] = node;

            if (_cache.Count > _capacity)
            {
                var last = _list.Last!;
                _list.RemoveLast();
                _cache.Remove(last.Value.Key);
            }
        }

        public void Remove(TKey key)
        {
            if (_cache.Remove(key, out var node)) _list.Remove(node);
        }

        private void MoveToFront(LinkedListNode<KeyValuePair<TKey, TValue>> node)
        {
            if (node == _list.First) return;
            _list.Remove(node);
            _list.AddFirst(node);
        }
    }

    internal class Program
    {
        private static void AnimalTest()
        {
            List<Animal> animals = new()
            {
                new(){Name = "Cat", Age = 15},
                new(){Name = "Dog", Age = 3},
                new(){Name = "Bird", Age = 2},
                new(){Name = "Shark", Age = 5},
                new(){Name = "Wolf", Age = 4},
                new(){Name = "Alien", Age = 95}
            };
            animals.Sort(); //Метод Sort не знает как сравнивать объекты Animal.
            foreach (var animal in animals) animal.Print();
            Console.WriteLine();
            animals.Sort(new AnimalNameComparer());
            foreach (var animal in animals) animal.Print();
            Console.WriteLine();
        }
        private static void EnumeratorTest()
        {
            AnimalList animals = new();
            animals.Add(new() { Name = "Cat", Age = 15 });
            animals.Add(new() { Name = "Dog", Age = 3 });
            animals.Add(new() { Name = "Bird", Age = 2 });
            animals.Add(new() { Name = "Shark", Age = 5 });
            animals.Add(new() { Name = "Wolf", Age = 4 });
            animals.Add(new() { Name = "Alien", Age = 95 });
            foreach (var animal in animals) animal.Print();
            Console.WriteLine();

            RandomNumbersCollection ints = new();
            //foreach(var i in ints) Console.WriteLine(i);
            IEnumerator<int> enumerator = ints.GetEnumerator();
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(enumerator.Current);
                enumerator.MoveNext();
            }
        }
        private static void LoggerTest()
        {
            ILogger console = new ConsoleLogger();
            console.LogInfo("Запуск программы");
            console.LogWarning("Мало памяти");
            console.LogError(null);

            ILogger file = new FileLogger();
            file.LogInfo("Сохранение данных");
            file.LogError("Файл не найден");

            ITimestampedLogger ts = new TimestampedFileLogger();
            ts.LogWithTimestamp("Событие произошло");
            ts.LogInfo("Обычный info");
            ts.LogWithTimestamp(null);
        }
        private static void FIFOTest()
        {
            try
            {
                // Код из примера на изображении
                var cache = new FifoCache<string, int>(3);
                cache.Set("one", 1);
                cache.Set("two", 2);
                cache.Set("three", 3);
                Console.WriteLine(cache.Get("one")); // Выведет 1
                cache.Set("four", 4); // Удалит "one" (самый старый)
                Console.WriteLine(cache.Get("one")); // Выбросит исключение
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine($"Перехвачено ожидаемое исключение: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Непредвиденная ошибка: {ex.Message}");
            }
        }
        private static void LruTest()
        {
            ICache<int, string> fifo = new FifoCache<int, string>(3);
            ICache<int, string> lru = new LruCache<int, string>(3);

            foreach (var cache in new[] { fifo, lru })
            {
                cache.Set(1, "one");
                cache.Set(2, "two");
                cache.Set(3, "three");
                cache.Get(1);
                cache.Set(4, "four");
            }

            Console.Write($"FIFO: ");
            for (int key = 1; key <= 4; key++)
            {
                try { fifo.Get(key); Console.Write($"{key}=есть  "); }
                catch (KeyNotFoundException) { Console.Write($"{key}=нет  "); }
            }
            Console.WriteLine();
            Console.Write($"LRU: ");
            for (int key = 1; key <= 4; key++)
            {
                try { lru.Get(key); Console.Write($"{key}=есть  "); }
                catch (KeyNotFoundException) { Console.Write($"{key}=нет  "); }
            }
            Console.WriteLine();
        }
        private static void Main(string[] args)
        {
            AnimalTest();
            EnumeratorTest();
            LoggerTest();
            FIFOTest();
            LruTest();

            Console.ReadKey();
        }
    }
}