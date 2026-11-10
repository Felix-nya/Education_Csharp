# C# / .NET: шпаргалка

## База .Net

### Алфавит

[character set] языка программирования – набор символов, используемый для построения текста программы
- символы латинского алфавита
- цифры
- символ подчёркивания _
- специальные символы

### Лексема

[lexeme] – элементарная синтаксическая единица текста программы, то есть минимальная значимая последовательность символов алфавита

### Ключевое слово

[reserved word] – лексема языка программирования, имеющая предопределенный смысл для транслятора

### Метка

[label] – необязательный специальный идентификатор оператора программы, служащий для указания места безусловной передачи управления оператором безусловного перехода goto(синтаксис может меняться)

### Литерал

[literal] используется для записи непосредственного значения, например, чисел (числовые литералы – нумералы) и текстовых строк (символьные литералы)
- "abc"  '\u006A’  123  0x2A  3_000.5F

### Платформа

[platform] – наиболее общее описание программно аппаратной (в том числе сетевой) среды, на которой разрабатывается и/или развёртывается прикладное ПО. Обязательно включает архитектуру компьютера, тип ОС и средства доступа к данным (СУБД).

### Код (в понимании .NET)

Код, выполнение которого управляется средой выполнения (**CLR**)
.NET предоставляет среду выполнения, называемую **общеязыковой средой выполнения (CLR)**, которая запускает код и предоставляет службы, упрощающие процесс разработки.

### CLR

Общеязыковая среда исполнения CLR управляет
- памятью и потоками выполнения кода
- проверкой безопасности кода
- компиляцией в машинный код
- выполнением машинного кода

### Плюсы от использования CLR

- Повышение производительности
- Возможность и упрощение использование компонентов, разработанных на разных ЯП
- Сборка мусора
- Делегаты вместо указателей на функцию повышают типобезопасность и надёжность
- Поддержка упорядоченного механизма обработки исключений
- Поддержка модели свободных поток
- Поддержка кастомныхатрибутов

### Сборка

Единица повторного использования, версионирования и безопасности

## Основы типов

### Методы Object

```csharp
ToString();
Equals();
GetHashCode();
GetType();
```

### Приведение типов (casting)

Два вида приведения:
- Явное (`Explicit`): Требует оператор (Type). Риск `InvalidCastException`.
- Неявное (`Implicit`): Происходит автоматически, безопасно.
```csharp
int a, b;
double c;
a = 10 / 4; //a = 2
c = 10 / 4.0; // c = 2.5
c = 10; // Implicit int to double
b = (int)c; // Explicit double to int
```

### Операторы is и as

Оператор `is` и pattern-matching и `as`.
`is` возвращает `bool`.
`as` пытается привести объект к типу. `null` при неудаче
```csharp
object item = "str";
if(item is string s) Console.WriteLine(s);
string str = item as string;
```

### checked / unchecked

Контроль переполнения целых чисел
- `checked` – контроль включён (при переполнении `OverflowException`)
- `unchecked` – контроль выключен (поведение по умолчанию, как в C++), кроме констант
```csharp
int a = int.MaxValue;
int b = 1;
try
{
    checked
    {
        int result = a + b; // OverflowException
    }
}
catch (OverflowException ex)
{
    Console.WriteLine(ex.Message);
}
```

### Примитивные типы (built-in types)

Типы, напрямую поддерживаемые компилятором
- Раньше советовали FCL-имена (`Int32`), сейчас предпочтительнее примитивные (`int`)
```csharp
bool     // System.Boolean
byte     // System.Byte, 8 бит без знака
sbyte    // System.SByte, 8 бит со знаком
short    // System.Int16, 16 бит со знаком
ushort   // System.UInt16, 16 бит без знака
int      // System.Int32, 32 бита со знаком
uint     // System.UInt32, 32 бита без знака
long     // System.Int64, 64 бита со знаком
ulong    // System.UInt64, 64 бита без знака
float    // System.Single, одинарная точность
double   // System.Double, двойная точность
decimal  // System.Decimal, 128 бит, для финансовых расчётов
char     // System.Char, UTF-16 code unit
```

### Ссылочные типы (Reference Types)

`class`, `interface`, `delegate`
- Переменная хранит ссылку на данные
- Размещаются в куче (heap)

### Значимые типы (Value Types)

`struct`, `enum`
- Переменная хранит само значение
- Размещаются в стеке (stack)
- ...или в куче, если находятся «внутри» ссылочных типов
- ...или в куче, если упакованы
```csharp
int foo = 4;            // в foo лежит 4
string bar = "hello";   // в bar лежит ссылка на "hello" в куче
```

### class или struct?

- `class`: сложные объекты, нужно наследование, важна идентичность объекта, объект может быть большим
- `struct`: маленькие простые типы, без наследования, идентичность не важна, копирование дёшево

### Nullable-типы

- Ссылочные типы: значение по умолчанию `null`
- Значимые типы: значение по умолчанию `0`
- Для совместимости с БД: `null` в значимых типах
- C# 8: **nullable reference types** – соглашение, компилятор лишь выдаёт **предупреждение**
- Ссылочный тип без `?` считается не-null, `!` говорит «здесь точно не null»
```csharp
int? i = null; i = 10;
string s = null;    // раньше
string? s = null;   // сейчас
string str = GetString()!;
```

### Упаковка (Boxing)

Преобразование значимого типа в ссылочный
- В куче выделяется память под объект
- Поля значимого типа копируются в новый объект
- Возвращается ссылка на этот объект
```csharp
int myInt = 123;
object boxedObject = myInt; // Implicit boxing
```

### Распаковка (Unboxing)

Извлечение значимого типа из упакованного объекта
- Проверяется, что объект – упакованный экземпляр **нужного** типа
- Значение копируется из кучи в стек
- Дорогая операция, может бросить `InvalidCastException`
```csharp
int unboxedInt = (int)boxedObject; // Explicit unboxing

double boxedDouble = 3.14;
object another = boxedDouble;
int invalid = (int)another; // InvalidCastException
```

