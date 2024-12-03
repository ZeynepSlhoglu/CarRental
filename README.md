# CarRental Projesi

CarRental, araç kiralama süreçlerini dijitalleştirmek ve kullanıcıların hızlı, güvenilir bir platform üzerinden kiralama işlemlerini gerçekleştirmelerini sağlamak amacıyla geliştirilmiştir.

## Kullanılan Teknolojiler
- **.NET Core MVC**: Web uygulaması geliştirme için temel framework.
- **PostgreSQL**: Veritabanı yönetimi için kullanılmıştır.
- **Docker**: PostgreSQL'in hızlı ve izole bir ortamda çalıştırılması sağlanmıştır.
- **FluentValidation**: Veri doğrulama işlemleri için kullanılmıştır.
- **Bootstrap**: Modern ve duyarlı bir kullanıcı arayüzü tasarımı için tercih edilmiştir.
- **Chart.js**: Grafiklerle verilerin görselleştirilmesi.
- **jQuery & AJAX**: Dinamik içerik güncellemeleri ve istemci-taraflı etkileşimler.
- **Cookie Authentication**: Kimlik doğrulama mekanizması.
- **Role-Based Authorization**: Kullanıcı yetkilendirme işlemleri.

## Proje Yapısı
Projede Onion Architecture prensipleri simüle edilmiştir. Katmanlar şu şekildedir:

1. **CarRental.Domain**:  
   - Entity sınıfları.  
   - Repository arayüzleri (`IRepository` ve `IBaseRepository`).  

2. **CarRental.Infrastructure**:  
   - Repository implementasyonları.  
   - `DataSeeder` sınıfı: Veritabanı başlangıç verilerini eklemek için kullanılır (Admin ve User rolleri).  

3. **CarRental.Application**:  
   - Servis sınıfları ve FluentValidation doğrulama sınıfları.  
   - `ServiceResult` sınıfı ile standart başarı/hata geri dönüş mekanizması.  

4. **UI (Kullanıcı Arayüzü)**:  
   - Controller'lar, ViewModel'ler ve Razor sayfaları.
   - Bootstrap ile modern tasarım, AJAX ve Chart.js ile dinamik grafik görselleştirme.

## Kurulum Adımları
### Gereksinimler
- **.NET 8 SDK**  
- **Docker Desktop**  
- **Node.js**  

### Adımlar
1. **Projeyi Klonlayın**:
   ```bash
   git clone https://github.com/<kullanıcı_adı>/CarRental.git
   cd CarRental

2. **Docker ile PostgreSQL'i Çalıştırın**;
Docker Compose ile servisleri başlatmak için aşağıdaki komutu çalıştırın:

```bash 
docker-compose up -d
 
docker run 
```
3. **Veritabanı Tohumlama (Seeding)**
Proje başlatıldığında, DataSeeder sınıfı sayesinde gerekli roller (Admin ve User) ile bir Admin kullanıcı otomatik olarak oluşturulacaktır.

4. **Migration Oluşturma**
Migration oluşturmak için aşağıdaki komutu çalıştırabilirsiniz:

```bash 
dotnet ef migrations add InitialCreate
```

5. **Uygulamayı Çalıştırın**
Aşağıdaki komutu çalıştırarak projeyi başlatabilirsiniz:

```bash 
dotnet run
```


## Demo Video

Projenin çalışan versiyonunu aşağıdaki görseli tıklayarak izleyebilirsiniz:

[![Proje Demo Videosu](https://img.youtube.com/vi/nLbMEAX_OPY/0.jpg)](https://www.youtube.com/watch?v=nLbMEAX_OPY)

