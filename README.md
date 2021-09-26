# Об репозитории
Volga IT’XXI - C# Задача - Отборочный этап - Решение 

- [x] Задокументированный код (XML)
- [x] Комментарии, объясняющие код
- [x] Использование стандартных классов (List, Dictionary, Regex, StreamReader, StreamWriter, StringBuilder и т.д.)
- [x] Обработка исключений (Внутри Analyzer.cs, вывод в Лог)
- [x] Чистый код
- [x] Много-уровневая архитектура
- [x] Расширяемость проекта (Возможность создания классов, наследующих `IReader`,`ICleaner`,`IWriter`)
- [x] Соблюдение ООП
- [x] Соблюдение SOLID
- [x] Использование паттерна `Facade` (Analyzer.cs)
- [x] Тесты (`WebpageAnalyzer.Tests`)
- [x] Сохранение в БД (`SQLWriter.cs`)
- [x] Лог (`static class Logger`, `struct LogMessage`)

# Как запустить
Требования:
- Visual Studio 2019+
- .NET 5

В Visual Studio потребуется открыть два проекта:
- WebpageAnalyzer/WebpageAnalyzer/WebpageAnalyzer.csproj
- WebpageAnalyzer/WebpageAnalyzer.Tests/WebpageAnalyzer.Tests.csproj

(Или создать пустое решение VS2019+ и добавить туда два этих проекта)

# Как работает и Архитектура

### WebpageAnalyzer.csproj

![Изображение архитектуры](/ARCHITECTURE.png?raw=true)

Проект является консольной программой, которая при запуске выводит сообщение: "Укажите путь до файла, которое потребуется обработать", и ждет ввода от пользователя.
После этого программа создает экземпляр класса `new Analyzer(IReader, ICleaner, IWriter)` и вызывает метод `Process()`, который:
1. Читает текст (с помощью указанного `IReader`: `FileReader` - из файла, `WebReader` - с сайта)
2. Разбивает текст на слова (с помощью класса `Splitter`)
3. Очищает массив слов от ненужных символов и убирает лишние слова (с помощью указанного `ICleaner`: `HtmlCleaner` - убирает HTML теги и символы)
4. Подсчитывает сколько раз встречалось каждое слово (с помощью класса `Counter`)
4. Сортирует статистику (с помощью класса `Sorter`)
5. Выводит статистику в консоль (Если `WriteResultsToConsole` в `Analyzer` было `true`)
6. Сохраняет статистику (с помощью указанного `IWriter`: `FileWriter` - в файл, `SQLWriter` - в SQL базу данных)
7. Сохраняет лог в файлы (В текущей папке выполнения, в файл `Log.txt` - весь лог и в `Errors.txt` - только ошибки)

Проект состоит из:
- Program.cs
- Analyzer.cs (`class Analyzer`)
- Components
  - Reader
    - IReader.cs (`interface IReader`)
    - FileReader.cs (`class FileReader`)
    - WebReader.cs (`class WebReader`)
  - Cleaner
    - ICleaner.cs (`interface ICleaner`)
    - HtmlCleaner.cs (`class HtmlCleaner`)
  - Writer
    - IWriter.cs (`interface IWriter`)
    - FileWriter.cs (`class FileWriter`, `enum FileFormat`)
    - SQLWriter.cs (`class SQLWriter`)
  - Splitter.cs (`class Splitter`)
  - Counter.cs (`class Counter`)
  - Sorter.cs (`class Sorter`)
- Log
  - Logger.cs (`class Logger`)
  - LogMessage.cs (`struct LogMessage`, `enum MessageType`)

### WebpageAnalyzer.Tests.csproj
Проект является тестами MSTest, которые проверяют функционал классов `Splitter`,`Cleaner`,`Counter` в проекте *WebpageAnalyzer.csproj*

Проект состоит из:
- SplitterTests.cs (Проверяет класс `Splitter` на правильность разбиения текста одним символом и несколькими)
- CleanerTests.cs (Проверяет класс `Cleaner` на правильность фильтрации по нескольким правилам)
- CounterTests.cs (Проверяет класс `Counter` на правильность подсчета с учетом регистра и без)