### Изменение полей в упакованных value-type

```csharp
Point p = new Point(1, 1);
object o = p;
p.X = 2;
Console.WriteLine(((Point)o).X); // 1
```
- Меняем исходную структуру `p`, а не упакованную копию в `o`

### Проблемы производительности при упаковке

- Выделение памяти в куче, нагрузка на GC, затраты CPU, лишняя память
- Когда происходит упаковка:
  - значимые типы в необобщённых коллекциях
  - передача значимых типов в методы, ожидающие `object` или интерфейс
  - `ToString()` у значимого типа
- Как избежать: generics, не делать лишних преобразований, выбирать между `struct` и `class`

### Равенство и тождество

- **Тождество**: один и тот же объект в памяти? `Object.ReferenceEquals()`
- **Равенство**: объекты логически равны? `Object.Equals()`

### Реализация равенства

- Нужно переопределять и `Equals()`, и `GetHashCode()`
- Если `Equals()` возвращает `true`, то и `GetHashCode()` одинаковый (но **не наоборот**)
- Ссылочные типы: `Object.Equals()` просто сравнивает ссылки
- `System.ValueType` переопределяет `Equals()`: рефлексия (медленно) или сравнение блоков памяти (быстрее)

### Хеш-коды

Используются в `Dictionary`, `HashSet`
- Равные объекты → одинаковый хеш (у разных тоже может совпасть)
- Хорошее распределение по «корзинам»
- Стабильность: в рамках процесса хеш одного объекта не меняется между вызовами

### Члены типа

- Константы (const)
- Поля (fields)
- Методы (methods)
- Свойства (properties)
- События (events)
- Индексаторы (indexers)
- Операторы (operators)

### Уровни доступа

- `public` – доступ не ограничен
- `protected` – содержащий класс и производные от него
- `internal` – текущая сборка
- `protected internal` – текущая сборка **или** производные классы
- `private` – только содержащий тип
- `private protected` – содержащий класс и производные **в текущей сборке**
- По умолчанию: члены `class` и `struct` – `private`, члены `interface` и `enum` – `public`
- Ограничивайте видимость там, где это возможно

### partial class / interface / struct

- Разделение определения на несколько файлов
- Применение: сгенерированный код (WinForms, WPF) отдельно от пользовательского
```csharp
public partial class Employee
{
    public string? LastName { get; set; }
}

public partial class Employee
{
    public int EmployeeId { get; set; }
}
```

### partial method

Объявление в одном файле, реализация в другом
- Только `void`
- Нельзя `out` (можно `ref`)
- Нельзя `virtual`, `extern`, `override`
- Только `private`, неявно `sealed`
- Если не реализован, компилятор вырезает вызов (без накладных расходов)
```csharp
// File: MyClassDeclaration.cs
partial void OnDataProcessed(string data);

// File: MyClassImplementation.cs
partial void OnDataProcessed(string data)
{
    Console.WriteLine($"Data processing complete for: {data}");
}
```

### virtual, override, new

- `virtual` – метод может быть переопределён
- `override` – явное переопределение виртуального метода
- `new` – новый метод, не связанный с методом базового класса (невиртуальный вызов)
```csharp
public class Animal { public virtual void MakeSound() => Console.WriteLine("Звук"); }
public class Dog : Animal { public override void MakeSound() => Console.WriteLine("Гав"); }

Animal myAnimal = new Dog();
myAnimal.MakeSound(); // Гав (override)

public class Vehicle { public void Move() => Console.WriteLine("Движется"); }
public class Car : Vehicle { public new void Move() => Console.WriteLine("Едет"); }

Vehicle myVehicle = new Car();
myVehicle.Move(); // Движется (new не срабатывает через базовый тип)
Car myCar = new Car();
myCar.Move();     // Едет
```

### sealed и abstract

- `sealed` – запрещает дальнейшее переопределение метода («запечатывает»)
- `abstract` – обязывает производный класс реализовать метод

### Принцип минимальных привилегий (PoLP)

Principle of Least Privilege
- Модуль получает только тот доступ, который минимально необходим для его задачи
- Начинайте с `private`, расширяйте видимость по необходимости
- Запечатывайте типы и методы, где нет наследования, не используйте `virtual` «на всякий случай»

## Константы и методы

### const и readonly

- `const`
  - значение вычисляется на этапе компиляции, «вшивается» в IL-код
  - только built-in типы (или любой тип, если значение `null`)
  - доступ из кода как к `static`-полю
- `readonly`
  - значение вычисляется в runtime (в конструкторе), дальше только чтение
  - любой тип
```csharp
public const double PI = 3.14159;
// PI = 3; // CTE!

public readonly int MaxValue = 100;     // при объявлении
public readonly string ConnectionString; // в конструкторе

public ReadonlyExample(string connection)
{
    ConnectionString = connection;
}
```

### Поля (fields)

Хранят состояние объекта
- Поле типа – `static`
- Поле экземпляра – нестатическое

### Конструкторы экземпляров

Инициализация полей экземпляра

#### Порядок инициализации объекта

1. Выделение памяти и обнуление CLR (в куче, поля = `default`: 0, false, null)
2. Инициализаторы полей, **сверху вниз**: сначала `Child`, потом `Base` (и до `System.Object`)
3. Конструкторы, **снизу вверх**: `System.Object` → `Base` → `Child`
4. Инициализаторы объекта (`new Child { Prop = 1 }`): последними, после всех конструкторов

#### Важно знать

- Конструктор по умолчанию генерируется компилятором для `class` и для `struct`
- C# 10: в `struct` можно явно объявить конструктор без параметров
- C# 11: в `struct` не обязательно заполнять все поля в конструкторе, остальные станут `default`
- `struct` не наследуется от других классов и структур, `base()` в них запрещён

