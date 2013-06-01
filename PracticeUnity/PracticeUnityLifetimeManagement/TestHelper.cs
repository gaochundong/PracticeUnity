using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using System.Threading;

namespace PracticeUnityLifetimeManagement
{
  public static class TestHelper
  {
    public static void TestTransientLifetimeManager()
    {
      IExample example;
      using (IUnityContainer container = new UnityContainer())
      {
        container.RegisterType(typeof(IExample), typeof(Example),
          new TransientLifetimeManager());

        // each one gets its own instance
        container.Resolve<IExample>().SayHello();
        example = container.Resolve<IExample>();
      }
      // container is disposed but Example instance still lives
      // all previously created instances weren't disposed!
      example.SayHello();

      Console.ReadKey();
    }

    public static void TestContainerControlledLifetimeManager()
    {
      IExample example;
      using (IUnityContainer container = new UnityContainer())
      {
        container.RegisterType(typeof(IExample), typeof(Example),
          new ContainerControlledLifetimeManager());

        IUnityContainer firstSub = null;
        IUnityContainer secondSub = null;

        try
        {
          firstSub = container.CreateChildContainer();
          secondSub = container.CreateChildContainer();

          // all containers share same instance
          // each resolve returns same instance
          firstSub.Resolve<IExample>().SayHello();

          // run one resolving in other thread and still receive same instance
          Thread thread = new Thread(
            () => secondSub.Resolve<IExample>().SayHello());
          thread.Start();

          container.Resolve<IExample>().SayHello();
          example = container.Resolve<IExample>();
          thread.Join();
        }
        finally
        {
          if (firstSub != null) firstSub.Dispose();
          if (secondSub != null) secondSub.Dispose();
        }
      }

      try
      {
        // exception - instance has been disposed with container
        example.SayHello();
      }
      catch (ObjectDisposedException ex)
      {
        Console.WriteLine(ex.Message);
      }

      Console.ReadKey();
    }

    public static void TestHierarchicalLifetimeManager()
    {
      IExample example;
      using (IUnityContainer container = new UnityContainer())
      {
        container.RegisterType(typeof(IExample), typeof(Example),
          new HierarchicalLifetimeManager());

        IUnityContainer firstSub = null;
        IUnityContainer secondSub = null;

        try
        {
          firstSub = container.CreateChildContainer();
          secondSub = container.CreateChildContainer();

          // each subcontainer has its own instance
          firstSub.Resolve<IExample>().SayHello();
          secondSub.Resolve<IExample>().SayHello();
          container.Resolve<IExample>().SayHello();
          example = firstSub.Resolve<IExample>();
        }
        finally
        {
          if (firstSub != null) firstSub.Dispose();
          if (secondSub != null) secondSub.Dispose();
        }
      }

      try
      {
        // exception - instance has been disposed with container
        example.SayHello();
      }
      catch (ObjectDisposedException ex)
      {
        Console.WriteLine(ex.Message);
      }

      Console.ReadKey();
    }

    public static void TestExternallyControlledLifetimeManager()
    {
      IExample example;
      using (IUnityContainer container = new UnityContainer())
      {
        container.RegisterType(typeof(IExample), typeof(Example),
          new ExternallyControlledLifetimeManager());

        // same instance is used in following
        container.Resolve<IExample>().SayHello();
        container.Resolve<IExample>().SayHello();

        // run garbate collector. Stored Example instance will be released
        // beacuse there is no reference for it and LifetimeManager holds
        // only WeakReference        
        GC.Collect();

        // object stored targeted by WeakReference was released
        // new instance is created!
        container.Resolve<IExample>().SayHello();
        example = container.Resolve<IExample>();
      }

      example.SayHello();

      Console.ReadKey();
    }

    public static void TestPerThreadLifetimeManager()
    {
      IExample example;
      using (IUnityContainer container = new UnityContainer())
      {
        container.RegisterType(typeof(IExample), typeof(Example),
          new PerThreadLifetimeManager());

        Action<int> action = delegate(int sleep)
        {
          // both calls use same instance per thread
          container.Resolve<IExample>().SayHello();
          Thread.Sleep(sleep);
          container.Resolve<IExample>().SayHello();
        };

        Thread thread1 = new Thread((a) => action.Invoke((int)a));
        Thread thread2 = new Thread((a) => action.Invoke((int)a));
        thread1.Start(50);
        thread2.Start(50);

        thread1.Join();
        thread2.Join();

        example = container.Resolve<IExample>();
      }

      example.SayHello();

      Console.ReadKey();
    }

    public static void TestPerResolveLifetimeManager()
    {
      IExample example;
      using (IUnityContainer container = new UnityContainer())
      {
        container.RegisterType(typeof(IExample), typeof(Example),
          new PerResolveLifetimeManager());

        container.Resolve<IExample>().SayHello();
        container.Resolve<IExample>().SayHello();

        example = container.Resolve<IExample>();
      }

      example.SayHello();

      Console.ReadKey();
    }
  }
}
