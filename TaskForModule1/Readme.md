# Exploring assemblies in .Net

## Task 1 - Create Hello World application

1. #### Create simple console application in Visual Studio
```
using System;
namespace HelloWorldApplication
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
```
2. ##### Change assembly version to 4.2.2.1
`[assembly: AssemblyFileVersion("1.0.0.0")]` - `[assembly: AssemblyFileVersion("4.2.2.1")]`

3. ##### Examine assembly with ILDASM tool
  ###### Manifest:
  ![Manifest](https://i.ibb.co/Jvd5XHq/Manifest.png)

  ###### Types metadata:
  ![Types metadata](https://i.ibb.co/WtJGvHV/TypeDef.png)


 ## Task 2 - Create static method "GetCurrentTime()"

```
using System;

namespace HelloWorldApplication
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine(GetCurrentTime());
        }

        private static DateTime GetCurrentTime()
        {
            return DateTime.Now;
        }
    }
}
```
1. ###### TypeDef - New def "GetCurrentTime":
![New Def static method](https://i.ibb.co/C1pVt08/Type-Def-Static-Method.png)

2. ###### TypeRef - New ref to "System.DateTime"
![New Ref to DateTime](https://i.ibb.co/vYrmRmY/Type-Ref-Data-Time.png)

## Task 3 - Custom DLL
*HelloWorldApplication:*
```
using System;
using TimeManager;

namespace HelloWorldApplication
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var timeService = new TimeService();
            var currentTime = timeService.GetCurrentTime();
            Console.WriteLine(currentTime);
        }
    }
}
```
*TimeManager:*
```
using System;

namespace TimeManager
{
    public class TimeService
    {
        public DateTime GetCurrentTime()
        {
            return DateTime.Now;
        }
    }
}
```
*In extern links to the libraries are stored at the beginning of the manifest:*

![Extern to TimeManager](https://i.ibb.co/Nn5Y2qc/Extern-To-Time-Manager.png)

## Task 4 - Debug vs Release

 1. #### Create console application "DebugVsRelease"
 ```
using System;
namespace DebugVsRelease
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Console.WriteLine($"{i}*{j} = {i * j}");
                }
            }
        }
    }
}
```
 2. #### Compare Debug build and Release build.
![Debug Vs Release](https://i.ibb.co/zmyHK00/Debug-Vs-Release.png)