### Первичные конструкторы (Primary constructors, C# 12)

```csharp
// До
public class Person
{
    private readonly string _firstName;
    private readonly string _lastName;
    public Person(string firstName, string lastName)
    {
        _firstName = firstName;
        _lastName = lastName;
    }
}

// После
public class Person(string firstName, string lastName)
{
    private readonly string _firstName = firstName;
    private readonly string _lastName = lastName;
}
```

### Статические конструкторы

`static MyClass() { ... }`
- Вызывается CLR один раз до первого обращения к типу
- Потокобезопасен (выполнится только в одном потоке)
- Нельзя вызвать вручную
- Нужен для инициализации статических полей со сложным доступом

### Методы перегруженных операторов

- Естественный синтаксис операций для наших типов
- Перегружать можно только существующие операторы, новые создать нельзя
- `[]` перегружается через индексатор
- `==` и `!=` только парой (а за ними `Equals` и `GetHashCode`)
- Не все языки .NET поддерживают операторы: в F#, VB.NET это статический метод `op_Addition` и т.п.
- Microsoft рекомендует перегружать для типов, ведущих себя как примитивы (`BigInteger`, `Complex`, `Decimal`); главное не злоупотреблять
```csharp
public static Vector2D operator +(Vector2D left, Vector2D right)
{
    return new Vector2D(left.X + right.X, left.Y + right.Y);
}

public static bool operator ==(Vector2D left, Vector2D right)
{
    return left.X == right.X && left.Y == right.Y;
}

Vector2D sum = v1 + v2;
```

### Методы операторов преобразования

- `implicit` (неявное): автоматически, безопасно
- `explicit` (явное): нужно указать приведение, возможна потеря данных
- Лучше не объявлять `implicit` без особой необходимости (трудно объяснимые последствия)
```csharp
public static implicit operator Fahrenheit(Celsius c) => new(c.Value * 9 / 5 + 32);
Fahrenheit f = tempCelsius;                // неявно

public static explicit operator Centimeter(Meter m) => new(m.Value * 100);
Centimeter cm = (Centimeter)lengthInMeters; // явно
```

### Методы расширения (Extension Methods)

«Добавляют» методы к существующим типам без наследования
- Статический метод в статическом классе, первый параметр с `this`
- Работают только при подключённом `using` пространства имён с классом-контейнером
- Если у типа уже есть метод с такой же сигнатурой, приоритет у него
- Не делайте расширения для базовых типов (`object`, `string`) без необходимости
- Под капотом: синтаксический сахар для вызова статического метода
```csharp
public static class StringExtensions
{
    public static string CapitalizeFirstLetter(this string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return input;
        }
        return char.ToUpper(input[0]) + input.Substring(1);
    }
}

string message = "hello world";
Console.WriteLine(message.CapitalizeFirstLetter()); // Hello world
```

### Необязательные параметры (Optional Parameters)

- Значение по умолчанию «вшивается» в место вызова компилятором
```csharp
void Method(int x, int y = 5) { }

Method(1, 1); // x=1, y=1
Method(2);    // x=2, y=5
```

### Именованные параметры (Named Parameters)

- Вызов с указанием имени, позиция не важна
- Сначала обязательные параметры, затем необязательные
- Версионность: при изменении значения по умолчанию нужно перекомпилировать клиентские сборки, иначе будет старое «вшитое» значение
```csharp
Method(y: 10, x: 20);
```

### var

Неявно типизированные локальные переменные
- `var x = 5; // int`
- Это **не** `dynamic` и **не** `object`: строгая статическая типизация на этапе компиляции
- Используйте, когда тип очевиден из правой части; не используйте, когда непонятен: `var result = SomeMethod();`

### ref и out

- `ref`: метод может изменять переменную вызывающего кода
  - переменная должна быть инициализирована **до** вызова
  - присваивать внутри метода не обязательно
  - `ref` указывается и при вызове
- `out`: возврат значений из метода («пустая коробка»)
  - инициализировать до вызова не нужно
  - метод **обязан** присвоить значение до завершения
  - `out`-параметров может быть сколько угодно
```csharp
void Modify(ref int x) { x = 10; }
Modify(ref myVar);

bool TryParse(string s, out int result)
```

### in

Передача по ссылке только для чтения
- Защита от изменения, нет копирования больших структур
- Компилятор гарантирует, что метод не изменит значение
```csharp
void Process(in BigStruct big) { ... }
Process(in myBigStruct);
```

### params

Переменное количество аргументов
- Под капотом компилятор создаёт массив на лету и передаёт в метод
```csharp
public static int Sum(params int[] numbers)
{
    int total = 0;
    foreach (int num in numbers)
    {
        total += num;
    }
    return total;
}

Sum(1, 2, 3);
Sum(1);
Sum();
Sum(new[] { 1, 2, 3 });
```

### Типы параметров и возвращаемых значений

- Параметры: самые **общие** типы (`IEnumerable<T>` вместо `List<T>`)
- Возвращаемое значение: самые **конкретные** типы (`List<T>` вместо `IList<T>`), если это не нарушает инкапсуляцию
- Для критичного к производительности кода избегайте `params` и часто меняемых `class`-параметров (нагрузка на GC)

## Символы и строки

### char (System.Char)

- Значимый тип, один 2-байтный символ (UTF-16 code unit)
- Эмодзи и редкие символы занимают **два** `char` (суррогатная пара)
- Литералы: `'A'`, `'\n'`, `'\u0041'` (4 hex-цифры), `'\x41'` (1-4 hex-цифры)
- Escape: `\'` `\"` `\\` `\0` `\n` `\r` `\t`
- Анализ: `char.IsDigit`, `IsLetter`, `IsLetterOrDigit`, `IsWhiteSpace`, `IsUpper`, `IsLower`, `GetUnicodeCategory`
- Не пишите `c >= '0' && c <= '9'`: не поймает арабские цифры, а `IsWhiteSpace` знает про `'\u00A0'`

