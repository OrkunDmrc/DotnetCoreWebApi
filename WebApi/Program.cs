//entity framewok kullanmak için www.nutger.org dan "emntityFrameworkcore" ile "entityframeworkcore.inmemory"
//dotnet cli üzerinden yükleneek
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApi.DbOperations;
using System.Reflection;
using WebApi.Middlewares;
using WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//bu ise database e bağlanmadan sanki oradan değer okuyormuşıuz gibi çalışmayı sağlar.
builder.Services.AddDbContext<BookStoreDbContext>(options => options.UseInMemoryDatabase(databaseName:"BookStoreDB"));
//bu dönüşümlerinin konfigürasyonlarını sağlar. Yani tektek sınıfları birbirine eşitlemeden hızlıca eşitler.
//dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection --version 11.0.0
//dotnet add package AutoMapper --version 11.0.
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddSingleton<ILoggerService, ConsoleLogger>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope()) { 
    var services = scope.ServiceProvider; 
    DataGenerator.Initialize(services);
}

//kendimizin yazdığı console a loglama ile ilgili bilgi veren middleware
app.UseCustomExceptionMiddle();

app.Run();

/*
Dependency Injection (DI) Kavramı (Bağımlılıkların Dışarıdan Verilmesi)


Dependency Injection tekniği uygulayarak bağımlılıkları sınıf içerisinde yönetmek yerine dışarıdan verilmesini sağlarız. Bu sayede bağımlı olunan nesnenin oluşturulması ve yönetimi sınıf dışında yapılmış olur ve bağımlılığın bir kısmı azaltılmış olur.



Aşağıdaki örneği inceleyecek olursak, Foo sınıfı Bar sınıfına bağımlı durumda. Fakat Bar sınıfına ait bir nesneyi yapıcı methodunda parametre olarak dışarıdan verilmesini bekliyor. Bu durumda artık Foo sınıfından bir nesne üretmek istediğimizde aynı zamanda bir de Bar sınıfından nesne üretmeli ve Foo sınıfının yapıcı methoduna vermeliyiz. Bu şekilde Foo sınıfından bir nesne ürettiğimizde aslında Foo sınıfının bağımlı olduğu Bar nesnesini dışarıdan vermiş yani Dependency Injection tekniğini uygulamış olduk.



public class Bar
{
    public void WriteSomething()
    {
        //bar
    }
}

public class Foo
{
    private readonly Bar _bar; //dependency

    public Foo(Bar bar) //dependency injection
    {
        _bar = bar;
    }

    public void DoSomething()
    {
        //do something for Foo
        _bar.WriteSomething();
    }
}

....
Bar bar1 = new Bar();
Foo foo1 = new Foo(bar); //dependency injected.
foo1.DoSomething();


Dependency Injection tekniğini 3 farklı yöntem ile uygulayabiliriz.



1 - Constructor (Yapıcı Method) ile : Bu yöntemde bağımlı olunan nesneler yapıcı methodda belirtilir ve dışarıdan beklenir. Yukarıdaki örnek bu yönteme bir örnektir. Foo sınıfı Bar nesnesini yapıcı methodda bekler. Bu yöntem en sık kullanılan yöntemdir.



2 - Setter Method/Property ile : Bu yöntemde bağımlı olunan nesneler bir method/property aracılığı ile dışardan beklenir.Örnek olarak Foo sınıfımız aşağıdaki şekilde bir Setter method ile bağımlı olduğu Bar nesnesini dışarıdan almış olur.



public class Foo
{
    private Bar _bar; //dependency

    public void SetBar(Bar bar) //dependency injection via Setter
    {
        _bar = bar;
    }

    public void DoSomething()
    {
        //do something for Foo
        _bar.WriteSomething();
    }
}

....
Bar bar1 = new Bar();
Foo foo1 = new Foo();
foo1.SetBar(bar1); //dependency injected.
foo1.DoSomething();


3 - Metot ile : Bu yöntemde bağımlı olunan nesneler yalnızca kullanıldığı methodlarda dışarıdan beklenir. Örnek olarak Foo sınıfı DoSomething metodu içerisinde bağımlı olduğu Bar sınıfına ait bir nesneyi metot parametresi aracılığı ile dışardan almış olur.



public class Foo
{
    public void DoSomething(Bar bar) //dependency injection
    {
        //do something for Foo
        _bar.WriteSomething(); //dependency
    }
}

....
Bar bar1 = new Bar();
Foo foo1 = new Foo();
foo1.DoSomething(bar1);  //dependency injected via Method

DI Container Kavramı


Uygulamamız büyüdükçe/değiştikçe ekleyeceğimiz bir çok yeni sınıf beraberinde yeni bağımlılıkları da getirecektir. Bu da bağımlılıkların yönetiminin zorlaşmasına ve hatta içinden çıkılmaz bir hal almasına sebep olabilir.



Bağımlılık yönetimini kolaylaştırmak için Dependency Injection Container adı verilen kütüphaneler kullanılır. Bu kütüphanelerin yardımı ile ihtiyacımız olan sınıfa ait bir nesneye; bağımlılıkları dışarıdan verilmiş kullanıma hazır bir şekilde rahatlıkla ulaşarak kullanabiliriz. Böylece ihtiyacımız olan bir nesneyi oluştururken bağımlı olduğu nesnelerin de yaratılması işlemlerinden kurtulmuş oluruz.



Container'a uygulamamız içerisindeki hangi sınıfları container aracılığı ile kullanacağımız ile ilgili bilgi veririz. Burada hem kullanacağımız sınıfları hem de bunların bağımlı olduğu diğer sınıfları containera kaydetmiş olmamız gerekir. Container tüm bu sınıfları bildiği için kayıtlı olan bir sınıfa ait bir nesne üretmesi gerektiğinde bağımlılıkları da otomatik olarak çözerek bize ihtiyacımız olan nesneyi oluşturur.



Aşağıdaki örnekte görebileceğimiz gibi hem Foo hem Bar sınıfımız önce container'a kayıt ediliyor. Daha sonra bir Foo nesnesini container'dan istediğimizde container; Foo sınıfının Bar sınıfına olan bağımlılığını görüyor ve önce Bar nesnesini üretip daha sonra Foo nesnesinin yapıcı methoduna bu nesneyi vererek (injection) bize bir Foo nesnesi üretmiş oluyor.



DIContainer container = new DIContainer();
container.Register<Foo>();
container.Register<Bar>();

Foo foo1 = container.GetObject<Foo>();
foo1.DoSomething();


Not: Yukarıdaki örnekte kullanılan DIContainer sınıfı ve metotları anlaşılabilir kılınmak adına isimlendirilmiştir. Kullanılan kütüphaneye göre gerçek method ve sınıf isimleri değişecektir.



Containerların önemini anlamak için örneğimizi biraz daha genişletelim. Bar sınıfının da yeni eklenen Baz sınıfına bağımlı hale geldiğini düşünelim. Son durumda sınıflar aşağıdaki şekilde olacaktır.



public class Baz
{
    public void ReadSomething()
    {
        //baz
    }
}

public class Bar
{
    private readonly Baz _baz; //dependency

    public Bar(Baz baz) //dependency injection
    {
        _baz = baz;
    }

    public void WriteSomething()
    {
        _baz.ReadSomething();
        //do something for Bar
    }
}

public class Foo
{
    private readonly Bar _bar; //dependency

    public Foo(Bar bar) //dependency injection
    {
        _bar = bar;
    }

    public void DoSomething()
    {
        //do something for Foo
        _bar.WriteSomething();
    }
}


Eğer ki bağımlılıkları container kullanmadan kendimiz yönetiyor olsaydık tüm uygulamamız içerisinde aşağıdaki şekilde Bar sınıfının bağımlılığını karşılamak için Baz nesnesi yaratmamız gerecekti. Uygulamamız ne kadar büyük ve Bar kullanıyorsa değişiklik yapacağımız yerler de o kadar çok olacak ve efor harcayacaktık.



Baz baz1 = new Baz();
Bar bar1 = new Bar(baz1); //dependency injected.
Foo foo1 = new Foo(bar); //dependency injected.
foo1.DoSomething();


Fakat container kullandığımız durumda bu değişikliklerin hiçbirini yapmadan, sadece yeni eklediğimiz Baz sınıfını containera kaydetmemiz yeterli olacak. Çünkü container Bar'ın Baz bağımlılığını biliyor ve Baz sınıfı için de bilgisi var. Bu nedenle uygulamamızın hiç bir yerinde başka bir değişiklik yapmamıza gerek yok. Container bizim için bunları yerine getiriyor.



DIContainer container = new DIContainer();
container.Register<Foo>();
container.Register<Bar>();
container.Register<Baz>(); //yeni class için yalnızca bir kayıt ekledik

//diğer kodlarda hiç bir değişikliğe ihtiyacımız yok
Foo foo1 = container.GetObject<Foo>();
foo1.DoSomething();


Dependency Injection Container'lardan kısaca DI Container olarak bahsedilir. Aynı zamanda DI Framework, IoC Container yada IoC Framework olarak da kullanımlarına rastlanabilinir.

.Net Core uygulamalarında kullanılabilecek bir çok farklı DI Container kütüphanesi mevcuttur. Çoğu temelde aynı işlevi sunar, fakat performans ve bazı ek yetenekler nedeniyle ihtiyaca göre bir seçim yapılabilir.

En çok kullanılan IoC
Castle Windsor - Castle Windsor is best of breed, mature Inversion of Control container available for .NET and Silverlight
Unity - Lightweight extensible dependency injection container with support for constructor, property, and method call injection
Autofac - An addictive .NET IoC container
DryIoc - Simple, fast all fully featured IoC container.
Ninject - The ninja of .NET dependency injectors
Spring.Net - Spring.NET is an open source application framework that makes building enterprise .NET applications easier
Lamar - A fast IoC container heavily optimized for usage within ASP.NET Core and other .NET server side applications.
LightInject - A ultra lightweight IoC container
Simple Injector - Simple Injector is an easy-to-use Dependency Injection (DI) library for .NET 4+ that supports Silverlight 4+, Windows Phone 8, Windows 8 including Universal apps and Mono.
Microsoft.Extensions.DependencyInjection - The default IoC container for ASP.NET Core applications.
Scrutor - Assembly scanning extensions for Microsoft.Extensions.DependencyInjection.
VS MEF - Managed Extensibility Framework (MEF) implementation used by Visual Studio.
TinyIoC - An easy to use, hassle free, Inversion of Control Container for small projects, libraries and beginners alike.
Stashbox - A lightweight, portable dependency injection framework for .NET based solutions.

.NET Core DI Container (Services)


.NET Core kendi içerisinde kullanıma hazır bir DI Container'ı barındırır. Bu sayede herhangi bir farklı kütüphane kullanmamıza gerek kalmadan uygulamamız içerisinde .net core di containerını rahatlıkla kullanabiliriz.



.Net Core içerisinde hazır bulunan containerı Startup'daki ConfigureServices metodu içerisinde kullanırız. Bu methodun IServiceCollection tipinde services adıyla aldığı parametre aslında container nesnesidir diye düşünebiliriz.



public void ConfigureServices("""""IServiceCollection""""" services)
{
    services.Add... //Register services
}


.Net Core DI Container'a bir sınıf kayıt ederken bu sınıfa ait nesnenin yaşam süresini de belirtmemiz gerekir. Bu yaşam süresine göre container kayıt sırasında kullanacağımız method ismi değişmektedir. Containerda nesnelerin yaşam süresi 3 çeşittir.



1 - Singleton Service : Bu yaşam süresine sahip nesne, uygulamanın çalışmaya başladığı andan duruncaya kadar geçen tüm süre boyunca yalnızca bir kez oluşturulur ve her zaman aynı nesne kullanılır. Singleton bir servis eklememiz için AddSingleton methodunu kullanırız. Örnek : services.AddSingleton<Foo>();



2 - Scoped Service : Bu yaşam süresine sahip nesne, bir http requesti boyunca bir kez oluşturulur ve response oluşana kadar her zaman aynı nesne kullanılır. Scoped bir servis eklememiz için AddScoped methodunu kullanırız. Örnek : services.AddScoped<Bar>();



3 - Transient Service : Bu yaşam süresine sahip nesne, container tarafından her seferinde yeniden oluşturulur. Transient bir servis eklememiz için AddTransient methodunu kullanırız. Örnek : services.AddTransient<Baz>();

Eğer kayıt edilecek servis bir interface implemente ediyor ve bu interface aracılığı ile kullanılıyor ise; kayıt sırasında hem interface tipini hem de bu interface'i implemente eden sınıfı belirtmemiz gerekir. Bu şekilde yaptığımız kayıtlarda da nesnenin yaşam süresini belirtmemiz gereklidir.



Örnekler :



services.AddSingleton<IFoo, Foo>(); services.AddTransient<IBaz, Baz>(); services.AddScoped<IBar, Bar>();



Bu şekilde bağımlı olunan nesnenin sınıfını bilmemize gerek kalmadan bir interface yardımı ile ihtiyaç duyduğumuz iletişimi sağlamış oluruz. Bağımlılıkların interface ile yönetilmesi uygulamamızdaki parçaların loosely coupled (gevşek bağımlı) kalmalarına yardımcı olan en büyük etmenlerden biridir. Loosely coupled uygulamalar daha esnek, kolay genişletilebilir/değiştirilebilir ve test edilebilir olurlar.



Aşağıdaki örnekte görebilebileceğimiz gibi, bağımlılıklar artık direkt olarak sınıf yerine bir interface üzerinden alınıyor. Böylece ihtiyaç duyulan interface'i implemente eden herhangi bir sınıfa ait nesne, bağımlı olan sınıf tarafından kullanılabilir. İlgili interface için hangi sınıfın kullanılacağı bilgisini ise container'a kaydetmiş olmamız gereklidir.




public interface IBaz {...}
public class Baz : IBaz {...}

public interface IBar {...}
public class Bar : IBar
{
    private readonly IBaz _baz; //dependency

    public Bar(IBaz baz) //dependency injection
    {
        _baz = baz;
    }
    ...
}

public interface IFoo {...}
public class Foo : IFoo
{
    private readonly IBar _bar; //dependency

    public Foo(IBar bar) //dependency injection
    {
        _bar = bar;
    }
    ...
}
.Net Core DI Container, bağımlılıkları yapıcı method (Constructor) yada Method Injection yöntemi ile sağlar. Method Injection yöntemini kullanmak için Controller sınıfı içerisindeki action method parametrelerine [FromServices] attribute ile ihtiyaç duyulan bağımlılık belirtilir. Yapıcı method yöntemi için ise Controller sınıfının yapıcı methoduna bağımlı olunan nesne belirtilmesi yeterlidir.



public class HomeController : Controller
{
    private readonly IDateTime _dateTime;

    public HomeController(IDateTime dateTime)
    {
        //constructor injection
        _dateTime = dateTime;
    }

    //Method injection
    public IActionResult Dependency([FromServices] IDateTime dateTime)
    {
        ...
    }
}


Veritabanı işlemlerimiz için EntityFramework Core kullanıyorsak, kullanılan DbContext'leri de Containera kaydedebilir ve DbContext'ler için de dependency injection uygulayabiliriz. DbContext'leri containera kaydetmek için AddDbContext methodunu kullanırız. Örnek : services.AddDbContext<MyDbContext>();



Containera kayıtlı servislerin kullanımı için IServiceCollection'ın yada herhangi bir methodun kullanımına ihtiyaç yoktur. ConfigureServices içerisinde containera kayıt edilen tüm servisler, yukarıdaki örnekte olduğu gibi Controller sınıfların yapıcı methodlarında belirtilerek kullanılabilirler. Controller sınıfları özel sınıflar olduğundan nesnelerinin yaratılması sırasında bağımlılıkları container üzerinden otomatik olarak çözülerek yaratılırlar.
*/
