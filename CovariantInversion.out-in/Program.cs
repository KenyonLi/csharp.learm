using System.Security.AccessControl;
using System.Threading.Channels;
using static CovariantInversion.out_in.Program;
using static System.Net.Mime.MediaTypeNames;

namespace CovariantInversion.out_in;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello,协变与逆变!");

        ICovariance<Animal> animaProvider = new DogProvider();
        Animal animal = animaProvider.Get();

        List<string> names = new List<string>();
        IContravariant<Dog> dogProcessor = new AnimalProcessor();
        dogProcessor.Set(new Dog());

       // IBase<object, int> f = null;
       //// IBar<string, int> f1 = null;
       // f = f1;




        // https://www.cnblogs.com/lemontea/archive/2013/02/17/2915065.html
        #region --

        {
            //父类 = 子类
            string str = "string";
            object obj = str;//变了
        }

        // 协变  泛型委托
        {
            MyFuncA<object> funcAObject = null;
            MyFuncA<object> funcOObject = null;

            MyFuncA<string> funcAString = null;
            MyFuncB<object> funcBObject = null;
            MyFuncB<string> funcBString = null;
            MyFuncB<int> funcBInt = null;

            // 不同类型 
            //funcAObject = funcAString;//编译失败，MyFuncA不支持逆变与协变
            funcAObject = funcOObject;

            funcBObject = funcBString;//变了，协变
            // funcBObject = funcBInt;//编译失败，值类型不参与协变或逆变


        }

        {
            //逆变
            MyActionB<Animal> animalIn = null;
            MyActionB<Dog> dog = null;
            // animalIn = dog;
            dog = animalIn;
        }

        {
            IFlyB<Animal> animal2 = null;
            IFlyB<Dog> dog = null;
            animal2 = dog; //协变 out 
            animal = animal2.Get();
        }

        {// 协变 泛型接口
            IFlyA<object> flyAObject = null;
            IFlyA<string> flyAString = null;
            IFlyB<object> flyBObject = null;
            IFlyB<string> flyBString = null;
            IFlyB<int> flyBInt = null;

            // flyAObject = flyAString;//编译失败，IFlyA不支持逆变与协变
            flyBObject = flyBString;//变了，协变
                                    //flyBObject = flyBInt;//编译失败，值类型不参与协变或逆变
                                    //数组：
            string[] strings = new string[] { "string" };
            object[] objects = strings;

            //int[] ints = new int[] { 1, 2, 3 };
            //object[] intobject = ints; 不支持
        }

        //逆变 泛型委托
        {
            MyActionA<object> actionAObject = null;
            MyActionA<string> actionAString = null;
            MyActionB<object> actionBObject = null;
            MyActionB<string> actionBString = null;
            //actionAString = actionAObject;//MyActionA不支持逆变与协变,编译失败
            actionBString = actionBObject;//变了，逆变
        }
        // 塑变 泛型接口
        {
            IPlayA<object> playAObject = null;
            IPlayA<string> playAString = null;
            IPlayB<object> playBObject = null;
            IPlayB<string> playBString = null;
            //playAString = playAObject;//IPlayA不支持逆变与协变,编译失败
            playBString = playBObject;//变了，逆变
        }

        /*
           以前的泛型系统（或者没有in/out关键字时），是不能“变”的，无论是“逆”还是“顺（协）”
           当前仅支持接口和委托的逆变与协变，不支持类和方法.但是数组也有协变
           值类型不参与逆变、协变
         */
        #endregion

        IFooIn<object> _object = null;
        IFooIn<string> fooInObject = null;
        fooInObject = _object;

        // _object = fooInObject;


        IFooOut<object> ob = null;
        IFooOut<string> st = null;
        ob = st;
        // st = ob;
    }

    public interface IBar<in T> { }
    //应该是in
    public interface IFooIn<in T>
    {
        //void  Test(IBar<T> bar);  报错
        IBar<T> Test();
        void Get(T b);
    }
    //还是 out
    public interface IFooOut<out T>
    {
        void Test(IBar<T> bar);
    }


    //泛型委托：
    public delegate T MyFuncA<T>();//不支持逆变与协变
    public delegate T MyFuncB<out T>();//支持协变

    //泛型接口
    public interface IFlyA<T> { }//不支持逆变与协变
    public interface IFlyB<out T>
    {
        T Get();
    }//支持协变

    //泛型委托
    public delegate void MyActionA<T>(T param);//不支持逆变与协变
    public delegate void MyActionB<in T>(T param);//支持逆变
    //泛型接口
    public interface IPlayA<T> { }//不支持逆变与协变
    public interface IPlayB<in T> { void Set(T values); }//支持逆变


    //public class Stent<out T, in O>
    //{

    //    void Get<out t, in o>();
    //}

    /// <summary>
    /// 协变（covariance） :用于返回类型，允许使用派生类类型，用 ‘out’ 关键字修改
    /// 逆变（contravariance）:用于参数类型，允许使用基类型，用‘in’ 关键字修改
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="O"></typeparam>

    interface GT_Inferface<in T, out O>
    {
        O Show(T obje);
    }

    //interface GT_inferface2<in T,out O>
    //{
    //    T Show(O obje);
    //}

    public class Animal { }

    public class Dog : Animal { }


    /// <summary>
    /// 协变
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICovariance<out T>
    {
        T Get();
    }

    public class DogProvider : ICovariance<Dog>
    {
        public Dog Get() => new Dog();
    }

    /// <summary>
    /// 逆变
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IContravariant<in T>
    {
        void Set(T value);
    }

    public class AnimalProcessor : IContravariant<Animal>
    {
        public void Set(Animal value) => Console.WriteLine(value.GetType().Name);
    }


}

//协变
interface IFooCo<out T> { }
//逆变
interface IFooContra<in T> { }
interface IBase<out T1, in T2> { }
interface IBar<out T1, in T2, T3> : IBase<T1, T2>
{
    //协变泛型接口作为方法的返回类型时， “IFooCo<>” 中填写“out T1” 作为泛型参数
    IFooCo<T1> Test1();
    //逆变泛型接口作为方法的返回类型时，“IFooContra<>”中填写“in T2” 作为泛型参数
    IFooContra<T2> Test2();

    IFooContra<T3> Test3();

    IFooCo<T3> Test4();

    T1 Get(T2 ob);

    //协变泛型接口作为方法入参类型时，"IFooContra<>"中填写“Out T1” 作为泛型参数
    void Test6(IFooContra<T1> fooCo);
    //逆变泛型接口作为方法的入参类型时，"IFooCo<>"中填写“In T2” 作为泛型参数
    void Test4(IFooCo<T2> fooCo);

    IFooCo<T1> Test3(IFooCo<T2> fooCo, IFooContra<T1> foCo);
}