### Регистр и культура

- `ToUpper()` / `ToLower()` – текущая культура, для показа пользователю
  - `'i'.ToUpper()` в `tr-TR` даст `'İ'`
- `ToUpperInvariant()` / `ToLowerInvariant()` – для внутренней логики (ключи словарей, имена файлов, парсинг)

### Rune (System.Text.Rune, .NET Core 3.0+)

- Структура, один полный Unicode-символ (скаляр U+0000..U+10FFFF, без суррогатов)
- Решает проблему «один `char` = половина символа»
```csharp
Rune r = new Rune(0x1F60A);          // 😊
foreach (Rune rune in str.EnumerateRunes()) { }
```

### string (System.String)

- Ссылочный тип, в куче, **неизменяемый** (`ToUpper`, `Replace`, `Substring` возвращают новую строку)
- Последовательность кодовых единиц UTF-16
- `Length` – число `char` (не байт), хранится в поле, считается мгновенно
- `String.Empty` – готовая пустая строка
- В памяти: sync block + method table pointer + длина + массив `char`
- Создание: литерал `"Hello"` (интернируется), `new string('*', 80)`, `ToString()`, `Encoding.UTF8.GetString(bytes)`
- `new string(...)` по умолчанию **не** интернируется

### Интернирование

- Пул интернирования – хеш-таблица CLR внутри процесса, одинаковые литералы хранятся один раз
- `String.Intern(s)` – поместить в пул (или вернуть существующую)
- `String.IsInterned(s)` – есть ли в пуле (иначе `null`)
- Плюсы: экономия памяти, быстрое сравнение по ссылке
- Минус: пул **не очищается GC**, не интернируйте много уникальных строк

### Неизменяемость строк

- Плюсы: потокобезопасность, безопасность при передаче в чужие методы, кеш хеш-кода, семантика значения
- Минус: конкатенация в цикле создаёт много мусора

### Операции

- Конкатенация: `+`, `String.Concat(a, b)`, `String.Join(sep, array)`
- Поиск: `Contains`, `StartsWith`, `EndsWith`, у них есть перегрузки со `StringComparison`, используйте их
- Сравнение: **не** `s1.ToLower() == s2.ToLower()` (2 лишние строки и проблемы культур)
```csharp
string.Equals(s1, s2, StringComparison.Ordinal);
string.Compare(s1, s2, StringComparison.OrdinalIgnoreCase) == 0;
str.StartsWith("A", StringComparison.OrdinalIgnoreCase);
```

### StringComparison

- `Ordinal` / `OrdinalIgnoreCase` – внутренние идентификаторы (файлы, URL, ключи конфигов, теги), самый быстрый вариант
- `CurrentCulture` / `CurrentCultureIgnoreCase` – данные для пользователя (сортировка в UI, ввод пользователя)
- `InvariantCulture` / `InvariantCultureIgnoreCase` – избегать, кроме случаев с сохранением данных, независимых от языка
- `StringComparer` – то же для коллекций
```csharp
var dict = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
```

### Форматирование

