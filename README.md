## This is an add-in for  [Fody](https://github.com/SimonCropp/Fody) 

Simplifies logging through a static class and some IL manipulation

[Introduction to Fody](https://github.com/SimonCropp/Fody/wiki/SampleUsage)

## Nuget

Nuget package http://nuget.org/packages/Anotar.Fody 

## Given this as a reference

    public static class Log
    {
        public static void Debug()
        public static void Debug(string format)
        public static void Debug(string format, params object[] args)
        public static void Debug(string message, Exception exception)
        public static void Info(string message)
        public static void Info()
        public static void Info(string format, params object[] args)
        public static void Info(string message, Exception exception)
        public static void Warn(string message)
        public static void Warn()
        public static void Warn(string format, params object[] args)
        public static void Warn(string message, Exception exception)
        public static void Error()
        public static void Error(string message)
        public static void Error(string format, params object[] args)
        public static void Error(string message, Exception exception)
    }

## Your Code

    public class MyClass
    {
        void MyMethod()
        {
            Log.Debug("TheMessage");
        }
    }

## What gets compiled

### If you reference NLog

    public class MyClass
    {
        static NLog.Logger logger = NLog.LogManager.GetLogger("MyClass");

        void MyMethod()
        {
            logger.Debug("Method: MyMethod. Line: ~12. TheMessage");
        }
    }

### If you reference log4net

    public class MyClass
    {
        static log4net.ILog logger = log4net.LogManager.GetLogger("MyClass");

        void MyMethod()
        {
            logger.Debug("Method: MyMethod. Line: ~12. TheMessage");
        }
    }
    
Note that after compilation the reference to Anotar will be removed so you don't need to deploy the assembly.
    
## But why? What purpose does this serve?

### 1. Dont make me think

When I am coding I often want to quickly add a line of logging code. If I dont already have the static `logger` field I have to jump back to the top of the file to add it. This breaks my train of thought. I know this is minor but it is still an annoyance. Static logging methods are much less disruptive to call.

### 2. I want some extra information

Often when I am logging I want to know the method and line number I am logging from. I don't want to manually add this. So using IL I just prefix the message with the method name and line number.

## Why not use CallerInfoAttributes

The CallerInfoAttributes consist of  [CallerLineNumberAttribute](http://msdn.microsoft.com/en-us/library/system.runtime.compilerservices.callerlinenumberattribute.aspx),  [CallerFilePathAttribute](http://msdn.microsoft.com/en-us/library/system.runtime.compilerservices.callerfilepathattribute.aspx) and [CallerMemberNameAttribute](http://msdn.microsoft.com/en-us/library/system.runtime.compilerservices.callermembernameattribute.aspx). The allow you to pass information about the caller method to the callee method. 

So some of this could be achieved using these attributes however there are a couple of points that complicate things.

### 1. Only .net 4.5 and up

So this makes it a little difficult to use with other runtimes.

### 2. Cant be used when passing arrays as `params`

Logging APIs all make use of `params` to pass arguments to a `string.Format`. Since you cant use `params` with CallerInfoAttributes most logging APIs choose not to use these attributes.

You can vote for [Compatibility between `params` with CallerInfoAttributes](http://visualstudio.uservoice.com/forums/121579-visual-studio/suggestions/2762025-caller-membername-filepath-linenumber-of-net-4-5-) 