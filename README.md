# ETicaretAPI
# Onion Architecture
* Core
	* Domain
		* Entities
		* Value Object
		* Enumeration
		* Exceptions (Entity Exceptions )
	* Application
		* DTO
		* ViewModel
		* Mapping
		* Validators
		* CQRS Pattern
		* Servis Aray�zleri [ IRepository ]
* Infrastructure
	* Infrastructure
	* Persistence
		*  Veritabanı operasyonlar�
		* Application alt�nda bulunan servis aray�zlerinin ger�eklendi�i aland�r
		* DbContext
		* Migrations
		* Configurations
		* Seeding
* Presentation 
	* Kullanici erişim katmanidir


 ## Async özelliği olmayan metotlar için
> await Task.Run(() => Table.Update(data)); 
>
> await Task.Run(() => Table.Remove(model));
## CORS
> Geliştirilen uygulamalarda client olarak Browser kullanıldığı durumda CORS politikaları devreye girmektedir.
 Same Origin Policy (SOP) SOP, tarayıcılar (browser) tarafından yüklenen kaynaklarının, birbirleriyle olan paylaşımlarını/ilişkilerini belirli kurallar çerçevesinde kısıtlayan bir politikadır.
 Tarayıcılar tarafından, geliştirilen bir güvenlik mekanizmasıdır. 
 Sadece aynı origine sahip kaynaklar birbirleri ile paylaşıma geçebilmektedir. 
 Eğer, SOP kısıtlaması olmasaydı, zararlı bir sayfayı ziyaret ettiğimizde, o an oturumumuzun açık olduğu bir sayfadaki bilgilerimizin okunabilmesine izin verilecekti. 
 Uygulamaların, farklı originler (alt alan adları veya üçüncü taraf kaynaklar) ile etkiletişime geçme ihtiyacı nedeniyle CORS, SOP’un getirmiş olduğu katı kuralları, kontrollü bir şekilde hafifletmek için kullanılmaktadır.
