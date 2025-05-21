var builder = WebApplication.CreateBuilder(args);

//Piotr Bacior 15 722 - WSEI Kraków

//Registrujemy us³ugi MVC, konkretne typy kontrolerów oraz widoków
builder.Services.AddControllersWithViews();

//Tworzymy aplikacjê ASP.NET Core
var aplikacjaPB = builder.Build();

//Konfigurujemy obs³ugê wyj¹tków oraz wymuszenia HTTPS w aplikacji naszej ASP.NET Core
if (!aplikacjaPB.Environment.IsDevelopment())
{
    //Przekierowujemy wszystkie b³êdy do strony b³êdu 
    aplikacjaPB.UseExceptionHandler("/Home/Error");

    //W³¹czamy nag³ówek HSTS, co zwiêksza bezpieczeñstwo aplikacji przy po³¹czeniach z HTTPS
    aplikacjaPB.UseHsts();
}

//Dokonujemy przekierowania HTTP do HTTPS, co zwiêksza bezpieczeñstwo aplikacji
aplikacjaPB.UseHttpsRedirection();

//Umo¿liwiamy obs³ugê plików statycznych takich jak style czy obrazy
aplikacjaPB.UseStaticFiles();

//Umo¿liwiamy routing w aplikacji, co pozwala na mapowanie adresów URL do odpowiednich kontrolerów i akcji
aplikacjaPB.UseRouting();

//Korzystamy z "Middleware" odpowiedzialnego za autoryzacjê, je¿eli zostanie ona wdro¿ona
aplikacjaPB.UseAuthorization();

//Przechodzimy do ustawienia domyœlnej trasy - nasza aplikacja startuje od ChmodController i jego akcji Index
aplikacjaPB.MapControllerRoute(
    name: "default",
    pattern: "{controller=Chmod}/{action=Index}/{id?}"
);

//Uruchamiamy nasz¹ aplikacjê 
aplikacjaPB.Run();