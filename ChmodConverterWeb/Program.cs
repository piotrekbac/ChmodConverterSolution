var builder = WebApplication.CreateBuilder(args);

//Piotr Bacior 15 722 - WSEI Krak�w

//Registrujemy us�ugi MVC, konkretne typy kontroler�w oraz widok�w
builder.Services.AddControllersWithViews();

//Tworzymy aplikacj� ASP.NET Core
var aplikacjaPB = builder.Build();

//Konfigurujemy obs�ug� wyj�tk�w oraz wymuszenia HTTPS w aplikacji naszej ASP.NET Core
if (!aplikacjaPB.Environment.IsDevelopment())
{
    //Przekierowujemy wszystkie b��dy do strony b��du 
    aplikacjaPB.UseExceptionHandler("/Home/Error");

    //W��czamy nag��wek HSTS, co zwi�ksza bezpiecze�stwo aplikacji przy po��czeniach z HTTPS
    aplikacjaPB.UseHsts();
}

//Dokonujemy przekierowania HTTP do HTTPS, co zwi�ksza bezpiecze�stwo aplikacji
aplikacjaPB.UseHttpsRedirection();

//Umo�liwiamy obs�ug� plik�w statycznych takich jak style czy obrazy
aplikacjaPB.UseStaticFiles();

//Umo�liwiamy routing w aplikacji, co pozwala na mapowanie adres�w URL do odpowiednich kontroler�w i akcji
aplikacjaPB.UseRouting();

//Korzystamy z "Middleware" odpowiedzialnego za autoryzacj�, je�eli zostanie ona wdro�ona
aplikacjaPB.UseAuthorization();

//Przechodzimy do ustawienia domy�lnej trasy - nasza aplikacja startuje od ChmodController i jego akcji Index
aplikacjaPB.MapControllerRoute(
    name: "default",
    pattern: "{controller=Chmod}/{action=Index}/{id?}"
);

//Uruchamiamy nasz� aplikacj� 
aplikacjaPB.Run();