- `String.Format("Hello, {0}! You have {1:C}.", name, balance);` – `{индекс:формат}`
- Минусы: массив `object[]`, парсинг шаблона, `ToString()` для каждого аргумента, плох в горячих циклах
- Числа: `"C"` валюта, `"N"` число, `"P"` процент, `"F"` фиксированная точка, свои: `"0000"`, `"##-##"`
- Даты: `"d"` короткая, `"D"` длинная, `"o"` / `"r"` для сериализации, свои: `"yyyy-MM-dd"`, `"HH:mm:ss"`
- Enum: `"G"` строка, `"D"` число, `"X"` hex, `"F"` флаги (`[Flags]`)
- **Интерполяция** `$"Hello, {name}! Today is {date:D}"` (C# 6)
  - до C# 10 компилируется в `String.Format`, это просто удобство
  - с C# 10 сравнима по скорости с `StringBuilder`
- `IFormattable` – чтобы свой тип поддерживал форматы: `ToString(string? format, IFormatProvider? provider)`

### StringBuilder

Изменяемый буфер для построения строк
- Проблема: `s = s1 + s2 + ... + s1000` создаёт ~999 промежуточных строк
- Внутри массив `char[]`; при нехватке места новый массив (ёмкость обычно ×2) и копирование
- `ToString()` создаёт одну итоговую строку, буфер остаётся (очистка `Clear()`)
- Использовать для множества операций в цикле (от ~4-5 `Append`)
- Для 1-2 конкатенаций `"Hello, " + name` лучше обычный `+`
- Задавайте ёмкость, чтобы не было переаллокаций; методы возвращают `this` (chaining)
```csharp
var sb = new StringBuilder(1024);
sb.Append("Hello, ").Append(name).AppendLine("!");
string result = sb.ToString();
```

### Кодировки

- **Encoding** – преобразование символов в байты и обратно (UTF-8, UTF-16, ASCII)
- **Code Page** – таблица «байт → символ», до 256 символов (Win-1251 для кириллицы, 1252 для Европы)
- UTF-8: 1-4 байта, стандарт для веба и файлов, совместим с ASCII
- UTF-16: 2 или 4 байта, внутреннее представление строк в .NET
- UTF-32: 4 байта, редко
- `Encoding.UTF8`, `Encoding.Unicode` (UTF-16), `Encoding.ASCII`, `Encoding.GetEncoding(1251)`
- При работе с файлами **всегда указывайте кодировку** явно
```csharp
new StreamReader("file.txt", Encoding.UTF8);
```

## Обобщения (Generics)

### Идея

Параметризация типов, один код для многих типов
- До (C# 1.0): `ArrayList` на `object`: упаковка, приведения, ошибка только в runtime (`InvalidCastException`)
- С C# 2.0: `List<int>`: ошибка на этапе компиляции, нет упаковки и приведений
```csharp
ArrayList list = new ArrayList();
list.Add("Oops!");
int bad = (int)list[0];        // InvalidCastException в runtime

List<int> nums = new List<int>();
nums.Add(42);
// nums.Add("Oops!");          // ошибка компиляции
```

### Термины

- Параметр типа: `T` в `List<T>`
- Аргумент типа: `int` в `List<int>`
- Открытый тип: `List<T>`; закрытый (сконструированный): `List<int>`

### Обобщённые типы и методы

```csharp
public class Repository<T>
{
    private List<T> _items = new List<T>();
    public void Add(T item) => _items.Add(item);
}

public static T Max<T>(T a, T b) where T : IComparable<T>
{
    return a.CompareTo(b) > 0 ? a : b;
}

class Pair<T1, T2> { public T1 First; public T2 Second; }
Pair<decimal, int> p = new() { First = 45m, Second = 45 };

public struct Point<T> { public T X, Y; }   // структуры тоже можно
```
- Обобщённым может быть и метод в необобщённом классе, и локальная функция (C# 7+)
- Тип аргумента компилятор обычно выводит сам: `Max(5, 10)`

### Преимущества

- Безопасность типов (ошибки при компиляции)
- Производительность (нет boxing/unboxing)
- Повторное использование (`Swap<T>(ref T a, ref T b)` для любых типов)

### Ограничения (constraints)

- Без них компилятор знает про `T` только как про `object`
- `where T : struct` – значимый тип (не `Nullable<T>`)
- `where T : class` – ссылочный тип (non-nullable)
- `where T : class?` – ссылочный, nullable или нет (C# 8)
- `where T : notnull` – не допускает `null`, и ссылочный, и значимый (C# 8)
- `where T : new()` – открытый конструктор без параметров (позволяет `new T()`)
- `where T : BaseClass` – наследник класса
- `where T : IInterface` – реализует интерфейс (самое частое)
- `where T : U` – наследник другого параметра типа
- Комбинируются: `where T : class, IComparable<T>, new()` (`new()` ставится последним)
- Не накладывайте лишних ограничений, они снижают переиспользуемость

### Под капотом (CLR)

- Для **значимых** типов JIT создаёт отдельный код (`List<int>`, `List<DateTime>`), нет упаковки
- Для **ссылочных** типов один общий код (`List<string>`, `List<MyClass>`), все ссылки одного размера
- Статические поля и статический конструктор свои для **каждого закрытого типа**
```csharp
GenericType<string>.Count = 1;
GenericType<int>.Count = 2;     // отдельное поле
```

### Вариантность

- По умолчанию обобщённые типы **инвариантны**: `List<object> o = new List<string>();` не компилируется
- **Ковариантность** (`out T`): можно использовать более производный тип, `T` только на выходе (возвращаемое значение)
```csharp
IEnumerable<string> strings = new List<string>();
IEnumerable<object> objects = strings;   // ok, IEnumerable<out T>
```
- **Контравариантность** (`in T`): можно использовать менее производный тип, `T` только на входе (параметры)
```csharp
IComparer<object> oc = ...;
IComparer<string> sc = oc;               // ok, IComparer<in T>
```
- Делегаты тоже: `Func<string>` → `Func<object>`, `Action<object>` → `Action<string>`
- Вариантность работает для интерфейсов и делегатов, не для классов

### `Nullable<T>`

- `int?` = `Nullable<int>`, особая поддержка CLR и компилятора
- Упаковка: `int? x = null; object o = x;` → `o == null`; со значением упаковывается обычный `int`
- `??`, `?.`, `HasValue`, `Value`

### default и сравнение в generics

- `default(T)` / `default`: `0` для `int`, `null` для `string`, `false` для `bool`
- `value == null` или `value == default(T)` нельзя использовать вслепую
- Правильно: `EqualityComparer<T>.Default.Equals(value, default)`
- Pattern matching: `if (item is List<int> list) { }`

### Прочее

- Коллекции: `ArrayList` → `List<T>`, `Hashtable` → `Dictionary<TKey, TValue>`, `Queue` → `Queue<T>`
- `Span<T>` – ref struct, живёт только на стеке, поэтому **нельзя** `List<Span<int>>` (ошибка CS0306), используйте `Memory<T>`
- .NET 7+: `INumber<T>` – обобщённая арифметика (`T.Zero`, `+=`)
- `record ApiResponse<T>(bool Success, T Data, string Message);`

### Best practices

- Осмысленные имена: `TKey`, `TValue`, `TEntity`, `TResult` вместо `T`, `U`, `V`
- Предпочитайте обобщённые коллекции необобщённым
- Используйте `in` / `out` в своих интерфейсах и делегатах, где уместно
- Антипаттерны: глубокие иерархии generics, проверка `item is int` внутри generic-метода, `new()` без нужды

### Частые ошибки компиляции

- `CS0311`: тип не удовлетворяет ограничению (например, `string` при `where T : class, new()`, нет публичного конструктора)
- `CS0452`: нужен ссылочный тип, а передали `int` при `where T : class`
- `CS0306`: тип нельзя использовать как аргумент типа (`Span<int>`, указатели)
- `CS1960`: `new T()` без ограничения `where T : new()`

## Интерфейсы

### Интерфейс

Контракт: определяет **что** должно быть сделано, но не **как**
- Содержит сигнатуры методов, свойств, событий, индексаторов; нет полей экземпляра
- Зачем: абстракция, полиморфизм, слабая связанность (зависим от интерфейса, а не от класса), тестируемость (mock-объекты)
- Имя принято начинать с `I`

### Интерфейс vs абстрактный класс

- Реализация по умолчанию: интерфейс нет (до C# 8), абстрактный класс да
- Поля экземпляра и конструктор: у интерфейса нет, у абстрактного класса есть
- Наследование: интерфейсов можно **много**, класса только один
- Суть: интерфейс «может» (can-do), абстрактный класс «является» (is-a)

### Объявление и реализация

```csharp
public interface IStorage
{
    void SaveData(string data);
    string Name { get; }
    event Action<string> DataSaved;
}

public class FileStorage : IStorage   // обязан реализовать ВСЕ члены
{
    public void SaveData(string data) => File.WriteAllText("data.txt", data);
    public string Name => "File Storage";
    public event Action<string> DataSaved;
}
```

### Полиморфизм и приведение

```csharp
IStorage storage = new FileStorage();           // upcast, безопасно
FileStorage fs  = (FileStorage)storage;         // InvalidCastException при неудаче
FileStorage fs2 = storage as FileStorage;       // null при неудаче
if (storage is FileStorage fileStorage) { }     // проверка + приведение
```

### Явная реализация (explicit)

- Нужна, когда два интерфейса имеют одинаковый член, или чтобы скрыть член из публичного API класса
- Метод виден только через приведение к интерфейсу
```csharp
public class Shape : IDrawable, UIElement
{
    void IDrawable.Draw() => Console.WriteLine("as IDrawable");
    void UIElement.Draw() => Console.WriteLine("as UIElement");
}

// shape.Draw();            // ошибка
((IDrawable)shape).Draw();  // ok
```
- При одинаковых сигнатурах и одинаковой семантике достаточно **одной** неявной реализации для обоих интерфейсов

### Наследование интерфейсов

- `interface ILogger : IWriter` – класс должен реализовать члены обоих
- Множественное: `class FileDevice : IReadable, IWritable`; `interface IRW : IReadable, IWritable`

### Ковариантность / контравариантность

Как в generics
- `IEnumerable<out T>`: `IEnumerable<Giraffe>` → `IEnumerable<Animal>`
- `IComparer<in T>`: `IComparer<Animal>` → `IComparer<Giraffe>`

### Методы по умолчанию (DIM, C# 8)

- Интерфейс может содержать реализацию метода: можно добавлять методы, не ломая существующие классы
```csharp
public interface ILogger
{
    void Log(string message);
    void LogError(string error) => Log($"ERROR: {error}");
}
```
- Класс **не наследует** такой метод: `fileLogger.LogError(...)` не компилируется (CS1061)
- Вызывать нужно через интерфейс:
```csharp
ILogger logger = fileLogger;
logger.LogError("ok");
((ILogger)fileLogger).LogError("ok");
(fileLogger as ILogger)?.LogError("ok");
```

### Diamond problem

- Класс реализует два интерфейса, которые по-разному переопределяют один DIM → ошибка `CS8705` (нет наиболее конкретной реализации)
- Решение: явно реализовать конфликтующий метод в самом классе
```csharp
public class Person : IFormalGreetable, IFriendlyGreetable
{
    void IGreetable.Greet() => ((IFormalGreetable)this).Greet(); // выбрать одну
}
```

### Обобщённые интерфейсы

- Типобезопасные контракты, без приведений и боксинга: `IComparer<T>` вместо `IComparer`
- Могут иметь ограничения: `IRepository<T> where T : class, IEntity, new()`

### Производительность

- Вызов через интерфейс идёт через vtable, чуть медленнее прямого (обычно незаметно)
- `sealed` классы быстрее
- **Структура → интерфейс = boxing**
```csharp
IDrawable d = point;   // boxing!
```
- Избежать: generic-метод с ограничением, для каждого value type свой код
```csharp
public static void DrawItem<T>(T item) where T : IDrawable { item.Draw(); } // без boxing
```
- Структура, реализующая **обобщённый** интерфейс (`ITransformer<double>`), боксинга не даёт

### Стандартные интерфейсы .NET

- `IEnumerable<T>` – «могу дать перечислитель», один метод `GetEnumerator()`, основа `foreach`
- `IEnumerator<T>` – `Current`, `MoveNext()`, `Reset()` (паттерн Итератор)
- Современная реализация через `yield return`, компилятор сам строит перечислитель
```csharp
public IEnumerator<int> GetEnumerator()
{
    for (int i = 0; i < _numbers.Length; i++)
        yield return _numbers[i];
}
IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
```
- `IComparable<T>` – «умею сравнивать себя с другими» (естественный порядок), `CompareTo(T other)`; используется в `list.Sort()`
- `IComparer<T>` – «умею сравнивать два объекта» (свой порядок), `Compare(T x, T y)`; `list.Sort(new MyComparer())`
- `IEquatable<T>` – типобезопасный `Equals(T other)`, **нет боксинга** для структур
  - реализуется вместе с `Equals(object)` и `GetHashCode()` (`HashCode.Combine(X, Y)`), `==` и `!=` по желанию
- `ISpanFormattable` (.NET 6) – форматирование прямо в `Span<char>` без промежуточных строк (`TryFormat`), для кода без аллокаций
- `IFormattable` – форматы в `String.Format` и интерполяции

## События и делегаты

### Важность делегатов и событий

- Обратные вызовы
- Подписка и уведомления 
- Реактивное поведение
- UI, асинхронность, многопоточность

### Делегаты

- Делегат - тип, представляющий ссылку на метод с определённой сигнатурой (типобезопасный)
```csharp
public delegate int comparison<T>(T x, T y);
public static int CompareByLength(string a, string b) => a.Length.CompareTo(b.Length);
comparison<string> comparator = CompareByLength; // адрес функции передаём

string[] words = {"hi","hello","word"};
Array.Sort(words, comparator);
int result = comparator("hi","hoo");
```

### Анонимные методы и лямбды

```csharp
public delegate bool FilterCondition(int value);
FilterCondition oddFilter = delegate (int n) {return n % 2 != 0;}; // анонимный метод
FilterCondition oddFilter = n => n % 2 != 0; // лямбда
```

### Внутреннее устройство делегата

- `System.MulticastDelegate` -> `System.Delegate`

- Поля:
- - `_target(object)` - ссылка на объект для вызова экземлярного метода (`null` для статического метода)
- - `_methodPtr(IntPtr)` - указатель на метод в памяти

### Статический и экземплярный метод

```csharp
// 1. Объявление делегата
public delegate void MyDelegate(string msg);
// 2. Метод экземпляра
class Logger
{
    public void Log(string msg) => Console.WriteLine($"[Instance] {msg}");
}
// 3. Статический метод и точка входа
class Program
{
    public static void StaticLog(string msg) => Console.WriteLine($"[Static] {msg}");
    static void Main()
    {
        // Подписка на метод экземпляра (нужен объект Logger)
        MyDelegate d1 = new Logger().Log;
        // Подписка на статический метод (объект не нужен)
        MyDelegate d2 = Program.StaticLog;
        // Вызов делегатов
        d1("Hello");
        d2("World");
    }
}
```

### Invoke

- Вызов делегата через Invoke (метод делегата)
```csharp
d?.Invoke("hi"); // С проверкой на null
d("hi"); // Эквивалент Invoke
```

### Multicast

- С помощью += и -= можно подписываться и отписываться на/от функции и создавать цепочки вызовов через делегат.

### GetInvocationList

- Метод делегата, который обращает делегат в лист функции для перебора в foreach

```csharp
// Перебор всех методов с обработкой исключений для ProcessHandler
foreach (ProcessHandler singleHandlerinhandler.GetInvocationList())
{
    try
    {
        singleHandler();
    }
    catch(Exception ex)
    {
        Console.WriteLine($"Ошибка в методе: {ex.Message}");
    }
}
```

### Generic делегаты: Action, Func

- до С# 2.0 для каждой сигнатуры метода нужно было объявлять свой тип делегата.

- `Action` - для методов, которые ничего не возвращают
- `Func` - для методов, которые что-то возвращают
- `Predicate<T>` - для методов, которые возвращают bool

### События `Events`

- Событие - это надстройка над делегатом, предоставляющая механизм для уведомления подписчиков, то есть решает проблему делегата с доступом к нему из любой части кода.

```csharp
public class Button
{
    publiceventEventHandler? Click;
}
// На самом деле под капотом это:
public class Button
{
    // Защищенное поле для хранения подписчиков
    privateEventHandler? _click;
    // Событие -обертка над полем
    publiceventEventHandler? Click
    {
        add { _click += value; } // Аксессор добавления
        remove { _click -= value; } // Аксессор удаления
    }
}
```

### Паттерн для созданиясобытий в .NET

1. Тип делегата для события:
`void ИмяСобытияHandler(object sender, EventArgs e)`
`void ИмяСобытияHandler(object sender, ИмяСобытияEventArgs e)`
2. Открытое поле события:
`public event ИмяСобытия;`
3. Защищенный закрытый виртуальный метод для вызова события :
`OnИмяСобытия(ИмяСобытияEventArgs e);`
4. При необходимости, аргументы события– класс, унаследованный от EventArgs:
    `public class ИмяСобытияEventArgs : EventArgs {…}`

### Аргументы события System.EventArgs

- EventArgs– это класс заглушка, не несущий данных
- Но! Он является родителем для стандартных типов данных событий
- Если необходимо передать данные, вы реализуете свой класс, наследник от EventArgs и добавляете в него необходимые свойства
  
```csharp
public class TemperatureChangedEventArgs : EventArgs
{
    public doubleOldTemperature { get; }
    public doubleNewTemperature { get; }
    public TemperatureChangedEventArgs(doubleoldTemp, doublenewTemp)
    {
        OldTemperature = oldTemp;
        NewTemperature = newTemp;
    }
}
```

### Пример вызова события

```csharp
public class Thermostat
{
    private double _currentTemp;
    public double CurrentTemperature
    {
        get => _currentTemp;
        set
        {
            if (_currentTemp != value)
            {
                var oldTemp = _currentTemp;
                _currentTemp = value;
                // Вызываем защищенный виртуальный метод
                OnTemperatureChanged(new TemperatureChangedEventArgs(oldTemp, _currentTemp));
            }
        }
    }
    // Событие
    public event EventHandler<TemperatureChangedEventArgs>? TemperatureChanged;
    // Защищенный виртуальный метод для вызова события (паттерн .NET)
    protected virtual void OnTemperatureChanged(TemperatureChangedEventArgs e)
    {
        // СОВРЕМЕННЫЙ СПОСОБ (C# 6.0+)
        TemperatureChanged?.Invoke(this, e);
    }
}

// Подписка на событие
Thermostat thermostat = newThermostat();
// Подписка через лямбда-выражение - ПЛОХО ЭТО УТЕЧКА ПАМЯТИ. НЕЛЬЗЯ ОТПИСАТЬСЯ
thermostat.TemperatureChanged += (sender, e) =>
{
    Console.WriteLine($"Temp changed from {e.OldTemperature} to {e.NewTemperature}");
};
// Изменение температуры вызовет событие
thermostat.CurrentTemperature = 25.0;
```

## Исключения (Exceptions)

### Введение в исключения

- Исключение - это:
1. Механизм обработки ошибок времени выполнения.
2. Объект, содержащий информацию об ошибке.
3. Позволяет разделить основной код и код обработки ошибок.

- Классификация ошибок в программах:
1. Ошибки на этапе компиляции (синтаксические)
2. Ошибки во время выполнения (исключения)

### Паттерн "Try" - методов

- Альтернатива исключениям для простых проверок.
- Возвращает bool, а результат выводит через out - параметр.

```csharp
// Стандартный метод с исключением
int number1 = int.Parse("123"); // Выбрасывает FormatException
// Try-паттерн
if (int.TryParse("123", out int number2))
{
    Console.WriteLine($"Преобразование успешно: {number2}");
}
else
{
    Console.WriteLine("Преобразование не удалось.");
}
```

### Блоки try, catch, finally

- `try` - блок, в который помещается код, способный сгенерировать исключение.
- `catch` – блок, который перехватывает и обрабатывает исключение определенного типа
- `finally` – блок, который выполняется всегда, независимо от того, было исключение или нет.

```csharp
try
{
    Console.Write("Введите число: ");
    int number = int.Parse(Console.ReadLine());
    Console.WriteLine($"Вы ввели: {number}");
}
catch (FormatException)
{
    Console.WriteLine("Ошибка! Введена не числовая строка.");
}
```

```csharp
try
{
    int[] numbers = { 1, 2, 3 };
    Console.WriteLine(numbers[5]); // Выход за границы массива!
}
catch (IndexOutOfRangeException ex) // 'ex' - объект исключения
{
    Console.WriteLine($"Произошла ошибка: {ex.Message}");
    // Выведет: "Индекс находился вне границ массива."
}
```

### Иерархия исключений

Все исключения наследуются от класса `System.Exception`
- `SystemException` – исключения, генерируемые средой выполнения (CLR)
- - `IndexOutOfRangeException`
- - `NullReferenceException`
- - `ArgumentException`
- - `FormatException`

### Множественные блоки catch

```csharp
try
{
    // Код, который может вызвать разные исключения
}
catch (DivideByZeroException)
{
    Console.WriteLine("Деление на ноль!");
}
catch (FormatException ex)
{
    Console.WriteLine($"Ошибка формата: {ex.Message}");
}
catch (Exception ex) // Самый общий тип - ловит ВСЕ исключения
{
    Console.WriteLine($"Неожиданная ошибка: {ex.Message}");
}
```

### Блок finally

```csharp
FileStream fileStream = null;
try
{
    fileStream = new FileStream("file.txt", FileMode.Open);
    // Работаем с файлом...
}
catch (FileNotFoundException)
{
    Console.WriteLine("Файл не найден!");
}
finally
{
    // Этот код выполнится в любом случае
    fileStream?.Close(); // Безопасное закрытие (если fileStream не null)
    Console.WriteLine("Ресурс освобожден.");
}
```

### Использование when для фильтрации исключений

```csharp
try
{
    int number = int.Parse("ABC"); // Вызовет FormatException
}
catch (FormatException ex) when (ex.Message.Contains("ABC"))
{
    Console.WriteLine("Ошибка формата из-за строки 'ABC'");
}
catch (FormatException)
{
    Console.WriteLine("Какая-то другая ошибка формата.");
}
```

### Генерация исключений (throw)

- Варианты действий в блоке `catch`:
- - Генерация исключения другого типа – для передачи дополнительной информации или абстракции
- - Повторная генерация того же исключения – для передачи информации коду выше в стеке вызовов
- - Выход из блока `catch` без генерации – когда исключение полностью обработано и программа может продолжить нормальную работу

```csharp
internal sealed class Account
{
    public decimal Balance { get; private set; }
    public static void Transfer(Account from, Account to, decimal amount)
    {
        if (from == null || to == null)
        {
            throw new ArgumentNullException("Счета не могут быть null");
        }
        if (amount <= 0)
        {
            throw new ArgumentException("Сумма перевода должна быть положительной");
        }
        if (from.Balance < amount)
        {
            throw new InvalidOperationException("Недостаточно средств для перевода");
        }
        from.Balance -= amount;
        to.Balance += amount;
    }
}
```

```csharp
try
{
    // ... какой-то код ...
}
catch (Exception ex)
{
    // Логируем ошибку
    LogError(ex);
    // И снова выбрасываем это же исключение для обработки выше
    throw;
}
```

### Реализация пользовательского исключения

```csharp
public class InvalidStudentException : Exception
{
    public InvalidStudentException()
    {
    }
    public InvalidStudentException(string message)
        : base(message)
    {
    }
    public InvalidStudentException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
```

### Паттерн "Exception Builder" для сложных исключений

```csharp
public class DatabaseExceptionBuilder
{
    private string _message;
    private string _connectionString;
    private Exception _innerException;
    public DatabaseExceptionBuilder WithMessage(string message)
    {
        _message = message;
        return this;
    }
    public DatabaseExceptionBuilder WithConnectionString(string connectionString)
    {
        _connectionString = connectionString;
        return this;
    }
    public DatabaseExceptionBuilder WithInnerException(Exception innerException)
    {
        _innerException = innerException;
        return this;
    }
    public DatabaseException Build()
    {
        return new DatabaseException(_message, _connectionString, _innerException);
    }
}

// Использование
var exception = new DatabaseExceptionBuilder()
    .WithMessage("Ошибка подключения к базе данных")
    .WithConnectionString(connectionString)
    .WithInnerException(sqlEx)
    .Build();
```

### Полезные свойства экземпляра Exception

- Свойства `StackTrace` и `InnerException`:
- - `StackTrace` – строковое представление стека вызовов в момент
возникновения исключения
- - `InnerException` – исключение, которое стало причиной текущего исключения
- Другие полезные свойства:
- - `ex.TargetSite` – метод, выбросивший исключение
- - `ex.Data` – словарь для дополнительных данных
- - `ex.HelpLink` – ссылка на документацию
- - `ex.HResult` – HRESULT код ошибки

### Документирование исключений

```csharp
/// <summary>
    /// Выполняет деление двух чисел.
    /// </summary>
    /// <param name="dividend">Делимое.</param>
    /// <param name="divisor">Делитель.</param>
    /// <returns>Результат деления.</returns>
    /// <exception cref="System.ArgumentException">
    /// Выбрасывается, когда <paramref name="divisor"/> равен нулю.
    /// </exception>
    public double Divide(double dividend, double divisor)
    {
        if (divisor == 0)
        {
            throw new ArgumentException("Делитель не может быть нулем.", nameof(divisor));
        }
        return dividend / divisor;
    }
```