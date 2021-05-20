# Recipe App

## Docker ile Kurulum

---

Gereksinimler

- Docker

`appsettings.json` dosyasındaki PostgreSQL ve MongoDB için **connection string** ve **mail parametlerini** `docker-compose.yaml` dosyasına uygun şekilde giriniz.

PostgreSQL için
`Username=user;password=postgres;Database=recipe;host=postgresql;port=5432;Pooling=True;`

MongoDB için `mongodb://mongodb:27017` değerlerini kullanabilirsiniz.

Mail değerlerini girmek zorunlu değildir. Eğer girdiyseniz [ModeratorController.cs](/src/RecipeApp.Web/Controllers/ModeratorController.cs) dosyasının `76. satırını` yorum satırından çıkararak aktif hale getirebilirsiniz.

Ardından,

```docker-compose
docker-compose up -d
```

komutu ile uygulamayı çalıştırabilirsiniz.
Adımları doğru yaptıysanız [http://localhost:5001](http://localhost:5001) adresinde proje yayında olacaktır.

## Manuel Kurulum

---

Gereksinimler

- .Net 5.0
- MongoDB
- PostgreSQL

Gereksinimlerin tamamı bilgisayarınızda yüklü ise yapmanız gereken şey `appsettings.json` dosyasındaki PostgreSQL ve MongoDB **connection string** ve **mail parametrelerine** kendi ayarlarınızı girmektir. 

Ardından
```
dotnet run
```
komutu ile uygulamayı çalıştırabilirsiniz.

---
Admin Paneli için [/dashboard]()

Kullanıcı Adı `admin`

Parola `asd123`